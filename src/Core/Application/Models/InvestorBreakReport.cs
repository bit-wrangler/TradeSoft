using Domain;

namespace Application.Models
{
    public class InvestorBreakReport
    {
        public Investor Investor { get; set; }
        public double ShareImbalance { get; set; }
        public double CashImbalance { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as InvestorBreakReport;
            if (other == null) return false;
            return this.Investor.Equals(other.Investor) &&
                    this.ShareImbalance.Equals(other.ShareImbalance) &&
                    this.CashImbalance.Equals(other.CashImbalance);
        }

        public override int GetHashCode()
        {
            return this.Investor.GetHashCode() ^
                    this.ShareImbalance.GetHashCode() ^
                    this.CashImbalance.GetHashCode();
        }
    }
}