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
            StockOrderMatching stockOrder;
            Order order = ParseStockOrder(stockDetail);

            if(!_stockMatchingMap.ContainsKey(order.StockName))
            {
                stockOrder = new StockOrderMatching();
                _stockMatchingMap.Add(order.StockName, stockOrder);
            }

            stockOrder = _stockMatchingMap[order.StockName];
            AddStockToOrderBook(stockOrder, order);

            stockOrder.GenerateReceiptForStock();

        }

        public void GenerateReceipt()
        {
            foreach(var stock in _stockMatchingMap)
            {
                var stockMatchingEntry = stock.Value;
                stockMatchingEntry.GenerateReceiptForStock();
            }
        }

        public void AddStockToOrderBook(StockOrderMatching stockOrderBook, Order order)
        {
            if (order.OrderType == OrderType.Buy)
            {
                stockOrderBook.BuyOrders.Add(order);
                return;
            }
            stockOrderBook.SellOrders.Add(order);
        }
        private Order ParseStockOrder(string stockDetail)
        {
            var stockInfo = stockDetail.Split(' ');
            var (stockOrderId, time, stockName, stockType, stockPrice, stockQuantity) =
                (stockInfo[0], stockInfo[1], stockInfo[2], stockInfo[3], stockInfo[4], stockInfo[5]);
            Order order = new Order(stockOrderId, time, stockName, stockType, stockPrice, stockQuantity);
            return order;
        }
    }
}
