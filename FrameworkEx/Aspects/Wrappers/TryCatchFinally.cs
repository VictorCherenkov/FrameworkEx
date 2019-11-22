using System;

namespace FrameworkEx.Aspects.Wrappers
{
    public static class TryCatchFinallyAspect
    {
        //Source
        private static void InvokeTryCatchFinally(this Action action, Action<Exception> onException, Action finallyBlock)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                onException(ex);
            }
            finally
            {
                finallyBlock();
            }
        }

        //Lazy
        public static Action WrapWithTryCatchFinally(this Action action, Action<Exception> onException, Action finallyBlock)
        {
            return () => InvokeTryCatchFinally(action, onException, finallyBlock);
        }

        //Direct
        public static void TryCatchFinally(this Action action, Action<Exception> onException, Action finallyBlock)
        {
            InvokeTryCatchFinally(action, onException, finallyBlock);
        }
    }
}