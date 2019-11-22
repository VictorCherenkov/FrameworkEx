using System.Collections.Generic;

namespace FrameworkEx
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrNull<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key) where TValue : class
        {
            TValue value;
            dic.TryGetValue(key, out value);
            return value;
        }
        public static TValue? GetNullable<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key) where TValue : struct
        {
            TValue value;
            return dic.TryGetValue(key, out value) ? value : default(TValue?);
        }

        public static TValue GetOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, TValue @default)
        {
            TValue value;
            return dic.TryGetValue(key, out value) ? value : @default;
        }
        public static TValue GetOrThrow<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key, string exceptionMessage)
        {
            TValue obj;
            return dic.TryGetValue(key, out obj) ? obj : Raise.NotSupported<TValue>(exceptionMessage);
        }
    }
}