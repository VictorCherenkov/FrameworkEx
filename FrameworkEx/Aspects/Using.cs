using System;

namespace FrameworkEx.Aspects
{
    public static class UsingAspect
    {
        public static void Using<T>(this T src, Action<T> action) where T : IDisposable
        {
            using (src)
            {
                action(src);
            }
        }
        public static TResult Using<T, TResult>(this T src, Func<T, TResult> action) where T : IDisposable
        {
            using (src)
            {
                return action(src);
            }
        }
    }
}