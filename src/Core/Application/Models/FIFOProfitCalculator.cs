using System.Collections.Generic;
using Domain;

namespace Application.Models
{
    public class FIFOProfitCalculator
    {
        public double Profit { get; private set; }
        public FIFOProfitCalculator(List<Transaction> purchases, List<Transaction> sales)
        {
            var purchasesQueue = new Queue<Transaction>(purchases);
            var salesQueue = new Queue<Transaction>(sales);
            this.Profit = 0;
            while (purchasesQueue.Count > 0 && salesQueue.Count > 0)
            {
                this.Profit += consumeQueues(purchasesQueue, salesQueue);
            }
        }

        private static double consumeQueues(Queue<Transaction> purchases, Queue<Transaction> sales)
        {
            double nSharesToConsume = 0;
            double buyPrice = (double)purchases.Peek().PricePerShare;
            double sellPrice = (double)sales.Peek().PricePerShare;
            if (purchases.Peek().NumberOfShares == sales.Peek().NumberOfShares)
            {
                nSharesToConsume = purchases.Peek().NumberOfShares;
                purchases.Dequeue();
                sales.Dequeue();
            }
            else if (purchases.Peek().NumberOfShares < sales.Peek().NumberOfShares)
            {
                nSharesToConsume = purchases.Peek().NumberOfShares;
                purchases.Dequeue();
                sales.Peek().NumberOfShares -= nSharesToConsume;
            }
            else
            {
                nSharesToConsume = sales.Peek().NumberOfShares;
                sales.Dequeue();
                purchases.Peek().NumberOfShares -= nSharesToConsume;
            }
            return nSharesToConsume * (sellPrice - buyPrice);
        }
    }
}
