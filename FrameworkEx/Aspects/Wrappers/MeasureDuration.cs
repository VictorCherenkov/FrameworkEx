using System;
using System.Diagnostics;

namespace FrameworkEx.Aspects.Wrappers
{
    public static class MeasureDurationAspect
    {
        //Source
        private static T Invoke<T>(this Func<T> function, Action<TimeSpan> elapsedHandler)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return function();
            }
            finally
            {
                elapsedHandler(sw.Elapsed);
            }
        }

        //Lazy
        public static Action WrapWithMeasureDuration(this Action action, Action<TimeSpan> elapsedHandler)
        {
            return () => Invoke(() => { action(); return Void.Default; }, elapsedHandler);
        }
        public static Func<T> WrapWithMeasureDuration<T>(this Func<T> function, Action<TimeSpan> elapsedHandler)
        {
            return () => Invoke(function, elapsedHandler);
        }

        //Direct
        public static void MeasureDuration(this Action action, Action<TimeSpan> elapsedHandler)
        {
            Invoke(() => { action(); return Void.Default; }, elapsedHandler);
        }
        public static T MeasureDuration<T>(this Func<T> function, Action<TimeSpan> elapsedHandler)
        {
            return Invoke(function, elapsedHandler);
        }
    }
}