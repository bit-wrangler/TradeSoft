using System;
using CSVTransactionRepository;

namespace CSVCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "Data.csv";
            var repo = new TransactionRepository(filePath);
            foreach (var t in repo.GetAll())
            {
                System.Console.WriteLine(
                    $"{t.Date}, {t.Type}, {t.NumberOfShares}, {t.PricePerShare}, {t.Fund.Name} "
                );
            }
        }
    }
}
