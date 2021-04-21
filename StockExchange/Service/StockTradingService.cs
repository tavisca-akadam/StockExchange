using StockExchange.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchange.Service
{
    public class StockTradingService
    {
        private Dictionary<string, StockOrderMatching> _stockMatchingMap;

        public StockTradingService()
        {
            _stockMatchingMap = new Dictionary<string, StockOrderMatching>();
        }

        public void AddStockDetails(string stockDetail)
        {
            var stockInfo = stockDetail.Split(' ');
            var (stockOrderId, time, stockName, stockType, stockPrice, stockQuantity) =
                (stockInfo[0], stockInfo[1], stockInfo[2], stockInfo[3], stockInfo[4], stockInfo[5]);

            Order order = new Order(stockOrderId, time, stockName, stockType, stockPrice, stockQuantity);

            if(_stockMatchingMap.ContainsKey(stockName))
            {
                StockOrderMatching stockOrder = _stockMatchingMap[stockName];
                if (order.OrderType == OrderType.Buy)
                {
                    stockOrder.BuyOrders.Add(order);
                    stockOrder.GenerateReceiptForStock();
                }
                else
                    stockOrder.SellOrders.Add(order);
            }
            else
            {
                StockOrderMatching stockOrder = new StockOrderMatching();
                if (order.OrderType == OrderType.Buy)
                {
                    stockOrder.BuyOrders.Add(order);
                    stockOrder.GenerateReceiptForStock();
                }
                else
                    stockOrder.SellOrders.Add(order);

                _stockMatchingMap.Add(stockName, stockOrder);
            }

        }

        public void GenerateReceipt()
        {
            foreach(var stock in _stockMatchingMap)
            {
                var stockMatchingEntry = stock.Value;
                stockMatchingEntry.GenerateReceiptForStock();
            }
        }
    }
}
