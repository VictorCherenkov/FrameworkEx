using System;

namespace FrameworkEx.Aspects.Invokers
{
    public static class IfNotNullAspect
    {
        public static T DoIfNotNull<T>(this T src, Action<T> action) where T : class { if (src != null) action(src); return src; }
        public static T? DoIfNotNull<T>(this T? src, Action<T> action) where T : struct { if (src != null) action(src.Value); return src; }
        public static TResult GetIfNotNull<T, TResult>(this T x, Func<T, TResult> map, TResult @default = null) where T : class where TResult : class { return x == null ? @default : map(x); }
        public static TResult GetStruct<T, TResult>(this T x, Func<T, TResult> map, TResult @default) where T : class where TResult : struct { return x == null ? @default : map(x); }
        public static TResult? GetNullable<T, TResult>(this T x, Func<T, TResult> map) where T : class where TResult : struct { return x == null ? default(TResult?) : map(x); }
        public static TResult? GetNullable<T, TResult>(this T x, Func<T, TResult?> map) where T : class where TResult : struct { return x == null ? default(TResult?) : map(x); }
        public static TResult GetFromNullable<T, TResult>(this T? x, Func<T, TResult> map, TResult @default = null) where T : struct where TResult : class { return x == null ? @default : map(x.Value); }
        public static TResult GetStructFromNullable<T, TResult>(this T? x, Func<T, TResult> map, TResult @default) where T : struct where TResult : struct { return x == null ? @default : map(x.Value); }
        public static TResult? GetNullableFromNullable<T, TResult>(this T? x, Func<T, TResult> map) where T : struct where TResult : struct { return x == null ? default(TResult?) : map(x.Value); }
        public static TResult? GetNullableFromNullable<T, TResult>(this T? x, Func<T, TResult?> map) where T : struct where TResult : struct { return x == null ? default(TResult?) : map(x.Value); }
    }
}