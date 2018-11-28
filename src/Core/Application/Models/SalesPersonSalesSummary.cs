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
    }
}