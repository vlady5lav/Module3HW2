using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModuleHW
{
    public class CultureService : ICultureService
    {
        private readonly CultureInfo _defaultCulture;

        public CultureService()
        {
            _defaultCulture = CultureInfo.GetCultureInfo("en-US");
        }

        public CultureInfo GetCultureInfo(char key)
        {
            if (string.IsNullOrEmpty(key.ToString()))
            {
                throw new ArgumentException("Can't get culture info, char is null or empty!");
            }

            if (Regex.IsMatch(key.ToString(), "[A-Za-z]"))
            {
                return CultureInfo.GetCultureInfo("en-US");
            }
            else if (Regex.IsMatch(key.ToString(), "[А-Яа-яЁё]"))
            {
                return CultureInfo.GetCultureInfo("ru-RU");
            }
            else if (Regex.IsMatch(key.ToString(), "[`~!@#$%^&*()_+-=,./?\\|';:[\"\\]{}<>0-9]"))
            {
                return null;
            }
            else
            {
                return _defaultCulture;
            }
        }
    }
}
