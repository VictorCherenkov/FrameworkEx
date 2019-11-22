using System;
using FrameworkEx.Aspects.Invokers;

namespace FrameworkEx
{
    public static class DisposableExtensions
    {
        public static void DisposeNullSafe(this IDisposable x)
        {
            x.DoIfNotNull(y => y.Dispose());
        }
        public static IDisposable AsDisposable(Action disposeAction)
        {
            return new DisposableFromAction(disposeAction);
        }
        private class DisposableFromAction : IDisposable
        {
            private readonly Action m_action;

            public DisposableFromAction(Action action)
            {
                m_action = action;
            }

            public void Dispose()
            {
                m_action();
            }
        }
    }
}