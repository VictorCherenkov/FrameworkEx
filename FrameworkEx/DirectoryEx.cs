using System.IO;
using System.Linq;

namespace FrameworkEx
{
    public static class DirectoryEx
    {
        public static long CalcSize(string folder)
        {
            return Directory.GetFiles(folder).Select(file => new FileInfo(file).Length).Sum()
                + Directory.GetDirectories(folder).Sum(dir => CalcSize(dir));
        }
    }
}