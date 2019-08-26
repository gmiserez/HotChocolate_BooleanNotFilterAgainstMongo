namespace Demo
{
    public class City
    {
        public City(
            int id, 
            string name, 
            string countryCode, 
            bool isCapitalCity)
        {
            Id = id;
            Name = name;
            CountryCode = countryCode;
            IsCapitalCity = isCapitalCity;
        }

        public int Id { get; }
        public string Name { get; }
        public string CountryCode { get; }
        public bool IsCapitalCity { get; }
    }
}
