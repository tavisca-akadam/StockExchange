using StockExchange.Service;
using System;
using System.IO;
using System.Reflection;

namespace StockExchange.App
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = args[0];

            if (string.IsNullOrEmpty(fileName))
                throw new InvalidOperationException();

            FileStream fileStream = new FileStream(fileName, FileMode.Open);
            StockTradingService tradingService = new StockTradingService();
            string line;
            
            using(StreamReader reader = new StreamReader(fileStream))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    var order = tradingService.AddStockDetails(line);
                    try
                    {
                        var output = tradingService.DoTrade(order);
                        if (!string.IsNullOrEmpty(output))
                            Console.WriteLine(output);
                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }

            }

           fileStream.Close();
        }
    }
}
