using System;
using System.Collections.Generic;
using Domain;
using Domain.Repositories;

namespace CSVTransactionRepository
{
    public class TransactionRepository : ITransactionRepository
    {
        public List<Transaction> GetAll()
        {
            return new List<Transaction>();
        }

        public List<Transaction> GetAllWhere(Func<Transaction, bool> filter)
        {
            return new List<Transaction>();
        }
    }
}
