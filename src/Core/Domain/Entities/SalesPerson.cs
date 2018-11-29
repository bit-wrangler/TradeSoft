namespace Domain
{
    public class SalesPerson
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as SalesPerson;
            if (other == null) return false;
            return this.Name.Equals(other.Name);
        }
    }
}