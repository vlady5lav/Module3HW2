using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace ModuleHW
{
    public class PhoneBook<T> : IPhoneBook<T>
        where T : IContact
    {
        private readonly IDictionary<CultureInfo, IList<T>> _lettersCollection;
        private readonly IDictionary<CharTypes, IList<T>> _symbolsCollection;
        private readonly ICultureService _cultureService;

        public PhoneBook(ICultureService cultureService)
        {
            _cultureService = cultureService;

            _lettersCollection = new Dictionary<CultureInfo, IList<T>>
            {
                { CultureInfo.GetCultureInfo("ru-RU"), new List<T>() },
                { CultureInfo.GetCultureInfo("en-US"), new List<T>() }
            };

            _symbolsCollection = new Dictionary<CharTypes, IList<T>>
            {
                { CharTypes.Digit, new List<T>() },
                { CharTypes.Symbol, new List<T>() }
            };
        }

        public IReadOnlyCollection<T> this[string key]
        {
            get
            {
                var collection = GetCollection(key);
                var result = new List<T>();

                foreach (var contact in collection)
                {
                    if (contact.FullName.StartsWith(key, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result.Add(contact);
                    }
                }

                return result;
            }
        }

        public void Add(T contact)
        {
            if (contact.FullName == null || string.IsNullOrEmpty(contact.FullName))
            {
                throw new ArgumentException("Full Name is null!");
            }

            var collection = GetCollection(contact.FullName);

            var position = GetPosition(contact.FullName, collection);

            if (position != -1)
            {
                collection.Insert(position, contact);
            }
            else
            {
                collection.Add(contact);
            }
        }

        public int GetPosition(string name, IList<T> collection)
        {
            for (var i = 0; i < collection.Count; i++)
            {
                if (name.CompareTo(collection[i].FullName) < 0)
                {
                    return i;
                }
            }

            return -1;
        }

        public IList<T> GetCollection(string name)
        {
            var cultureInfo = _cultureService.GetCultureInfo(name[0]);

            if (cultureInfo == null)
            {
                if (Regex.IsMatch(name[0].ToString(), "[0-9]"))
                {
                    return _symbolsCollection[CharTypes.Digit];
                }
                else
                {
                    return _symbolsCollection[CharTypes.Symbol];
                }
            }

            return _lettersCollection[cultureInfo];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var collection in _lettersCollection)
            {
                foreach (var contact in collection.Value)
                {
                    yield return contact;
                }
            }

            foreach (var collection in _symbolsCollection)
            {
                foreach (var contact in collection.Value)
                {
                    yield return contact;
                }
            }

            yield break;
        }

        public IEnumerator GetEnumerator()
        {
            return (this as IEnumerable<T>).GetEnumerator();
        }
    }
}
