using StockExchange.Models;
using StockExchange.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchange.Service
{
    public class StockExchangeService
    {
        public SortedSet<Order> BuyOrders { get; }
        public SortedSet<Order> SellOrders { get; }

        public StockExchangeService()
        {
            BuyOrders = new SortedSet<Order>(new BuyStockPriceComparer());
            SellOrders = new SortedSet<Order>(new SellStockPriceComparer());
        }

        public string Exchange()
        {
            string exchangeReceipt = "";
            while (BuyOrders.Count > 0 && SellOrders.Count > 0)
            {
                Order buyOrder = BuyOrders.First(), sellOrder = SellOrders.First();

                if (buyOrder.OrderPrice >= sellOrder.OrderPrice)
                {
                    exchangeReceipt = $"{buyOrder.Id}  {sellOrder.OrderPrice} {GetUpdatedQuantity(buyOrder.OrderQuantity, sellOrder.OrderQuantity)} {sellOrder.Id}.";

                    if (buyOrder.OrderQuantity > sellOrder.OrderQuantity)
                    {
                        buyOrder.OrderQuantity = buyOrder.OrderQuantity - sellOrder.OrderQuantity;
                        SellOrders.Remove(sellOrder);
                    }
                    else
                    {
                        sellOrder.OrderQuantity = sellOrder.OrderQuantity - buyOrder.OrderQuantity;
                        BuyOrders.Remove(buyOrder);
                    }
                }
                else
                    break;
            }
            return exchangeReceipt;
        }

        private int GetUpdatedQuantity(int buyOrderQuantity, int sellOrderQuantity)
        {
            return (buyOrderQuantity <= sellOrderQuantity ? buyOrderQuantity : sellOrderQuantity);
        }
    }
}
