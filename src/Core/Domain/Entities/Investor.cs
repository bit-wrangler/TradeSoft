namespace Domain
{
    public class Investor
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Investor;
            if (other == null) return false;
            return this.Name.Equals(other.Name);
        }
    }
}