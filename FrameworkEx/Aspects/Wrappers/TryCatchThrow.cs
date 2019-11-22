using System;

namespace FrameworkEx.Aspects.Wrappers
{
    public static class TryCatchThrowAspect
    {
        //Source
        private static T Invoke<T>(this Func<T> function, Action<Exception> onException)
        {
            try
            {
                return function();
            }
            catch (Exception ex)
            {
                onException(ex);
                throw;
            }
        }

        //Lazy
        public static Func<T> WrapWithTryCatchThrow<T>(this Func<T> function, Action<Exception> onException)
        {
            return () => Invoke(function, onException);
        }
        public static Action WrapWithTryCatchThrow(this Action action, Action<Exception> onException)
        {
            return () => Invoke(() => { action(); return Void.Default; }, onException);
        }
        public static Action WrapWithTryCatchThrow<T>(this T src, Action<T> action, Action<Exception> onException)
        {
            return () => Invoke(() => { action(src); return Void.Default; }, onException);
        }

        //Direct
        public static T TryCatchThrow<T>(this Func<T> function, Action<Exception> onException)
        {
            return Invoke(function, onException);
        }
        public static void TryCatchThrow(this Action action, Action<Exception> onException)
        {
            Invoke(() => { action(); return Void.Default; }, onException);
        }
        public static void TryCatchThrow<T>(this T src, Action<T> action, Action<Exception> onException)
        {
            Invoke(() => { action(src); return Void.Default; }, onException);
        }
    }
}