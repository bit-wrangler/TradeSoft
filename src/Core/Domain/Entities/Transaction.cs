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
        public double NumberOfShares { get; set; }
        public decimal PricePerShare { get; set; }
        public Fund Fund { get; set; }
        public Investor Investor { get; set; }
        public SalesPerson SalesPerson { get; set; }

    }
}
