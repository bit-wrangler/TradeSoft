using NUnit.Framework;
using Application.Utilities;
using Domain.Repositories;
using System;
using System.Linq;
using Domain;
using System.Collections.Generic;
using Application;
using Application.Models;

namespace Tests
{
    public class TestRepo : ITransactionRepository
    {
        public static readonly SalesPerson SalesPerson = new SalesPerson{Name = "salesPerson"};
        public static readonly double BuyTransactionSum = (double)1m * 10.0 + (double)1m * 10.0;
        public static readonly double NetTransactionSum = (double)1m * 10.0 + (double)1m * 10.0 - (double)1m * 10.0;
        private static readonly List<Transaction> dataSet = new List<Transaction>{
            new Transaction{
                Date = new DateTime(2018,1,1),
                Fund = new Fund{Name = "fund"},
                Investor = new Investor{Name = "investor"},
                NumberOfShares = 10,
                PricePerShare = 1,
                SalesPerson = SalesPerson,
                Type = Transaction.TransactionType.BUY
            },
            new Transaction{
                Date = new DateTime(2018,1,1),
                Fund = new Fund{Name = "fund"},
                Investor = new Investor{Name = "investor"},
                NumberOfShares = 10,
                PricePerShare = 1,
                SalesPerson = SalesPerson,
                Type = Transaction.TransactionType.BUY
            },
            new Transaction{
                Date = new DateTime(2018,1,1),
                Fund = new Fund{Name = "fund"},
                Investor = new Investor{Name = "investor"},
                NumberOfShares = 10,
                PricePerShare = 1,
                SalesPerson = SalesPerson,
                Type = Transaction.TransactionType.SELL
            }
        };
        public List<Transaction> GetAll()
        {
            return dataSet;
        }

    }

    [TestFixture]
    public class TransactionReporterTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckSingleSalesPersonSummaryWithNoTransactionsInDateRange()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(
                new List<SalesPersonSalesSummary>(),
                reporter.SalesSummary(new DateTime(2017,12,31))
            );
        }

        [Test]
        public void CheckSingleSalesPersonSummarySameMonth()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(new List<SalesPersonSalesSummary>{
            new SalesPersonSalesSummary
            {
                SalesPerson = TestRepo.SalesPerson,
                InceptionToDate = TestRepo.BuyTransactionSum,
                MonthToDate = TestRepo.BuyTransactionSum,
                QuarterToDate = TestRepo.BuyTransactionSum,
                YearToDate = TestRepo.BuyTransactionSum,
            }
            }, reporter.SalesSummary(new DateTime(2018,1,1)));
        }

        [Test]
        public void CheckSingleSalesPersonSummaryLastMonth()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(new List<SalesPersonSalesSummary>{
            new SalesPersonSalesSummary
            {
                SalesPerson = TestRepo.SalesPerson,
                InceptionToDate = TestRepo.BuyTransactionSum,
                MonthToDate = 0,
                QuarterToDate = TestRepo.BuyTransactionSum,
                YearToDate = TestRepo.BuyTransactionSum,
            }
            }, reporter.SalesSummary(new DateTime(2018,2,1)));
        }

        [Test]
        public void CheckSingleSalesPersonSummaryLastQuarter()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(new List<SalesPersonSalesSummary>{
            new SalesPersonSalesSummary
            {
                SalesPerson = TestRepo.SalesPerson,
                InceptionToDate = TestRepo.BuyTransactionSum,
                MonthToDate = 0,
                QuarterToDate = 0,
                YearToDate = TestRepo.BuyTransactionSum,
            }
            }, reporter.SalesSummary(new DateTime(2018,4,1)));
        }

        [Test]
        public void CheckSingleSalesPersonSummaryLastYear()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(new List<SalesPersonSalesSummary>{
            new SalesPersonSalesSummary
            {
                SalesPerson = TestRepo.SalesPerson,
                InceptionToDate = TestRepo.BuyTransactionSum,
                MonthToDate = 0,
                QuarterToDate = 0,
                YearToDate = 0,
            }
            }, reporter.SalesSummary(new DateTime(2019,1,1)));
        }

        [Test]
        public void CheckSingleSalesPersonAUMSummary()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(new List<SalesPersonAUMSummary>{
            new SalesPersonAUMSummary
            {
                SalesPerson = TestRepo.SalesPerson,
                Amount = TestRepo.NetTransactionSum
            }
            }, reporter.AssetsUnderManagementSummary(new DateTime(2018,1,1)));
        }

        [Test]
        public void CheckSingleSalesPersonAUMSummaryWithNoTransactionsInDateRange()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(
                new List<SalesPersonAUMSummary>(),
                reporter.AssetsUnderManagementSummary(new DateTime(2017,1,1))
            );
        }

    }
}