using Domain;

namespace Application.Models
{
    public class InvestorBreakReport
    {
        public Investor Investor { get; set; }
        public double ShareImbalance { get; set; }
        public double CashImbalance { get; set; }
    }
}