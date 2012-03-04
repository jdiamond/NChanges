using NUnit.Framework;
using System.Collections.Generic;

namespace NChanges.Core.Tests
{
    [TestFixture]
    public class Describe_TypeHelper
    {
        [Test]
        public void It_converts_runtime_type_names_to_friendly_names()
        {
            Assert.AreEqual("int", TypeHelpers.NormalizeTypeName(typeof(int).FullName));
            Assert.AreEqual("int[]", TypeHelpers.NormalizeTypeName(typeof(int[]).FullName));
            Assert.AreEqual("int?", TypeHelpers.NormalizeTypeName(typeof(int?).FullName));
            Assert.AreEqual("List<int>", TypeHelpers.NormalizeTypeName(typeof(List<int>).FullName));
            Assert.AreEqual("Dictionary<int, string>", TypeHelpers.NormalizeTypeName(typeof(Dictionary<int, string>).FullName));
        }
    }
}