using System;

namespace FrameworkEx.Aspects.Invokers
{
    public static class IfAspect
    {
        public static Action DoIf(this Action action, bool condition){if (condition) action(); return action;}
        public static Action DoIf(this Action action, Func<bool> condition){if (condition())action();return action;}
        public static T DoIf<T>(this T src, Func<T, bool> condition, Action<T> action){if (condition(src))action(src);return src;}
        public static T DoIfNot<T>(this T src, Func<T, bool> condition, Action<T> action){if (!condition(src))action(src);return src;}
        public static T DoIfNull<T>(this T src, Action action) where T : class{if (src == null)action(); return src;}
        public static void DoIfTrue(this bool src, Action action){if (src) action();}
    }
}