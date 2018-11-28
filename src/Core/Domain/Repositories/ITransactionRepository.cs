using System;
using System.Collections.Generic;
using System.Transactions;

namespace Domain.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
        List<Transaction> GetAllWhere(Func<Transaction,bool> filter);
    }
}