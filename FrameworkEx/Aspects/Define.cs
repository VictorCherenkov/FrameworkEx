using System;

namespace FrameworkEx.Aspects
{
    public static class Actions
    {
        public static readonly Action Empty = () => {};
        public static Action From(this Action src)
        {
            return src;
        }
        public static Action ToAction<T>(this T src, Action<T> action)
        {
            return () => action(src);
        }
        public static EventHandler ToEventHandler(this Action src)
        {
            return (_, __) => src();
        }
    }

    public static class Functions
    {
        public static readonly Func<bool> True = () => true;
        public static readonly Func<bool> False = () => false;

        public static Func<T> From<T>(this Func<T> src)
        {
            return src;
        }
        public static Func<T, TResult> From<T, TResult>(this Func<T, TResult> src)
        {
            return src;
        }
        public static Func<T> ToFunc<T>(this T src)
        {
            return () => src;
        }
        public static Func<TResult> ToFunc<T, TResult>(this T src, Func<T, TResult> func)
        {
            return () => func(src);
        }
    }
}