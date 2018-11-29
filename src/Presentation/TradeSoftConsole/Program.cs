using System;
using System.IO;
using Domain;
using Application;
using CSVTransactionRepository;


namespace TradeSoftConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("Must supply a path to the transactions CSV file.");
                return;
            }

            var filePath = args[0];

            if (!File.Exists(filePath))
            {
                System.Console.WriteLine("file doesn't exist");
                return;
            }

            DateTime endDate = new DateTime(2018, 4, 20);

            var repo = new TransactionRepository(filePath);
            var reporter = new TransactionReporter(repo);

            System.Console.WriteLine("--------------Sales Report--------------");
            System.Console.WriteLine(
                    $"{"Sales Person",25} | " +
                    $"{"Year-To-Date",16} | " +
                    $"{"Month-To-Date",16} | " +
                    $"{"Quarter-To-Date",16} | " +
                    $"{"Inception-To-Date",18}"
                );
            var salesReport = reporter.SalesSummary(endDate);
            foreach (var item in salesReport)
            {
                System.Console.WriteLine(
                    $"{item.SalesPerson.Name,25} | " +
                    $"{item.YearToDate,16:C} | " +
                    $"{item.MonthToDate,16:C} | " +
                    $"{item.QuarterToDate,16:C} | " +
                    $"{item.InceptionToDate,18:C}"
                );
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("-----------Assets Under Management-----------");
            System.Console.WriteLine(
                    $"{"Sales Person",25} | " +
                    $"{"Amount",16}"
                );
            var AUMS = reporter.AssetsUnderManagementSummary(endDate);
            foreach (var item in AUMS)
            {
                System.Console.WriteLine(
                    $"{item.SalesPerson.Name,25} | " +
                    $"{item.Amount,16:C}"
                );
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("-------------Break Report----------------");
            System.Console.WriteLine(
                    $"{"Investor",25} | " +
                    $"{"Fund",16} | " +
                    $"{"Shares",16}"
                );
            var breakReport = reporter.InvestorBreakReport(endDate);
            foreach (var item in breakReport)
            {
                System.Console.WriteLine(
                    $"{item.Investor.Name,25} | " +
                    $"{item.Fund.Name,16} | " +
                    $"{item.ShareImbalance,16}"
                );
            }

            System.Console.WriteLine();
            System.Console.WriteLine();
            System.Console.WriteLine("-------------Profit Report----------------");
            System.Console.WriteLine(
                    $"{"Investor",30} | " +
                    $"{"Fund",16} | " +
                    $"{"Net Profit",16}"
                );
            var profitReport = reporter.InvestorProfitReport(endDate);
            foreach (var item in profitReport)
            {
                System.Console.WriteLine(
                    $"{item.Investor.Name,30} | " +
                    $"{item.Fund.Name,16} | " +
                    $"{item.Profit,16:C}"
                );
            }
        }
    }
}
