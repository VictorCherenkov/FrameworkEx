using System;

namespace FrameworkEx.Aspects.Wrappers
{
    public static class LockAspect
    {
        //Source
        private static T InvokeLock<TContext, T>(this Func<TContext, T> function, object lockObj, TContext context)
        {
            lock(lockObj)
            {
                return function(context);
            }
        }

        //Lazy
        public static Func<T> WrapWithLock<T>(this Func<T> function, object lockObj)
        {
            return () => InvokeLock(_ => function(), lockObj, Void.Default);
        }
        public static Action WrapWithLock(this Action action, object lockObj)
        {
            return () => InvokeLock(_ => { action(); return Void.Default; }, lockObj, Void.Default);
        }

        //Direct
        public static T Lock<T>(this object lockObj, Func<T> function)
        {
            return InvokeLock(_ => function(), lockObj, Void.Default);
        }
        public static void Lock(this object lockObj, Action action)
        {
            InvokeLock(_ => { action(); return Void.Default; }, lockObj, Void.Default);
        }
    }
}