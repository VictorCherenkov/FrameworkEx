using System;
using System.Reactive.Linq;

namespace FrameworkEx.Rx
{
    public class MidnightTimerScheduler : IDisposable
    {
        private static readonly TimeSpan s_interval = TimeSpan.FromDays(1);
        private readonly IDisposable m_subscription;

        public MidnightTimerScheduler(Action actionToRaise, TimeSpan addDelay)
        {
            m_subscription = Observable
                .Timer(s_interval.Add(addDelay) - DateTime.UtcNow.TimeOfDay, s_interval)
                .Subscribe(_ => actionToRaise());
        }
        public void Dispose()
        {
            m_subscription.DisposeNullSafe();
        }
    }
}