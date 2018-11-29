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
        public static readonly double TransactionSum = (double)1m * 10.0 + (double)1m * 10.0;
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
        public void CheckSingleSalesPersonSummarySameMonth()
        {
            var repo = new TestRepo();
            var reporter = new TransactionReporter(repo);
            Assert.AreEqual(new List<SalesPersonSalesSummary>{
            new SalesPersonSalesSummary
            {
                SalesPerson = TestRepo.SalesPerson,
                InceptionToDate = TestRepo.TransactionSum,
                MonthToDate = TestRepo.TransactionSum,
                QuarterToDate = TestRepo.TransactionSum,
                YearToDate = TestRepo.TransactionSum,
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
                InceptionToDate = TestRepo.TransactionSum,
                MonthToDate = 0,
                QuarterToDate = TestRepo.TransactionSum,
                YearToDate = TestRepo.TransactionSum,
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
                InceptionToDate = TestRepo.TransactionSum,
                MonthToDate = 0,
                QuarterToDate = 0,
                YearToDate = TestRepo.TransactionSum,
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
                InceptionToDate = TestRepo.TransactionSum,
                MonthToDate = 0,
                QuarterToDate = 0,
                YearToDate = 0,
            }
            }, reporter.SalesSummary(new DateTime(2019,1,1)));
        }

    }
}