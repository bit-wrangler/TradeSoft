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
            var buyTransactions = this.transactionRepository.GetAll().Where(t => t.Type == Transaction.TransactionType.BUY);
            var groupedBySalesRep = buyTransactions.GroupBy(t => t.SalesPerson.Name);
            var report = new List<SalesPersonSalesSummary>();

            return groupedBySalesRep.Select(g => new SalesPersonSalesSummary{
                SalesPerson = g.First().SalesPerson,
                InceptionToDate = g.Sum(t => t.TransactionTotal),
                MonthToDate = g.Where(t => t.Date >= ToDatePeriodUtility.StartOfMonth(endDate)).Sum(t => t.TransactionTotal),
                QuarterToDate = g.Where(t => t.Date >= ToDatePeriodUtility.StartOfQuarter(endDate)).Sum(t => t.TransactionTotal),
                YearToDate = g.Where(t => t.Date >= ToDatePeriodUtility.StartOfYear(endDate)).Sum(t => t.TransactionTotal)
            }).ToList();
        }
    }
}
