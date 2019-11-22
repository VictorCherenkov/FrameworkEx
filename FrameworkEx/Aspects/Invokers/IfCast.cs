using System;

namespace FrameworkEx.Aspects.Invokers
{
    public static class IfCastAspect
    {
        private static MatchResult DoIfCast<TCast>(this object src, bool handled, Action<TCast> action) where TCast : class
        {
            if (handled)
            {
                return new MatchResult(src, true);
            }
            var cast = src.As<TCast>();
            if (cast == null)
            {
                return new MatchResult(src, false);
            }
            action(cast);
            return new MatchResult(src, true);
        }
        public static MatchResult DoIfCast<TCast>(this object src, Action<TCast> action) where TCast : class
        {
            return DoIfCast(src, false, action);
        }
        public static MatchResult DoIfCast<TCast>(this MatchResult src, Action<TCast> action) where TCast : class
        {
            return DoIfCast(src.Context, src.Handled, action);
        }
        public static TResult GetIfCast<TCast, TResult>(this object src, Func<TCast, TResult> function, TResult defaultValue = null)
            where TCast : class
            where TResult : class
        {
            var cast = src.As<TCast>();
            return cast == null ? defaultValue : function(cast);
        }
    }

    public class MatchResult
    {
        public object Context { get; private set; }
        public bool Handled { get; private set; }

        public MatchResult(object context, bool handled)
        {
            Context = context;
            Handled = handled;
        }
    }
}