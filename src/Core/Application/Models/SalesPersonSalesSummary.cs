using Domain;

namespace Application.Models
{
    public class SalesPersonSalesSummary
    {
        public SalesPerson SalesPerson { get; set; }
        public double YearToDate { get; set; }
        public double MonthToDate { get; set; }
        public double QuarterToDate { get; set; }
        public double InceptionToDate { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as SalesPersonSalesSummary;
            if (other == null) return false;
            return this.SalesPerson.Equals(other.SalesPerson) &&
                    this.YearToDate.Equals(other.YearToDate) &&
                    this.MonthToDate.Equals(other.MonthToDate) &&
                    this.QuarterToDate.Equals(other.QuarterToDate) &&
                    this.InceptionToDate.Equals(other.InceptionToDate);
        }
    }
}