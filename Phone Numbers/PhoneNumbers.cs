using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Sentinel.Properties;
using System.Collections.Generic;
using System.Text;

namespace PhoneNumbersLib
{
    public class PhoneNumbers
    {
        private static char[] _characters = "0123456789".ToCharArray();
        private static dynamic _json = null;
        private static List<CountryInfo> _countryInfos = null;

        public static List<CountryInfo> GetCountryInfos()
        {
            if (_json == null)
            {
                LoadJSON();
            }

            if (_countryInfos == null)
            {
                LoadCountryInfos();
            }

            return _countryInfos;
        }

        public static PhoneNumber GetPhoneNumberInfo(string phoneNumber)
        {
            try
            {
                if (_json == null)
                {
                    LoadJSON();
                }

                if (_countryInfos == null)
                {
                    LoadCountryInfos();
                }

                phoneNumber = FilterString(phoneNumber);

                if (phoneNumber == "")
                {
                    return null;
                }

                ulong _number = 0;
                ushort _prefix = 0;
                string _country = "", _countryCode = "";

                foreach (dynamic data in _json)
                {
                    string prefix = data.phone;

                    if (data.phoneLength is JArray)
                    {
                        foreach (dynamic theLength in data.phoneLength)
                        {
                            int phoneLength = theLength;

                            if (phoneNumber.StartsWith(prefix))
                            {
                                phoneNumber = phoneNumber.Substring(prefix.Length);

                                if (phoneNumber.Length == phoneLength)
                                {
                                    _number = ulong.Parse(phoneNumber);
                                    _prefix = ushort.Parse(prefix);

                                    _country = data.label;
                                    _countryCode = data.code;

                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (data.phoneLength != null)
                        {
                            int phoneLength = data.phoneLength;

                            if (phoneNumber.StartsWith(prefix))
                            {
                                phoneNumber = phoneNumber.Substring(prefix.Length);

                                if (phoneNumber.Length == phoneLength)
                                {
                                    _number = ulong.Parse(phoneNumber);
                                    _prefix = ushort.Parse(prefix);

                                    _country = data.label;
                                    _countryCode = data.code;

                                    break;
                                }
                            }
                        }
                        else
                        {
                            int min = data.min, max = data.max;

                            if (phoneNumber.StartsWith(prefix))
                            {
                                phoneNumber = phoneNumber.Substring(prefix.Length);

                                if (phoneNumber.Length >= min && phoneNumber.Length <= max)
                                {
                                    _number = ulong.Parse(phoneNumber);
                                    _prefix = ushort.Parse(prefix);

                                    _country = data.label;
                                    _countryCode = data.code;

                                    break;
                                }
                            }
                        }
                    }
                }

                return new PhoneNumber(_country, _countryCode, _number, _prefix);
            }
            catch
            {
                return null;
            }
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            return GetPhoneNumberInfo(phoneNumber) != null;
        }

        private static string FilterString(string phoneNumber)
        {
            string result = "";

            foreach (char c in phoneNumber)
            {
                foreach (char s in _characters)
                {
                    if (c.Equals(s))
                    {
                        result += c;
                        break;
                    }
                }
            }

            return result;
        }

        private static void LoadJSON()
        {
            _json = JsonConvert.DeserializeObject(Encoding.UTF8.GetString(Resources.data));
        }

        private static void LoadCountryInfos()
        {
            _countryInfos = new List<CountryInfo>();

            foreach (dynamic data in _json)
            {
                try
                {
                    string prefix = data.phone, countryCode = data.code, country = data.label;
                    _countryInfos.Add(new CountryInfo(country, countryCode, $"+{prefix}", ushort.Parse(prefix)));
                }
                catch
                {

                }
            }
        }
    }
}