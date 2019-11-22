using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace FrameworkEx.NUnit
{
    public static class MoreAssert
    {
        public static void AllEqual<T, TKey>(TKey valueToCompare, Func<T, TKey> keySelector, IEnumerable<T> src)
        {
            var i = 0;
            foreach (var x in src)
            {
                Assert.AreEqual(keySelector(x), valueToCompare, string.Format("Failure element index: {0}", i));
                i++;
            }
        }
        public static void AreEqual<T1, T2, TKey>(T1 src1, T2 src2, Func<T1, TKey> keySelector1, Func<T2, TKey> keySelector2)
        {
            Assert.AreEqual(keySelector1(src1), keySelector2(src2));
        }
        public static void AreEqual<T, TKey>(T src1, T src2, Func<T, TKey> keySelector)
        {
            Assert.AreEqual(keySelector(src1), keySelector(src2));
        }
    }
}