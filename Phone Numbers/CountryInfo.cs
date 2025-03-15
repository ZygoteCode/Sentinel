namespace PhoneNumbersLib
{
    public class CountryInfo
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public string StringPrefix { get; set; }
        public ushort NumericPrefix { get; set; }

        public CountryInfo(string country, string countryCode, string stringPrefix, ushort numericPrefix)
        {
            Country = country;
            CountryCode = countryCode;
            StringPrefix = stringPrefix;
            NumericPrefix = numericPrefix;
        }

        public string GetCountry()
        {
            return Country;
        }

        public string GetCountryCode()
        {
            return CountryCode;
        }

        public string GetStringPrefix()
        {
            return StringPrefix;
        }

        public ushort GetNumericPrefix()
        {
            return NumericPrefix;
        }
    }
}