using System;
using System.Linq;
using System.Collections.Generic;
using Domain;
using Domain.Repositories;
using FileHelpers;
using System.IO;
using System.Text;
using System.Globalization;

namespace CSVTransactionRepository
{
    //https://stackoverflow.com/questions/25283510/ignore-dollar-currency-sign-in-decimal-field-with-filehelpers-library
    public class CurrencyConverter : ConverterBase
    {
        private NumberFormatInfo nfi = new NumberFormatInfo();

        public CurrencyConverter()
        {
            nfi.NegativeSign = "-";
            nfi.NumberDecimalSeparator = ".";
            nfi.NumberGroupSeparator = ",";
            nfi.CurrencySymbol = "$";
        }

        public override object StringToField(string from)
        {
            return decimal.Parse(from, NumberStyles.Currency, nfi);
        }
    }


    [DelimitedRecord(",")]
    [IgnoreFirst()]
    class CSVTransaction
    {
        [FieldConverter(ConverterKind.Date, "M/d/yy")]
        public DateTime Date { get; set; }
        public string Type { get; set; }

        public double NumberOfShares { get; set; }
        [FieldConverter(typeof(CurrencyConverter))]
        public decimal PricePerShare { get; set; }
        public string FundName { get; set; }

        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string InvestorName { get; set; }
        [FieldQuoted(QuoteMode.OptionalForRead)]
        public string SalesPersonName { get; set; }

        public Transaction getTransaction()
        {
            return new Transaction
            {
                Date = this.Date,
                Fund = new Fund { Name = this.FundName },
                Investor = new Investor { Name = this.InvestorName },
                NumberOfShares = this.NumberOfShares,
                PricePerShare = this.PricePerShare,
                SalesPerson = new SalesPerson { Name = this.SalesPersonName },
                Type = (this.Type.Equals("BUY")) ?
                        Transaction.TransactionType.BUY :
                        Transaction.TransactionType.SELL
            };
        }
    }
    public class TransactionRepository : ITransactionRepository
    {
        private List<Transaction> AllData;

        public TransactionRepository(string filePath)
        {
            this.AllData = new List<Transaction>();
            var engine = new FileHelperEngine<CSVTransaction>();
            this.AllData = engine.ReadFile(filePath)
                            .Select(t => t.getTransaction()).ToList();
        }
        public List<Transaction> GetAll()
        {
            return this.AllData;
        }

        public List<Transaction> GetAllWhere(Func<Transaction, bool> filter)
        {
            return this.AllData.Where(filter).ToList();
        }
    }
}
