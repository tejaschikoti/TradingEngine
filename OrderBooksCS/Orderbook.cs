﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingEngine.Orders;
using TradingEngine.Instrument;

namespace TradingEngine.Orderbook
{
    public class Orderbook : IRetrievalOrderbook
    {
        private readonly Security _instrument;
        private readonly Dictionary<long, OrderbookEntry> _orders = new Dictionary<long, OrderbookEntry>();
        private readonly SortedSet<Limit> _askLimits = new SortedSet<Limit>(AskLimitComparer.Comparer);
        private readonly SortedSet<Limit> _bidLimits = new SortedSet<Limit>(BidLimitComparer.Comparer);


        public Orderbook(Security instrument)
        {
            _instrument = instrument;
        }


        public int Count => _orders.Count;

        public void AddOrder(Order order)
        {
            var baseLimit = new Limit(order.Price);
            AddOrder(order, baseLimit, order.IsBuySide ? _bidLimits : _askLimits, _orders);
        }
        private static void AddOrder(Order order,Limit baseLimit,SortedSet<Limit> limitLevels, Dictionary<long, OrderbookEntry> internalBook)
        {
            if(limitLevels.TryGetValue(baseLimit,out Limit limit))
            {
                OrderbookEntry orderbookEntry = new OrderbookEntry(order, baseLimit);
                if(limit.Head == null)
                {
                    limit.Head = orderbookEntry;
                    limit.Tail = orderbookEntry;
                }

                else
                {
                    OrderbookEntry tailPointer = limit.Tail;
                    tailPointer.Next = orderbookEntry;
                    orderbookEntry.Previous = tailPointer;
                    limit.Tail = orderbookEntry;
                }
                internalBook.Add(order.OrderId, orderbookEntry);
            }
            else
            {
                limitLevels.Add(baseLimit);
                OrderbookEntry orderbookEntry = new OrderbookEntry(order, baseLimit);
                baseLimit.Head = orderbookEntry;
                baseLimit.Tail = orderbookEntry;
                internalBook.Add(order.OrderId, orderbookEntry);
            }
        }

        public void ChangeOrder(ModifyOrder modifyOrder)
        {
            if(_orders.TryGetValue(modifyOrder.OrderId,out OrderbookEntry obe))
            {
                RemoveOrder(modifyOrder.ToCancelOrder());
                AddOrder(modifyOrder.ToNewOrder(),obe.ParentLimit,modifyOrder.IsBuySide ? _bidLimits : _askLimits,_orders);
            }

        }


        public bool ContainsOrder(long orderId)
        {
            return _orders.ContainsKey(orderId);
        }

        public List<OrderbookEntry> GetAskOrders()
        {
            List<OrderbookEntry> orderbookEntries = new List<OrderbookEntry>();
            foreach (var askLimit in _askLimits)
            {
                if (askLimit.IsEmpty) continue;
                else
                {
                    OrderbookEntry askLimitPointer = askLimit.Head;
                    while (askLimitPointer != null)
                    {
                        orderbookEntries.Add(askLimitPointer);
                        askLimitPointer = askLimitPointer.Next;
                    }
                }

            }

            return orderbookEntries;
        }

        public List<OrderbookEntry> GetBidOrders()
        {
            List<OrderbookEntry> orderbookEntries = new List<OrderbookEntry>();
            foreach (var bidLimits in _bidLimits)
            {
                if (bidLimits.IsEmpty) continue;
                else
                {
                    OrderbookEntry bidLimitPointer = bidLimits.Head;
                    while (bidLimitPointer != null)
                    {
                        orderbookEntries.Add(bidLimitPointer);
                        bidLimitPointer = bidLimitPointer.Next;
                    }
                }

            }

            return orderbookEntries;
        }

        public OrderbookSpread GetSpread()
        {
            long? bestAsk = null;
            long? bestBid = null;
            if (_askLimits.Any() && !_askLimits.Min.IsEmpty)
                bestAsk = _askLimits.Min.Price;
            if (_bidLimits.Any() && !_bidLimits.Max.IsEmpty)
                bestBid = _bidLimits.Max.Price;
            return new OrderbookSpread(bestBid, bestAsk);

        }

        public void RemoveOrder(CancelOrder cancelOrder)
        {
            if(_orders.TryGetValue(cancelOrder.OrderId, out OrderbookEntry obe))
            {
                RemoveOrder(cancelOrder.OrderId, obe, _orders);
            }
        }

        private static void RemoveOrder(long orderId,OrderbookEntry obe, Dictionary<long, OrderbookEntry> internalBook)
        {
            if(obe.Previous != null  && obe.Next != null)
            {
                obe.Next.Previous = obe.Previous;
                obe.Previous.Next = obe.Next;
            }

            else if(obe.Previous != null)
            {
                obe.Previous.Next = null;

            }

            else if(obe.Next != null )
            {
                obe.Next.Previous = null;
            }

            if(obe.ParentLimit.Head == obe && obe.ParentLimit.Tail == obe)
            {
                obe.ParentLimit.Head = null;
                obe.ParentLimit.Tail = null;
            }

            else if (obe.ParentLimit.Head == obe)
            {
                obe.ParentLimit.Head = obe.Next;
            }

            else if (obe.ParentLimit.Tail == obe)
            {
                obe.ParentLimit.Tail = obe.Previous;
            }

            internalBook.Remove(orderId);
        }

    }
}
