using System;
using FrameworkEx;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class TestsDirectoryEx
    {
        [Test]
        public void Test()
        {
            var result = DirectoryEx.CalcSize(@"C:\Amat");
            Console.WriteLine(BytesConverter.GBytes(result));
        }
    }
}
