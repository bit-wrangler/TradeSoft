using System;
using System.Collections.Generic;
using System.Linq;
using Application.Models;
using Application.Utilities;
using Domain;
using Domain.Repositories;

namespace Application
{
    public class TransactionReporter
    {
        private readonly ITransactionRepository transactionRepository;

        public TransactionReporter(ITransactionRepository transactionRepository)
        {
            this.transactionRepository = transactionRepository;
        }

        public List<SalesPersonSalesSummary> SalesSummary(DateTime endDate)
        {
            var buyTransactionsBySalesRep = this.transactionRepository.GetAll()
                .Where(t => t.Date <= endDate)
                .Where(t => t.Type == Transaction.TransactionType.BUY)
                .GroupBy(t => t.SalesPerson);

            return buyTransactionsBySalesRep.Select(g => new SalesPersonSalesSummary
            {
                SalesPerson = g.Key,
                InceptionToDate = g.Sum(t => t.TransactionTotal),
                MonthToDate = g.Where(t => t.Date >= ToDatePeriodUtility.StartOfMonth(endDate)).Sum(t => t.TransactionTotal),
                QuarterToDate = g.Where(t => t.Date >= ToDatePeriodUtility.StartOfQuarter(endDate)).Sum(t => t.TransactionTotal),
                YearToDate = g.Where(t => t.Date >= ToDatePeriodUtility.StartOfYear(endDate)).Sum(t => t.TransactionTotal)
            }).ToList();
        }

        public List<SalesPersonAUMSummary> AssetsUnderManagementSummary(DateTime endDate)
        {
            var transactionsBySalesRep = this.transactionRepository.GetAll()
                .Where(t => t.Date <= endDate)
                .GroupBy(t => t.SalesPerson);

            return transactionsBySalesRep.Select(g => new SalesPersonAUMSummary
            {
                SalesPerson = g.Key,
                Amount = g.Sum(t => t.TransactionTotal * ((t.Type == Transaction.TransactionType.BUY) ? 1 : -1))
            }).ToList();
        }

        public List<InvestorShareImbalance> InvestorBreakReport(DateTime endDate)
        {
            var transactionsByInvestor = this.transactionRepository.GetAll()
                .Where(t => t.Date <= endDate)
                .GroupBy(t => t.Investor);

            // loop over each investor's set of transactions
            // select many allows us to automatically concatenate the resulting lists
            return transactionsByInvestor.SelectMany(
                g =>
                {
                    var transactionsByFund = g.GroupBy(t => t.Fund);
                    System.Console.WriteLine(g.Key.Name);
                    // loop over each investor's set of transactions for each fund
                    return transactionsByFund.Select(
                        // for each set of transactions related to a particular, calculate the balance
                        f =>
                        {
                            System.Console.WriteLine(f.Key.Name);
                            return new
                            {
                                Fund = f.Key,
                                Balance = f.Sum(t => t.NumberOfShares * ((t.Type == Transaction.TransactionType.BUY) ? 1 : -1))
                            };
                        }
                    ).Where(b => b.Balance < 0) // keep only balances that are negative
                    .Select(b => new InvestorShareImbalance
                    {
                        Investor = g.Key,
                        Fund = b.Fund,
                        ShareImbalance = -b.Balance
                    }).ToList();
                }
            ).ToList();
        }

        // public List<InvestorProfit> InvestorProfitReport(DateTime endDate)
        // {
        //     // to calculate profits we first need to find cases where
        //     // shares are sold, then calculate profit based on FIFO approach
        //     var transactionsByInvestor = this.transactionRepository.GetAll()
        //         .OrderBy(t => t.Date)
        //         .Where(t => t.Date <= endDate)
        //         .GroupBy(t => t.Investor);

        //     var investorFundSummaries = transactionsByInvestor.SelectMany(i => {
        //         var transactionsByFund = i.GroupBy(t => t.Fund);

        //         return transactionsByFund.Select(f =>  new InvestorFundSummary{
        //                 Investor = i.Key,
        //                 Fund = f.Key,
        //                 Purchases = new Queue<Transaction>(f.Where(t => t.Type == Transaction.TransactionType.BUY)),
        //                 Sales = new Queue<Transaction>(f.Where(t => t.Type == Transaction.TransactionType.SELL))
        //             }
        //         ).Where(s => s.Sales.Count > 0);
        //     }).ToList();

        //     foreach (var summary in investorFundSummaries)
        //     {
                
        //     }
        // }
    }
}
