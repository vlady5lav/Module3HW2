using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModuleHW
{
    public class CultureIdentifier : ICultureIdentifier
    {
        private readonly IDictionary<string, CultureInfo> _cultureData;

        public CultureIdentifier()
        {
            _cultureData = new Dictionary<string, CultureInfo>();
        }

        public void Add(string cultureID)
        {
            var cultureInfo = new CultureInfo(cultureID);

            _cultureData.Add(cultureID, cultureInfo);
        }

        public CultureInfo[] GetCultureData()
        {
            var cultureInfo = new CultureInfo[_cultureData.Count];
            var i = 0;

            foreach (var cultureData in _cultureData)
            {
                cultureInfo[i++] = cultureData.Value;
            }

            return cultureInfo;
        }

        public CultureInfo GetCultureInfo(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("Can't get culture info, string is null or empty!");
            }

            bool ukr = false;
            bool rus = false;
            bool eng = false;
            bool sym = false;

            foreach (var c in str)
            {
                var ch = c.ToString();

                if (Regex.IsMatch(ch, "[ЇїІіЄєҐґ']"))
                {
                    ukr = true;
                }

                if (Regex.IsMatch(ch, "[а-яёА-ЯЁ]"))
                {
                    rus = true;
                }

                if (Regex.IsMatch(ch, "[a-zA-Z]"))
                {
                    eng = true;
                }

                if (Regex.IsMatch(ch, "[`~!@#$%^&*()_+-=,./?\\|';:[\"\\]{}<>\\s0-9]"))
                {
                    sym = true;
                }
            }

            return true switch
            {
                true when ukr => CultureInfo.GetCultureInfo("uk-UA"),
                true when rus => CultureInfo.GetCultureInfo("ru-RU"),
                true when eng => CultureInfo.GetCultureInfo("en-US"),
                true when sym => null,
                _ => throw new ArgumentException("Unknown culture!"),
            };
        }
    }
}
