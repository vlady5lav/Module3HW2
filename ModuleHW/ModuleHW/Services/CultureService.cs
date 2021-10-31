using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModuleHW
{
    public class CultureService : ICultureService
    {
        private readonly CultureInfo _defaultCulture;
        private readonly IDictionary<string, CultureInfo> _cultureData;

        public CultureService()
        {
            _defaultCulture = CultureInfo.GetCultureInfo("en-US");
            _cultureData = new Dictionary<string, CultureInfo>();
        }

        public void Add(params string[] cultureIDs)
        {
            foreach (var cultureID in cultureIDs)
            {
                var cultureInfo = new CultureInfo(cultureID);

                _cultureData.Add(cultureID, cultureInfo);
            }
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

        public object GetCulture(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentNullException("Can't get culture info, string is null or empty!");
            }

            return true switch
            {
                true when Regex.IsMatch(str, "[ЇїІіЄєҐґ']") => CultureInfo.GetCultureInfo("uk-UA"),
                true when Regex.IsMatch(str, "[а-яёА-ЯЁ]") => CultureInfo.GetCultureInfo("ru-RU"),
                true when Regex.IsMatch(str, "[a-zA-Z]") => CultureInfo.GetCultureInfo("en-US"),
                true when Regex.IsMatch(str, "[0-9]") => CharTypes.Digit,
                true when Regex.IsMatch(str, "[`~!@#$%^&*()_+-=,./?\\|';:[\"\\]{}<>\\s]") => CharTypes.Symbol,
                _ => _defaultCulture,
            };
        }
    }
}
