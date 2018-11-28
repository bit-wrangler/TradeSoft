using System;

namespace Domain
{
    public class Transaction
    {
        public enum TransactionType
        {
            BUY,
            SELL
        }
        public DateTime Date { get; set; }
        public TransactionType Type { get; set; }
        public int NumberOfShares { get; set; }
        public decimal Price { get; set; }
        public Fund Fund { get; set; }
        public Investor Investor { get; set; }
        public SalesPerson SalesPerson { get; set; }

    }
}
