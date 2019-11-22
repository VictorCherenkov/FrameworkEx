using System;
using System.Collections.Generic;

namespace FrameworkEx
{
    public class KeyEqualityComparer<T, TKey> : IEqualityComparer<T>
    {
        private readonly Func<T, TKey> m_keyExtractor;

        public KeyEqualityComparer(Func<T, TKey> keyExtractor)
        {
            m_keyExtractor = keyExtractor;
        }

        public virtual bool Equals(T x, T y)
        {
            return m_keyExtractor(x).Equals(m_keyExtractor(y));
        }

        public int GetHashCode(T obj)
        {
            return m_keyExtractor(obj).GetHashCode();
        }
    }

}