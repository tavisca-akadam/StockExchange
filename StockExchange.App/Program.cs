using StockExchange.Service;
using System;
using System.IO;

namespace StockExchange.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            StockTradingService trade = new StockTradingService();
            //var fileData = File.ReadAllLines("input/input.txt");
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(@"input/input.txt");
            while ((line = file.ReadLine()) != null)
            {
                trade.AddStockDetails(line);
            }

           trade.GenerateReceipt();

            file.Close();

            //File.WriteAllLines("../../../output/output.txt", fileData);

        }
    }
}
