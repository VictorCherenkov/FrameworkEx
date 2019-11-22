using System;

namespace FrameworkEx.Aspects.Wrappers
{
    public static class WrapAspect
    {
        public static Func<TResult, T> Wrap<T, TResult>(this Func<TResult, T> func, Action onBefore = null, Action<T> onAfter = null)
        {
            return x => { (onBefore ?? (() => { }))(); var val = func(x); (onAfter ?? (_ => { }))(val); return val; };
        }
    }
}