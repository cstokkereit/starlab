namespace Pegasus.Symbols
{
    public class EmptySymbolTests
    {
        private static ISymbol empty = Symbol.Empty;

        [Test]
        public void TestConstructor()
        {
            Assert.IsNotNull(empty);
        }

        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            Assert.IsFalse(empty.Equals("ΔT<sub>1</sub><sup>2</sup>"));
        }

        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            Assert.IsTrue(empty.Equals(Symbol.Empty));
        }

        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            Assert.IsFalse(empty.Equals(new Symbol("Delta", "T", false, false, 1, 2)));
        }

        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            Assert.IsFalse(empty.Equals(null));
        }

        [Test]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var empty2 = empty;

            Assert.IsTrue(empty.Equals(empty2));
        }

        [Test]
        public void TestGetHashCode()
        {
            Assert.AreEqual(0, empty.GetHashCode());
        }

        [Test]
        public void TestToString()
        {
            Assert.AreEqual(string.Empty, empty.ToString());
        }

        [Test]
        public void TestToStringFormattedAsRtf()
        {
            Assert.AreEqual(string.Empty, empty.ToString(new RtfFormatter()));
        }
    }
}
