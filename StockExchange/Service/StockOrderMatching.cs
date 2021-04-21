using StockExchange.Models;
using StockExchange.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchange.Service
{
    public class StockOrderMatching
    {
        public SortedSet<Order> BuyOrders { get; }
        public SortedSet<Order> SellOrders { get; }
        public string stockName = "";

        public StockOrderMatching()
        {
            BuyOrders = new SortedSet<Order>(new BuyStockPriceComparer());
            SellOrders = new SortedSet<Order>(new SellStockPriceComparer());
        }

        //public void GenerateReceiptForStock()
        //{
        //    IEnumerator<Order> buyOrdersItr = BuyOrders.GetEnumerator();
        //    IEnumerator<Order> sellOrdersItr = SellOrders.GetEnumerator();

        //    while(buyOrdersItr.MoveNext() && sellOrdersItr.MoveNext())
        //    {
        //        Order buyOrder = buyOrdersItr.Current, sellOrder = sellOrdersItr.Current;

        //        if (buyOrder.OrderPrice >= sellOrder.OrderPrice)
        //        {
        //            Console.WriteLine($"{buyOrder.Id} = {sellOrder.OrderPrice} = {buyOrder.OrderQuantity} = {sellOrder.Id}.");

        //            if (buyOrder.OrderQuantity > sellOrder.OrderQuantity)
        //            {
        //                Order order = new Order(buyOrder);
        //                order.OrderQuantity = buyOrder.OrderQuantity - sellOrder.OrderQuantity;
        //                BuyOrders.Add(order);
        //            }
        //        }
        //        else
        //            break;
        //    }
        //}

        public void GenerateReceiptForStock()
        {
            foreach (var buyOrder in BuyOrders.ToList())
            {
                foreach (var sellOrder in SellOrders.ToList())
                {
                    if (buyOrder.OrderPrice >= sellOrder.OrderPrice)
                    {
                        Console.WriteLine($"Sold {buyOrder.Id}  {sellOrder.OrderPrice} {(buyOrder.OrderQuantity <= sellOrder.OrderQuantity ? buyOrder.OrderQuantity : sellOrder.OrderQuantity)} {sellOrder.Id}.");

                        if (buyOrder.OrderQuantity > sellOrder.OrderQuantity)
                        {
                            buyOrder.OrderQuantity = buyOrder.OrderQuantity - sellOrder.OrderQuantity;
                            SellOrders.Remove(sellOrder);
                            break;
                        }
                        else
                        {
                            sellOrder.OrderQuantity = sellOrder.OrderQuantity - buyOrder.OrderQuantity;
                            BuyOrders.Remove(buyOrder);
                            break;
                        }
                    }
                    else
                        break;
                }
            }
        }
    }
}
