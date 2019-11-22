using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;

namespace FrameworkEx.Rx
{
    public static class MoreObservable
    {
        public static IObservable<long> Timer(TimeSpan offset, TimeSpan period, CancellationToken token, TimeSpan checkCancelPeriod)
        {
            return Observable.Timer(offset, period).Select(x => new { Iteration = x, Signal = Signal.Poll })
                .Merge(Observable.Timer(TimeSpan.Zero, checkCancelPeriod).Select(x => new { Iteration = x, Signal = Signal.Pulse }))
                .TakeWhile(_ => !token.IsCancellationRequested)
                .Where(x => x.Signal == Signal.Poll)
                .Select(x => x.Iteration);
        }
        private enum Signal { Poll, Pulse }

        public static IObservable<TArgResult> FromCustomEvent<TEventHandler, TArg1, TArg2, TArgResult>(
            Action<TEventHandler> add,
            Action<TEventHandler> remove,
            Func<Action<TArg1, TArg2>, TEventHandler> actionToEventHandler,
            Func<TArg1, TArg2, TArgResult> resultConverter)
        {
            return Observable.Create<TArgResult>(o =>
            {
                var eventHandler = actionToEventHandler((s, a) => o.OnNext(resultConverter(s, a)));
                add(eventHandler);
                return Disposable.Create(() => remove(eventHandler));
            });
        }
    }
}