using System;

namespace FrameworkEx.Aspects.Wrappers
{
    public static class TryCatchAspect
    {
        //Source
        private static T InvokeTryCatch<TContext, T>(this Func<TContext, T> function, Action<TContext, Exception> onException, TContext context, T defaultValue = default(T))
        {
            try
            {
                return function(context);
            }
            catch (Exception ex)
            {
                onException(context, ex);
                return defaultValue;
            }
        }

        //Lazy
        public static Func<T> WrapWithTryCatch<TContext, T>(this TContext context, Func<TContext, T> function, Action<TContext, Exception> onException, T defaultValue = default(T))
        {
            return () => InvokeTryCatch(function, onException, context, defaultValue);
        }
        public static Func<T> WrapWithTryCatch<TContext, T>(this TContext context, Func<TContext, T> function, Action<Exception> onException, T defaultValue = default(T))
        {
            return () => InvokeTryCatch(function, (_, ex) => onException(ex), context, defaultValue);
        }
        public static Action WrapWithTryCatch<TContext>(this TContext context, Action<TContext> action, Action<Exception> onException)
        {
            return () => InvokeTryCatch(_ => { action(context); return Void.Default; }, (_, ex) => onException(ex), context, Void.Default);
        }
        public static Func<T> WrapWithTryCatch<T>(this Func<T> function, Action<Exception> onException, T defaultValue = default(T))
        {
            return () => InvokeTryCatch(_ => function(), (_, ex) => onException(ex), 0, defaultValue);
        }
        public static Func<T> WrapWithTryCatch<T>(this Func<T> function, Action<Exception> onException, T src, T defaultValue)
        {
            return () => InvokeTryCatch(_ => function(), (_, ex) => onException(ex), src, defaultValue);
        }
        public static Action WrapWithTryCatch(this Action action, Action<Exception> onException)
        {
            return () => InvokeTryCatch(_ => { action(); return Void.Default; }, (_, ex) => onException(ex), Void.Default, Void.Default);
        }

        //Direct
        public static T TryCatch<TContext, T>(this TContext context, Func<TContext, T> function, Action<TContext, Exception> onException, T defaultValue = default(T))
        {
            return InvokeTryCatch(function, onException, context, defaultValue);
        }
        public static T TryCatch<TContext, T>(this TContext context, Func<TContext, T> function, Action<Exception> onException, T defaultValue = default(T))
        {
            return InvokeTryCatch(function, (_, ex) => onException(ex), context, defaultValue);
        }
        public static void TryCatch<TContext>(this TContext context, Action<TContext> action, Action<Exception> onException)
        {
            InvokeTryCatch(_ => { action(context); return Void.Default; }, (_, ex) => onException(ex), context, Void.Default);
        }
        public static T TryCatch<T>(this Func<T> function, Action<Exception> onException, T defaultValue = default(T))
        {
            return InvokeTryCatch(_ => function(), (_, ex) => onException(ex), 0, defaultValue);
        }
        public static T TryCatch<T>(this Func<T> function, Action<Exception> onException, T src, T defaultValue)
        {
            return InvokeTryCatch(_ => function(), (_, ex) => onException(ex), src, defaultValue);
        }
        public static void TryCatch(this Action action, Action<Exception> onException)
        {
            InvokeTryCatch(_ => { action(); return Void.Default; }, (_, ex) => onException(ex), Void.Default, Void.Default);
        }
    }
}