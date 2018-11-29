using System;
using System.Collections.Generic;
using System.Transactions;

namespace Domain.Repositories
{
    public interface ITransactionRepository
    {
        List<Transaction> GetAll();
    }
}