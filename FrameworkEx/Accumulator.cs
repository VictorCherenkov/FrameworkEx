using System.Collections.Generic;
using System.Linq;

namespace FrameworkEx
{
    /// <summary>
    /// Accumulates values and collections. Emulates property bag functionality.
    /// </summary>
    public class Accumulator
    {
        private readonly Dictionary<string, object> m_values = new Dictionary<string, object>();

        public void AccumulateValue<T>(string key, T value)
        {
            m_values[key] = value;
        }

        public void AccumulateCollectionElement<T>(string key, T collectionValue)
        {
            m_values
                .GetOrCreate(key, () => new List<T>().CastTo<object>())
                .CastTo<List<T>>()
                .Add(collectionValue);
        }

        public T GetElement<T>(string key, T defaultValue)
        {
            return (m_values.GetOrNull(key) ?? defaultValue).CastTo<T>();
        }

        public T GetElement<T>(string key)
        {
            return GetElement(key, default(T));
        }

        public IEnumerable<T> GetCollection<T>(string key, List<T> defaultValue)
        {
            object value;
            var isGot = m_values.TryGetValue(key, out value);
            return isGot ? value.CastTo<List<object>>().Cast<T>() : defaultValue;
        }

        public IEnumerable<T> GetCollection<T>(string key)
        {
            return GetCollection(key, new List<T>());
        }
    }
}