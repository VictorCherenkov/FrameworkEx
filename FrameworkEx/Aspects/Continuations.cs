using System;

namespace FrameworkEx.Aspects
{
    public static class Continuations
    {
        public static Func<T> ContinueWith<T>(this Action src, Func<T> continuation)
        {
            return () =>
            {
                src();
                return continuation();
            };
        }
        public static Func<TResult> ContinueWithFold<T1, T2, TResult>(this Func<Tuple<T1, T2>> src, Func<T1, T2, TResult> continuation)
        {
            return () => src().Fold(continuation);
        }
        public static Func<TResult> ContinueWith<T, TResult>(this Func<T> src, Func<T, TResult> continuation)
        {
            return () => continuation(src());
        }
        public static Func<T> ContinueWithSideEffect<T>(this Func<T> src, Action<T> sideEffect)
        {
            return () =>
            {
                var x = src();
                sideEffect(x);
                return x;
            };
        }
        public static Action ContinueWith(this Action src, Action continuation)
        {
            return () =>
            {
                src();
                continuation();
            };
        }
        public static Func<Tuple<T1, T2>> CombineWith<T1, T2>(this Func<T1> src, Func<T2> continuation)
        {
            return () => Tuple.Create(src(), continuation());
        }
    }
}