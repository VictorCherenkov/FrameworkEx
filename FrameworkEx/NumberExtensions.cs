using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkEx
{
    public static class NumberExtensions
    {
        public static decimal? Abs(this decimal? x)
        {
            return x != null ? Math.Abs(x.Value) : (decimal?) null;
        }
        public static decimal? Divide(this decimal? x, int division)
        {
            return x != null ? Math.Round(x.Value / division, 1) : (decimal?)null;
        }
        public static Sign? ToSign(this decimal? x)
        {
            return x == null ? (Sign?)null : ToSign(x.Value);
        }
        public static Sign ToSign(this decimal x)
        {
            return x > 0 ? Sign.Positive : x < 0 ? Sign.Negative : Sign.Zero;
        }
        public static double Bound(this double x, double low, double high)
        {
            return x < low ? low : x > high ? high : x;
        }
        public static decimal? Negate(this decimal? x)
        {
            return x != null ? Negate(x.Value) : (decimal?)null;
        }
        public static decimal Negate(this decimal x)
        {
            return -x;
        }
        public static decimal SampleRight(this decimal src, SampleGrid grid)
        {
            return grid.SampleRight(src);
        }
    }

    public class SampleGrid
    {
        private readonly decimal?[] m_grid;
        public SampleGrid(IEnumerable<decimal> grid)
        {
            m_grid = grid.Select(x => new decimal?(x)).OrderBy(x => x).ToArray();
        }
        public decimal SampleRight(decimal src)
        {
            return m_grid.FirstOrDefault(x => x > src) ?? decimal.MaxValue;
        }
    }

    public enum Sign
    {
        Negative,
        Zero,
        Positive,
    }
}