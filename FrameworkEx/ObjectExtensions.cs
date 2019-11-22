using System.Linq;

namespace FrameworkEx
{
    public static class ObjectExtensions
    {
        public static T[] Unfold<T>(this T value)
        {
            return new[] {value};
        }
        public static TResult As<TResult>(this object x) where TResult : class
        {
            return x as TResult;
        }
        public static T CastTo<T>(this object x)
        {
            return (T)x;
        }
        public static bool In<T>(this T value, params T[] set)
        {
            return set.Contains(value);
        }
        public static bool NotIn<T>(this T value, params T[] set)
        {
            return !In(value, set);
        }

        // TODO: move it somewhere else
        public delegate bool TryWithOutFunc<in TP, TV>(TP input, out TV output);
        public static TV? TryParseToNullable<TV>(this string obj, TryWithOutFunc<string, TV> func) where TV : struct
        {
            TV v; return func(obj, out v) ? v : (TV?)null;
        }
        public static TV TryParseToNull<TV>(this string obj, TryWithOutFunc<string, TV> func) where TV : class
        {
            TV v; return obj == null ? null : func(obj, out v) ? v : null;
        }
    }
}