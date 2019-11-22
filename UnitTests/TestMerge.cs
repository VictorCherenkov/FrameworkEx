using FrameworkEx;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class TestMerge
    {
        [Test]
        public void BasicTests()
        {
            var data = MergeUtils.Merge(new[] {1, 2, 3}, new[] {3, 4, 5}, x => x);
        }
    }
}