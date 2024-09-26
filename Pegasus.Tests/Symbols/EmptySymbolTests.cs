namespace Pegasus.Symbols
{
    [TestClass]
    public class EmptySymbolTests
    {
        private static ISymbol empty = Symbol.Empty;

        [TestMethod]
        public void TestConstructor()
        {
            Assert.IsNotNull(empty);
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            Assert.IsFalse(empty.Equals("ΔT<sub>1</sub><sup>2</sup>"));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsEqual()
        {
            Assert.IsTrue(empty.Equals(Symbol.Empty));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            Assert.IsFalse(empty.Equals(new Symbol("Delta", "T", false, false, 1, 2)));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNull()
        {
            Assert.IsFalse(empty.Equals(null));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var empty2 = empty;

            Assert.IsTrue(empty.Equals(empty2));
        }

        [TestMethod]
        public void TestGetHashCode()
        {
            Assert.AreEqual(0, empty.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual(string.Empty, empty.ToString());
        }

        [TestMethod]
        public void TestToStringFormattedAsRtf()
        {
            Assert.AreEqual(string.Empty, empty.ToString(new RtfFormatter()));
        }
    }
}
