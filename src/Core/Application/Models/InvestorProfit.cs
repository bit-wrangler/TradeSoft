using Domain;

namespace Application.Models
{
    public class InvestorProfit
    {
        public Investor Investor { get; set; }
        public Fund Fund { get; set; }
        public double Profit { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as InvestorProfit;
            if (other == null) return false;
            return this.Investor.Equals(other.Investor) &&
                    this.Fund.Equals(other.Fund) &&
                    this.Profit.Equals(other.Profit);
        }

        public override int GetHashCode()
        {
            return this.Investor.GetHashCode() ^ this.Fund.GetHashCode();
        }
    }
}