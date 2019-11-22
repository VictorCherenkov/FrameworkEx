using System;

namespace FrameworkEx.Aspects.Invokers
{
    public static class InvokeAlwaysAspect
    {
        public static T Do<T>(this T src, Action<T> action) { action(src); return src;}
        public static TResult Get<TValue, TResult>(this TValue value, Func<TValue, TResult> mapFunction) { return mapFunction(value);}
    }
}