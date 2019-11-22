using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkEx
{
    public static class StringExtensions
    {
        public static string EmptyToNull(this string src)
        {
            return src == string.Empty ? null : src;
        }
        public static string ToLinesString<T>(this IEnumerable<T> items)
        {
            return string.Join(Environment.NewLine, items);
        }
        public static IEnumerable<string> SkipNullsOrEmpty(this IEnumerable<string> source)
        {
            return source.Where(x => !string.IsNullOrEmpty(x));
        }
        public static IEnumerable<T> SkipNullsOrEmpty<T>(this IEnumerable<T> source, Func<T, string> keySelector)
        {
            return source.Where(x => !string.IsNullOrEmpty(keySelector(x)));
        }
        public static bool IsNullOrEmpty(this string instance)
        {
            return string.IsNullOrEmpty(instance);
        }
        public static bool IsNotNullOrEmpty(this string instance)
        {
            return !instance.IsNullOrEmpty();
        }
        public static bool AreEqualIgnoreCase(this string x, string other)
        {
            return string.Compare(x, other, StringComparison.InvariantCultureIgnoreCase) == 0;
        }
        public static string ToLine(this IEnumerable<string> x)
        {
            return string.Join(" ", x);
        }
        public static string CapitaliseFirst(this string src)
        {
            return src.IsNotNullOrEmpty() ? string.Format("{0}{1}", src.Substring(0, 1).ToUpper(), src.Substring(1)) : src;
        }
    }
}
