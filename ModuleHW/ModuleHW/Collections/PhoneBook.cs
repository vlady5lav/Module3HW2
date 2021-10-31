using System;
using System.Collections;
using System.Collections.Generic;

namespace ModuleHW
{
    public class PhoneBook<T> : IPhoneBook<T>
        where T : IContact
    {
        private readonly ICultureService _cultureService;
        private readonly IDictionary<object, IList<T>> _cultureCollection;

        public PhoneBook(ICultureService cultureService)
        {
            _cultureService = cultureService;

            _cultureService.Add("uk-UA", "ru-RU", "en-US");

            _cultureCollection = new Dictionary<object, IList<T>>();

            foreach (var cultureInfo in _cultureService.GetCultureData())
            {
                _cultureCollection.Add(cultureInfo, new List<T>());
            }

            _cultureCollection.Add(CharTypes.Digit, new List<T>());
            _cultureCollection.Add(CharTypes.Symbol, new List<T>());
        }

        public IReadOnlyCollection<T> this[string str]
        {
            get
            {
                var result = new List<T>();

                foreach (var contacts in _cultureCollection.Values)
                {
                    foreach (var contact in contacts)
                    {
                        if (contact.FullName.StartsWith(str, StringComparison.InvariantCultureIgnoreCase))
                        {
                            result.Add(contact);
                        }
                    }
                }

                result.Sort((c1, c2) => c1.FullName.CompareTo(c2.FullName));

                return result;
            }
        }

        public void Add(T contact)
        {
            if (string.IsNullOrEmpty(contact.FullName))
            {
                throw new ArgumentException("Full Name is null!");
            }

            var collection = GetCollection(contact);

            Insert(contact, collection);
        }

        public void Insert(T contact, IList<T> contacts)
        {
            var pos = -1;

            for (var i = 0; i < contacts.Count; i++)
            {
                if (contact.FullName.CompareTo(contacts[i].FullName) < 0)
                {
                    pos = i;
                }
            }

            if (pos > 0 && pos < contacts.Count)
            {
                contacts.Insert(pos, contact);
            }
            else
            {
                contacts.Add(contact);
            }
        }

        public IList<T> GetCollection(T contact)
        {
            if (contact == null)
            {
                throw new ArgumentNullException("Contact is null!");
            }

            var str = contact.FullName;

            var culture = _cultureService.GetCulture(str);

            (contact as Contact).Collection = culture.ToString();

            return _cultureCollection[culture];
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (var collection in _cultureCollection)
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
