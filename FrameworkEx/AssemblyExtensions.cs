using System.Linq;
using System.Reflection;

namespace FrameworkEx
{
    public static class AssemblyExtensions
    {
        public static string GetInformationalVersion(this Assembly src)
        {
            return src
                .GetCustomAttributes(typeof(AssemblyInformationalVersionAttribute))
                .OfType<AssemblyInformationalVersionAttribute>()
                .Single()
                .InformationalVersion;
        }
    }
}