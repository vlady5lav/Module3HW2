using System.Collections.Generic;

namespace ModuleHW
{
    public interface IPhoneBook<T> : IEnumerable<T>
        where T : IContact
    {
        IReadOnlyCollection<T> this[string key] { get; }

        void Add(T contact);
    }
}
