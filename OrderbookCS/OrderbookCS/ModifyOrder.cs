﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingEngine.Orders
{
    public class ModifyOrder : IOrderCore
    {

        public ModifyOrder(IOrderCore orderCore, long modifyPrice, uint modifyQuantity, bool isBuySide)
        {

            Price = modifyPrice;
            Quanity = modifyQuantity;
            IsBuySide = isBuySide;

            _orderCore = orderCore;
        }

        public long Price { get; private set; }

        public uint Quanity { get; private set; }

        public bool IsBuySide { get; private set; }

        public long OrderId => _orderCore.OrderId;

        public string Username => _orderCore.Username;

        public int SecurityId => _orderCore.SecurityId;

        public CancelOrder ToCancelOrder()
        {
            return new CancelOrder(this);
        }

        public Order ToNewOrder()
        {
            return new Order(this);
        }


        
        private readonly IOrderCore _orderCore;

    }
}
