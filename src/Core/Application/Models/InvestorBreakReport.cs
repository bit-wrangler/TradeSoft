using Domain;

namespace Application.Models
{
    public class InvestorBreakReport
    {
        public Investor Investor { get; set; }
        public Fund Fund { get; set; }
        public double ShareImbalance { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as InvestorBreakReport;
            if (other == null) return false;
            return this.Investor.Equals(other.Investor) &&
                    this.Fund.Equals(other.Fund) &&
                    this.ShareImbalance.Equals(other.ShareImbalance);
        }

        public override int GetHashCode()
        {
            return this.Investor.GetHashCode() ^
                    this.Fund.GetHashCode();
        }
    }
}