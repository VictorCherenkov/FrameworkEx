using System;
using System.Collections.Generic;
using System.Linq;

namespace FrameworkEx
{
    public static class RangeExtensions
    {
        public static Tuple<T, T>[] FindOverlaps<T>(this IEnumerable<T> src, Func<T, DateTime> startField, Func<T, DateTime> endField)
        {
            var ordered = src.OrderBy(startField).ThenBy(endField).ToArray();
            return ordered.Select((x, i) => new
            {
                Item = x,
                Laps = ordered.Skip(i + 1).TakeWhile(y => endField(x) > startField(y)).ToArray()
            }).SelectMany(x => x.Laps.Select(y => Tuple.Create(x.Item, y))).ToArray();
        }
        public static TimeSpan ShiftLeft(this TimeSpan src, TimeSpan grid)
        {
            return TimeSpan.FromTicks(src.Ticks / grid.Ticks * grid.Ticks);
        }
        public static TimeSpan ShiftRight(this TimeSpan src, TimeSpan grid)
        {
            return TimeSpan.FromTicks(src.Ticks % grid.Ticks == 0
                ? src.Ticks
                : src.Ticks / grid.Ticks * grid.Ticks + grid.Ticks);
        }
        public static DateTime ShiftLeft(this DateTime src, TimeSpan grid)//grid must be even divisible from 24h
        {
            return src.Date.Add(src.TimeOfDay.ShiftLeft(grid));
        }
        public static DateTime ShiftRight(this DateTime src, TimeSpan grid)//grid must be even divisible from 24h
        {
            return src.Date.Add(src.TimeOfDay.ShiftRight(grid));
        }
    }
}