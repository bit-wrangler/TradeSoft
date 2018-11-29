using Domain;

namespace Application.Models
{
    public class SalesPersonAUMSummary
    {
        public SalesPerson SalesPerson { get; set; }
        public double Amount { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as SalesPersonAUMSummary;
            if (other == null) return false;
            return this.SalesPerson.Equals(other.SalesPerson) &&
                    this.Amount.Equals(other.Amount);
        }

        public override int GetHashCode()
        {
            return this.SalesPerson.GetHashCode();
        }
    }
}