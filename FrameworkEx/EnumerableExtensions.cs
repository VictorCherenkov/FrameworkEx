using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FrameworkEx
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<TSource> Concat<TSource>(params IEnumerable<TSource>[] sources)
        {
            return sources.Aggregate((acc, value) => acc.Concat(value));
        }
        public static TResult[] SelectArrayNullSafe<T, TResult>(this IEnumerable<T> src, Func<T, TResult> converter)
        {
            return src != null ? src.Select(converter).ToArray() : null;
        }
        public static TResult[] SelectArraySafe<T, TResult>(this IEnumerable<T> x, Func<T, TResult> selector, Action<T, Exception> errorHandler)
        {
            return SelectSafe(x, selector, errorHandler).ToArray();
        }
        public static IEnumerable<TResult> SelectSafe<T, TResult>(this IEnumerable<T> x, Func<T, TResult> selector, Action<T, Exception> errorHandler)
        {
            var result = default(TResult);
            var resultValueSet = false;
            foreach (var element in x)
            {
                try
                {
                    result = selector(element);
                    resultValueSet = true;
                }
                catch (Exception ex)
                {
                    errorHandler(element, ex);
                }
                if (resultValueSet)
                {
                    yield return result;
                    resultValueSet = false;
                }
            }
        }
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> value)
        {
            return value.ToList().AsReadOnly();
        }

        public static IEnumerable<T> Exclude<T>(this IEnumerable<T> source, Predicate<T> predicate)
        {
            return source.Where(x => !predicate(x));
        }

        public static IEnumerable<T> WhereEqualsMax<T, TKey>(this ICollection<T> items, Func<T, TKey> keySelector)
        {
            if (!items.Any())
            {
                return items;
            }
            var max = items.Max(keySelector);
            var comparer = Comparer<TKey>.Default;
            return items.Where(x => comparer.Compare(keySelector(x), max) == 0);
        }

        public static IEnumerable<T> WhereEqualsMin<T, TKey>(this ICollection<T> items, Func<T, TKey> keySelector)
        {
            if (!items.Any())
            {
                return items;
            }
            var max = items.Min(keySelector);
            var comparer = Comparer<TKey>.Default;
            return items.Where(x => comparer.Compare(keySelector(x), max) == 0);
        }

        public static TValue GetOrCreate<TKey, TValue>(this Dictionary<TKey, TValue> x, TKey key, Func<TValue> creator)
        {
            TValue result;
            var isGot = x.TryGetValue(key, out result);
            if (!isGot)
            {
                result = creator();
                x[key] = result;
            }
            return result;
        }

        public static IEnumerable<TResult> Select<T1, T2, TResult>(IEnumerable<T1> x1, IEnumerable<T2> x2, Func<T1, T2, TResult> func)
        {
            return x1.SelectMany(p1 => x2.Select(p2 => func(p1, p2)));
        }
        public static IEnumerable<TResult> Select<T1, T2, T3, TResult>(IEnumerable<T1> x1, IEnumerable<T2> x2, IEnumerable<T3> x3, Func<T1, T2, T3, TResult> func)
        {
            return x1.SelectMany(p1 => x2.SelectMany(p2 => x3.Select(p3 => func(p1, p2, p3))));
        }

        public static IEnumerable<TSource> ReplaceWith<TSource, TKey>(this IEnumerable<TSource> source, TSource element, Func<TSource, TKey> keySelector)
        {
            return source.Except(new[] { element }, new KeyEqualityComparer<TSource, TKey>(keySelector)).Concat(new[] { element });
        }

        public static IEnumerable<TSource> ReplaceWith<TSource, TKey>(this IEnumerable<TSource> source, ICollection<TSource> elements, Func<TSource, TKey> keySelector)
        {
            return source.Except(elements, new KeyEqualityComparer<TSource, TKey>(keySelector)).Concat(elements);
        }

        public static IEnumerable<TSource> Except<TSource>(this IEnumerable<TSource> source, TSource element)
        {
            return source.Except(new[] { element });
        }

        public static bool HasElements<T>(this IEnumerable<T> x)
        {
            return x != null && x.Any();
        }

        public static bool HasNoElements<T>(this IEnumerable<T> x)
        {
            return !HasElements(x);
        }

        public static IEnumerable<T> SkipNulls<T>(this IEnumerable<T> source)
            where T: class
        {
            return source.Where(x => x != null);
        }

        public static IEnumerable<T> SkipNulls<T>(this IEnumerable<T?> source)
            where T : struct
        {
            return source.Where(x => x.HasValue).Select(x => x.GetValueOrDefault());
        }

        public static IEnumerable<T> SkipNulls<T, TKey>(this IEnumerable<T> source, Func<T, TKey> keySelector)
            where TKey : class
        {
            return source.Where(x => keySelector(x) != null);
        }

        public static TResult[] SelectArray<T, TResult>(this IEnumerable<T> x, Func<T, TResult> selector)
        {
            return x.Select(selector).ToArray();
        }

        public static void PivotOf2<T>(this ICollection<T> src, Action<T, T> action)
        {
            action(src.First(), src.Skip(1).First());
        }

        public static bool SequencesOrderedEqual<T, TKey, TCmpObj>(this IEnumerable<T> src, IEnumerable<T> upd, Func<T, TKey> orderKeySelector, Func<T, TCmpObj> compareObjectsSelector)
        {
            return src.OrderBy(orderKeySelector).Select(compareObjectsSelector).SequenceEqual(upd.OrderBy(orderKeySelector).Select(compareObjectsSelector));
        }
    }
}