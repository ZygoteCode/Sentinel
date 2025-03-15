namespace PhoneNumbersLib
{
    public class PhoneNumber
    {
        public string Country { get; set; }
        public string CountryCode { get; set; }
        public ulong Number { get; set; }
        public ushort Prefix { get; set; }

        public PhoneNumber(string country, string countryCode, ulong number, ushort prefix)
        {
            Country = country;
            CountryCode = countryCode;
            Number = number;
            Prefix = prefix;
        }

        public PhoneNumber(string phoneNumber)
        {
            try
            {
                PhoneNumber data = PhoneNumbers.GetPhoneNumberInfo(phoneNumber);
                Country = data.Country;
                CountryCode = data.CountryCode;
                Number = data.Number;
                Prefix = data.Prefix;
            }
            catch
            {

            }
        }

        public string GetCountry()
        {
            return Country;
        }

        public void SetCountry(string country)
        {
            Country = country;
        }

        public string GetCountryCode()
        {
            return CountryCode;
        }

        public void SetCountryCode(string countryCode)
        {
            CountryCode = CountryCode;
        }

        public ulong GetNumber()
        {
            return Number;
        }

        public void SetNumber(ulong number)
        {
            Number = number;
        }

        public ushort GetPrefix()
        {
            return Prefix;
        }

        public void SetPrefix(ushort prefix)
        {
            Prefix = prefix;
        }

        public override string ToString()
        {
            return $"+{Prefix}{Number}";
        }

        public string GetPossibleItalianOperator()
        {
            if (Country != "Italy" || CountryCode != "IT" || Prefix != 39)
            {
                return null;
            }

            ushort _prefix = ushort.Parse(Number.ToString()[0].ToString() + Number.ToString()[1].ToString() + Number.ToString()[2].ToString());
            byte _first = byte.Parse(Number.ToString()[3].ToString());
            byte _second = byte.Parse(Number.ToString()[4].ToString());

            if ((_prefix == 331 && _first == 1) || (_prefix == 370 && _first == 3) || (_prefix == 353 && _first == 4))
            {
                return "CoopVoce";
            }
            else if (_prefix == 330 || _prefix == 331 || _prefix == 333 || _prefix == 334 || _prefix == 335 || _prefix == 336 || _prefix == 337 || _prefix == 338 || _prefix == 339 || _prefix == 360 || _prefix == 366 || _prefix == 368)
            {
                return "TIM";
            }
            else if (_prefix == 340 || _prefix == 342 || _prefix == 344 || _prefix == 345 || _prefix == 346 || _prefix == 347 || _prefix == 348 || _prefix == 349)
            {
                return "Vodafone Italia";
            }
            else if (_prefix == 320 || _prefix == 324 || _prefix == 327 || _prefix == 328 || _prefix == 329 || _prefix == 380 || _prefix == 388 || _prefix == 389 || _prefix == 391 || _prefix == 392 || _prefix == 393)
            {
                return "Wind Tre / Very Mobile";
            }
            else if ((_prefix == 352 && (_first == 0 || _first == 2)) || (_prefix == 351 && (_first >= 3 && _first <= 9)))
            {
                return "Iliad";
            }
            else if (_prefix == 378 && _first == 3)
            {
                return "Vianova";
            }
            else if (_prefix == 370 && _first == 1)
            {
                return "Tiscali Mobile";
            }
            else if (_prefix == 378 && _first == 0)
            {
                return "spusu";
            }
            else if (_prefix == 375 && (_first >= 5 && _first <= 8))
            {
                return "Fastweb";
            }
            else if (_prefix == 379 && (_first == 1 || _first == 2))
            {
                return "ho. Mobile";
            }
            else if (_prefix == 377 && _first == 0 && _second == 8)
            {
                return "ho. Mobile";
            }
            else if (_prefix == 377 && _first == 0 && _second == 9)
            {
                return "ho. Mobile";
            }
            else if (_prefix == 350 && (_first == 0 || _first == 1 || _first == 5))
            {
                return "Kena Mobile";
            }
            else if (_prefix == 377 && (_first == 2 || _first == 4 || _first == 6 || _first == 9))
            {
                return "PosteMobile";
            }
            else if (_prefix == 371 && (_first == 1 || _first == 3 || _first == 4 || _first == 5 || _first == 6 || _first == 7))
            {
                return "PosteMobile";
            }
            else if (_prefix == 377 && _first == 0)
            {
                return "Optima Mobile";
            }

            return null;
        }
    }
}