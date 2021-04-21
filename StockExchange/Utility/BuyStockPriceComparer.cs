using StockExchange.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchange.Utility
{
    public class BuyStockPriceComparer : IComparer<Order>
    {
        public int Compare(Order x, Order y)
        {
            if (x == null || y == null)
                return 1;
            if (x.OrderPrice == y.OrderPrice)
                return x.OrderCreationTime.CompareTo(y.OrderCreationTime);
            return y.OrderPrice.CompareTo(x.OrderPrice);
        }
    }
}
