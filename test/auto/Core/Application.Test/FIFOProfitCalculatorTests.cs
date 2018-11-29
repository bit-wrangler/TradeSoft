using NUnit.Framework;
using Application.Utilities;
using System;
using Application.Models;
using Domain;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class FIFOProfitCalculatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CheckEmptyInputListsBoth()
        {
            var purchases = new List<Transaction>();
            var sales = new List<Transaction>();
            Assert.AreEqual(0, new FIFOProfitCalculator(purchases, sales).Profit);
        }

        [Test]
        public void CheckEmptyInputListsPurchases()
        {
            var purchases = new List<Transaction>();
            var sales = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 1
                }
            };
            Assert.AreEqual(0, new FIFOProfitCalculator(purchases, sales).Profit);
        }

        [Test]
        public void CheckEmptyInputListsSales()
        {
            var sales = new List<Transaction>();
            var purchases = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 1
                }
            };
            Assert.AreEqual(0, new FIFOProfitCalculator(purchases, sales).Profit);
        }

        [Test]
        public void CheckBothListsWithEqualTransactions()
        {
            var sales = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 1
                }
            };
            var purchases = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 1
                }
            };
            Assert.AreEqual(0, new FIFOProfitCalculator(purchases, sales).Profit);
        }

        [Test]
        public void CheckBothListsWithUnequalTransactionsDifferentNumbers()
        {
            var sales = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 1
                }
            };
            var purchases = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 5,
                    PricePerShare = 1
                }
            };
            Assert.AreEqual(0, new FIFOProfitCalculator(purchases, sales).Profit);
        }

        [Test]
        public void CheckBothListsWithUnequalTransactinosDifferentPrices()
        {
            var sales = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 1
                }
            };
            var purchases = new List<Transaction>{
                new Transaction{
                    NumberOfShares = 10,
                    PricePerShare = 2
                }
            };
            Assert.AreEqual((double)1m*10.0 - (double)2m*10.0, new FIFOProfitCalculator(purchases, sales).Profit);
        }
    }
}