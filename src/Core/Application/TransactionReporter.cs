using System;
using System.Linq;
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

        public void SalesSummary()
        {
            var today = DateTime.Now.Date;
            
            var buyTransactions = this.transactionRepository.GetAllWhere(t => t.Type == Transaction.TransactionType.BUY);
            var groupedBySalesRep = buyTransactions.GroupBy(t => t.SalesPerson.Name);
            foreach (var group in groupedBySalesRep)
            {
                group.Sum(t => t.TransactionTotal);
                group.Where(t => t.Date >= today - TimeSpan.FromDays(365)).Sum(t => t.TransactionTotal);
                group.Where(t => t.Date >= today - TimeSpan.FromDays(30)).Sum(t => t.TransactionTotal);
                group.Where(t => t.Date >= today - TimeSpan.FromDays(90)).Sum(t => t.TransactionTotal);
            }
        }
    }
}
