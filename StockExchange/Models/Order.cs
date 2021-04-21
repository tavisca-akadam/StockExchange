using System;
using System.Collections.Generic;
using System.Text;

namespace StockExchange.Models
{
    public class Order
    {
        public Order(){}

        public Order(string stockOrderId, string time, string stockName, string stockType, string stockPrice, string stockQuantity)
        {
            this.Id = stockOrderId;
            this.OrderCreationTime = DateTime.Parse(time);
            this.StockName = stockName;
            this.OrderType = (OrderType)Enum.Parse(typeof(OrderType), stockType, true);
            this.OrderPrice = double.Parse(stockPrice);
            this.OrderQuantity = Int32.Parse(stockQuantity);
        }
        public Order(Order order)
        {
            this.Id = order.Id;
            this.OrderCreationTime = order.OrderCreationTime;
            this.StockName = order.StockName;
            this.OrderType = order.OrderType;
            this.OrderPrice = order.OrderPrice;
            this.OrderQuantity = order.OrderQuantity;
            
        }
        public string Id { get; set; }
        public DateTime OrderCreationTime { get; set; }
        public string StockName { get; set; }
        public OrderType OrderType { get; set; }
        public double OrderPrice { get; set; }
        public int OrderQuantity { get; set; }
    }
}
