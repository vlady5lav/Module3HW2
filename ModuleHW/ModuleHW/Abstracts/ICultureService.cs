using System.Globalization;

namespace ModuleHW
{
    public interface ICultureService
    {
        CultureInfo GetCultureInfo(char key);
    }
}
