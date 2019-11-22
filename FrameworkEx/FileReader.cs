using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FrameworkEx
{
    public static class FileReader
    {
        public static IEnumerable<string> EnumLines(this string fileName)
        {
            using (var fs = File.OpenRead(fileName))
            using (var sr = new StreamReader(fs, Encoding.UTF8, true, 4096))
                while (true)
                {
                    var line = sr.ReadLine();
                    if (line == null) yield break;
                    yield return line;
                }
        }
    }
}