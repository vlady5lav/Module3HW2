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
        private readonly ICultureIdentifier _cultureIdentifier;
        private readonly IDictionary<CultureInfo, IList<T>> _identifiedCollection;
        private readonly IDictionary<CharTypes, IList<T>> _symbolsCollection;
        private IList<T> _allList;

        public PhoneBook(ICultureIdentifier cultureIdentifier)
        {
            _cultureIdentifier = cultureIdentifier;

            _cultureIdentifier.Add("uk-UA");
            _cultureIdentifier.Add("ru-RU");
            _cultureIdentifier.Add("en-US");

            _identifiedCollection = new Dictionary<CultureInfo, IList<T>>();

            foreach (var culture in _cultureIdentifier.GetCultureData())
            {
                _identifiedCollection.Add(culture, new List<T>());
            }

            _symbolsCollection = new Dictionary<CharTypes, IList<T>>()
            {
                { CharTypes.Digit, new List<T>() },
                { CharTypes.Symbol, new List<T>() },
            };

            _allList = new List<T>();
        }

        public IReadOnlyCollection<T> this[string str]
        {
            get
            {
                GeneralListBuilder(_identifiedCollection.Values);
                GeneralListBuilder(_symbolsCollection.Values);
                (_allList as List<T>).Sort((str1, str2) => str1.FullName.CompareTo(str2.FullName));

                var result = new List<T>();

                foreach (var contact in _allList)
                {
                    if (contact.FullName.StartsWith(str, StringComparison.InvariantCultureIgnoreCase))
                    {
                        result.Add(contact);
                    }
                }

                return result;
            }
        }

        public void GeneralListBuilder(ICollection<IList<T>> dic)
        {
            foreach (var contacts in dic)
            {
                foreach (var contact in contacts as IList<T>)
                {
                    _allList.Add(contact);
                }
            }
        }

        public void Add(T contact)
        {
            if (contact.FullName == null || string.IsNullOrEmpty(contact.FullName))
            {
                throw new ArgumentException("Full Name is null!");
            }

            var collection = GetCollection(contact.FullName);
            (contact as Contact).Collection = _cultureIdentifier.GetCultureInfo(contact.FullName)?.Name;

            var position = GetPosition(contact.FullName, collection);

            if (position > 0 && position < collection.Count)
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
            var cultureInfo = _cultureIdentifier.GetCultureInfo(name);

            if (cultureInfo == null)
            {
                if (Regex.IsMatch(name[0..].ToString(), "[0-9]") || (Regex.IsMatch(name[0].ToString(), "[+]") && Regex.IsMatch(name[1..].ToString(), "[0-9]")))
                {
                    return _symbolsCollection[CharTypes.Digit];
                }
                else
                {
                    return _symbolsCollection[CharTypes.Symbol];
                }
            }

            return _identifiedCollection[cultureInfo];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var collection in _identifiedCollection)
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
