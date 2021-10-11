using System.Globalization;

namespace ModuleHW
{
    public interface ICultureIdentifier
    {
        void Add(string cultureID);
        CultureInfo[] GetCultureData();
        CultureInfo GetCultureInfo(string str);
    }
}