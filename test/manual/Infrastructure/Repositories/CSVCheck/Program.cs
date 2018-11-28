using System;
using System.Linq;
using CSVTransactionRepository;

namespace CSVCheck
{
    class Program
    {
        static void Main(string[] args)
        {
            const string filePath = "Data.csv";
            var repo = new TransactionRepository(filePath);
            var grouped = repo.GetAll().GroupBy(t => t.SalesPerson.Name);
            System.Console.WriteLine(grouped.Count());
            foreach (var group in grouped)
            {
                System.Console.WriteLine(group.Key);
                foreach (var item in group.ToList())
                {
                    System.Console.WriteLine(item.Date);
                }
            }
        }
    }
}
