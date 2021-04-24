using StockExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockExchange.Service
{
    public class StockTradingService
    {
        private Dictionary<string, StockExchangeService> _stockMatchingMap;

        public StockTradingService()
        {
            _stockMatchingMap = new Dictionary<string, StockExchangeService>();
        }

        public Order AddStockDetails(string stockDetail)
        {
            StockExchangeService stockOrder;
            Order order = ParseStockOrder(stockDetail);

            if(!_stockMatchingMap.ContainsKey(order.StockName))
            {
                stockOrder = new StockExchangeService();
                _stockMatchingMap.Add(order.StockName, stockOrder);
            }

            stockOrder = _stockMatchingMap[order.StockName];
            AddStockToOrderBook(stockOrder, order);         

            return order;
        }

        public string DoTrade(Order order)
        {
            if (order == null)
                throw new InvalidOperationException("Order can not null or empty.");
            try
            {
                var stockOrder = _stockMatchingMap[order.StockName];  
                return stockOrder.Exchange();
            }
            catch(Exception ex)
            {
                throw new KeyNotFoundException("Stock order not found.");
            }
        }

        private void AddStockToOrderBook(StockExchangeService stockOrderBook, Order order)
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
            var stockInfo = System.Text.RegularExpressions.Regex.Split(stockDetail, @"\s+"); ;
            var (stockOrderId, time, stockName, stockType, stockPrice, stockQuantity) =
                (stockInfo[0], stockInfo[1], stockInfo[2], stockInfo[3], stockInfo[4], stockInfo[5]);
            Order order = new Order(stockOrderId, time, stockName, stockType, stockPrice, stockQuantity);
            return order;
        }
    }
}
