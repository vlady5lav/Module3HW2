using System.Globalization;

namespace ModuleHW
{
    public interface ICultureService
    {
        void Add(params string[] cultureIDs);
        CultureInfo[] GetCultureData();
        object GetCulture(string str);
    }
}
