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

            return buyTransactionsBySalesRep.Select(g => new SalesPersonSalesSummary{
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

            return transactionsBySalesRep.Select(g => new SalesPersonAUMSummary{
                SalesPerson = g.Key,
                Amount = g.Sum(t => t.TransactionTotal * ((t.Type == Transaction.TransactionType.BUY) ? 1 : -1))
            }).ToList();
        }
    }
}
