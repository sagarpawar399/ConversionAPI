namespace ConversionAPI.Model
{
    public class LengthUnitFactor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Factor { get; set; }

        public List<string>? Values
        {
            get
            {
                if (!string.IsNullOrEmpty(Name))
                    return Name.Split(',').ToList();
                else return null;
            }
        }
    }
}
