using Domain;

namespace Application.Models
{
    public class InvestorProfit
    {
        public Investor Investor { get; set; }
        public Fund Fund { get; set; }
        public double Profit { get; set; }
    }
}