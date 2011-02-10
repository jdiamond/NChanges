using NUnit.Framework;

namespace NChanges.Core.Tests
{
    [TestFixture]
    public class Describe_PadNumbersExtension
    {
        [Test]
        public void It_pads_all_number_groups_in_a_string()
        {
            Assert.AreEqual("0000000001.0000000010", "1.10".PadNumbers());
        }
    }
}