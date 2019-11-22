using System;
using System.Collections.Generic;
using System.Text;

namespace FrameworkEx
{
    public static class StringBuilderExtensions
    {
        public static StringBuilder AppendLines(this StringBuilder sb, IEnumerable<string> lines)
        {
            foreach (var x in lines)
                sb.AppendLine(x);
            return sb;
        }
        public static StringBuilder AppendLinesFormat<T, T1, T2>(this StringBuilder sb, string prefix, IEnumerable<T> array, Func<T, T1> selector1, Func<T, T2> selector2)
        {
            foreach (var x in array) 
                sb.AppendFormat(prefix, selector1(x), selector2(x)).AppendLine();
            return sb;
        }
        public static StringBuilder AppendLineFormat(this StringBuilder sb, string line, object arg0)
        {
            return sb.AppendFormat(line, arg0).AppendLine();
        }
    }
}