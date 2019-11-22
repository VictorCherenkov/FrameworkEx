using System;

namespace FrameworkEx
{
    public static class DateTimeExtensions
    {
        public static DateTime ResetKind(this DateTime src)
        {
            return DateTime.SpecifyKind(src, DateTimeKind.Unspecified);
        }
    }
}