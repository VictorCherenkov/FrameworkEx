using System;

namespace FrameworkEx
{
    public static class TimeSpanExtensions
    {
        public static bool IsNegative(this TimeSpan x)
        {
            return x.Duration() != x;
        }
        public static double Divide(this TimeSpan src, TimeSpan divider)
        {
            return src.Ticks / (double)divider.Ticks;
        }
    }
}