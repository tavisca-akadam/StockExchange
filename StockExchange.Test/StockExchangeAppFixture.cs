using StockExchange.Models;
using StockExchange.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace StockExchange.Test
{
    public class StockExchangeAppFixture
    {
        [Fact]
        public void AddStockDetails_AddSellOrderEntry_Test_ReturnSuccess()
        {
            StockTradingService tradingService = new StockTradingService();

            string stockInfo = "#1 09:45 BAC sell 240.12 100";

            var order = tradingService.AddStockDetails(stockInfo);

            Assert.NotNull(order);
            Assert.Equal("#1", order.Id);
            Assert.Equal("BAC", order.StockName);
            Assert.Equal("Sell", order.OrderType.ToString());
            Assert.Equal("240.12", order.OrderPrice.ToString());
            Assert.Equal("100", order.OrderQuantity.ToString());
        }

        [Fact]
        public void AddStockDetails_AddBuyOrderEntry_Test_ReturnSuccess()
        {
            StockTradingService tradingService = new StockTradingService();

            string stockInfo = "#3 09:48 BAC buy 237.80 10";

            var order = tradingService.AddStockDetails(stockInfo);

            Assert.NotNull(order);
            Assert.Equal("#3", order.Id);
            Assert.Equal("BAC", order.StockName);
            Assert.Equal("Buy", order.OrderType.ToString());
            Assert.Equal("237.80", order.OrderPrice.ToString());
            Assert.Equal("10", order.OrderQuantity.ToString());
        }

        [Fact]
        public void DoTrade_WithValidOrder_ReturnSuccess()
        {
            StockTradingService tradingService = new StockTradingService();
            Order order;
            string stockInfo = "#1 09:48 BAC sell 237.80 10";
            order = tradingService.AddStockDetails(stockInfo);
            stockInfo = "#2 09:49 BAC buy 237.80 10";
            order = tradingService.AddStockDetails(stockInfo);

            var output = tradingService.DoTrade(order);
            Assert.NotNull(output);
        }
        [Fact]
        public void DoTrade_WithValidOrder_ChangeInSequence_ReturnSuccess()
        {
            StockTradingService tradingService = new StockTradingService();
            Order order;
            string stockInfo = "#1 09:48 BAC buy 237.80 10";
            order = tradingService.AddStockDetails(stockInfo);
            stockInfo = "#2 09:49 BAC sell 237.80 10";
            order = tradingService.AddStockDetails(stockInfo);

            var output = tradingService.DoTrade(order);
            Assert.NotNull(output);
        }

        [Fact]
        public void DoTradeTest_With_NullOrder_ResturnFailure()
        {
            StockTradingService tradingService = new StockTradingService();
            Order order = null;

            var exception = Assert.Throws<InvalidOperationException>(() => tradingService.DoTrade(order));
            Assert.NotNull(exception);
            Assert.Equal("Order can not null or empty.", exception.Message);
        }

        [Fact]
        public void DoTradeTest_With_InvalidStockOrder_ReturnFailure()
        {
            StockTradingService tradingService = new StockTradingService();
            Order order;
            string stockInfo = "#1 09:48 BAC buy 237.80 10";
            order = tradingService.AddStockDetails(stockInfo);

            var invalidOrder = GetInvalidOrderData();

            var exception = Assert.Throws<KeyNotFoundException>(() => tradingService.DoTrade(invalidOrder));
            Assert.NotNull(exception);
            Assert.Equal("Stock order not found.", exception.Message);
        }

        private Order GetInvalidOrderData()
        {
            return new Order
            {
                Id = "#3",
                OrderCreationTime = DateTime.Now,
                StockName = "Test",
                OrderPrice = 500,
                OrderQuantity = 1000,
                OrderType = OrderType.Sell
            };
        }
    }
}