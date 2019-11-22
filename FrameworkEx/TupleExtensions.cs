using System;

namespace FrameworkEx
{
    public static class TupleExtensions
    {
        public static Tuple<T1, T2> Fold0<T1, T2>(this Tuple<T1, T2> x, Action<T1, T2> action)
        {
            action(x.Item1, x.Item2);
            return x;
        }
        public static TResult Fold<T1, T2, TResult>(this Tuple<T1, T2> x, Func<T1, T2, TResult> func)
        {
            return func(x.Item1, x.Item2);
        }
        public static Tuple<T1, T2, T3> Fold0<T1, T2, T3>(this Tuple<T1, T2, T3> x, Action<T1, T2, T3> action)
        {
            action(x.Item1, x.Item2, x.Item3);
            return x;
        }
        public static TResult Fold<T1, T2, T3, TResult>(this Tuple<T1, T2, T3> x, Func<T1, T2, T3, TResult> func)
        {
            return func(x.Item1, x.Item2, x.Item3);
        }
    }
}