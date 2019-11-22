using System;
using System.Collections.Generic;

namespace FrameworkEx
{
    public static class ComparerUtils
    {
        public static ComparisonResult CompareByDefault<T, TKey1, TKey2>(T x, T y, Func<T, TKey1> byKey, Func<T, TKey2> thenByKey)
        {
            var result = ComparisonResult.FromComparer(Comparer<TKey1>.Default.Compare(byKey(x), byKey(y)));
            return result == ComparisonResult.Same
                ? ComparisonResult.FromComparer(Comparer<TKey2>.Default.Compare(thenByKey(x), thenByKey(y)))
                : result;
        }

        public static ComparisonResult CompareByDefault<T1, T2>(T1 x, T1 y, Func<T1, T2> keySelector)
        {
            return ComparisonResult.FromComparer(Comparer<T2>.Default.Compare(keySelector(x), keySelector(y)));
        }

        public static ComparisonResult CompareByDefault<T>(T x, T y)
        {
            return ComparisonResult.FromComparer(Comparer<T>.Default.Compare(x, y));
        }

        public static IComparer<T> CreateComparer<T>(Func<T, T, ComparisonResult> comparison)
        {
            return new ComparerByComparison<T>(comparison);
        }

        private class ComparerByComparison<T> : ComparerEx<T>
        {
            private readonly Func<T, T, ComparisonResult> m_comparison;

            public ComparerByComparison(Func<T, T, ComparisonResult> comparison)
            {
                m_comparison = comparison;
            }

            public override ComparisonResult Compare(T x, T y)
            {
                return m_comparison(x, y);
            }
        }
    }
    public abstract class ComparerEx<T> : IComparer<T>
    {
        int IComparer<T>.Compare(T x, T y)
        {
            return Compare(x, y).ToSign();
        }
        public abstract ComparisonResult Compare(T x, T y);
    }
    public class ComparisonResult
    {
        public static ComparisonResult LeftIsGreater = new ComparisonResult(1);
        public static ComparisonResult RightIsGreater = new ComparisonResult(-1);
        public static ComparisonResult Same = new ComparisonResult(0);
        public static ComparisonResult FromComparer(int comparisonResult)
        {
            return comparisonResult < 0 ? RightIsGreater : comparisonResult > 0 ? LeftIsGreater : Same;
        }

        public ComparisonResult Invert()
        {
            if (this == Same)
            {
                return Same;
            }
            if (this == LeftIsGreater)
            {
                return RightIsGreater;
            }
            if (this == RightIsGreater)
            {
                return LeftIsGreater;
            }
            throw new NotSupportedException(string.Format("Unknown ComparisonResult: {0}", this));
        }

        private readonly int m_sign;

        private ComparisonResult(int sign)
        {
            m_sign = sign;
        }

        public int ToSign()
        {
            return m_sign;
        }
    }
}