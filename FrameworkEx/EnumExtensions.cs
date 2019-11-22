using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkEx
{
    public static class EnumExtensions
    {
         public static IEnumerable<TEnum> GetEnumValues<TEnum>()
         {
             return Enum.GetValues(typeof(TEnum)).Cast<object>().Select(value => value.CastTo<TEnum>());
         }
         public static TEnum ParseToEnum<TEnum>(this string src)
         {
             return (TEnum)Enum.Parse(typeof(TEnum), src);
         }
        public static string GetEnumName<TEnum>(this TEnum src)
        {
            return Enum.GetName(typeof(TEnum), src);
        }
    }
}