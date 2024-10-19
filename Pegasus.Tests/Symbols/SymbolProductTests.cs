namespace Pegasus.Symbols
{
    public class SymbolProductTests
    {
        private readonly IEnumerable<ISymbol> symbols = new List<ISymbol>(new[] { new Symbol("kg"), new Symbol("m"), new Symbol("s", false, false, string.Empty, "-2") });

        [Test]
        public void TestIEnumerableConstructor()
        {
            var product = new SymbolProduct(symbols);

            Assert.IsNotNull(product);
        }

        [Test]
        public void TestIEnumerableAndStringConstructor()
        {
            var product = new SymbolProduct(symbols, "x");

            Assert.IsNotNull(product);
        }

        [Test]
        public void TestIEnumerableAndSymbolgConstructor()
        {
            var product = new SymbolProduct(symbols, new Symbol("x"));

            Assert.IsNotNull(product);
        }

        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.IsFalse(product.Equals("m s<sup>-1</sup>"));
        }

        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.IsTrue(product1.Equals(product2));
        }

        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-2") }));

            Assert.IsFalse(product1.Equals(product2));
        }

        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.IsFalse(product.Equals(null));
        }

        [Test]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = product1;

            Assert.IsTrue(product1.Equals(product2));
        }

        [Test]
        public void TestHashCodesDoNotMatchForDifferentSymbolProducts()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-2") }));

            Assert.AreNotEqual(product1.GetHashCode(), product2.GetHashCode());
        }

        [Test]
        public void TestHashCodesMatchForSameSymbolProducts()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.AreEqual(product1.GetHashCode(), product2.GetHashCode());
        }

        [Test]
        public void TestToStringForSingleTerm()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("N") }));

            Assert.AreEqual("N", product.ToString());
        }

        [Test]
        public void TestToStringForMultipleTerms()
        {
            var product = new SymbolProduct(symbols);

            Assert.AreEqual("kg m s<sup>-2</sup>", product.ToString());
        }

        [Test]
        public void TestToStringForMultipleTermsWithCrossSeparator()
        {
            var product = new SymbolProduct(symbols, "×");

            Assert.AreEqual("kg×m×s<sup>-2</sup>", product.ToString());
        }

        [Test]
        public void TestToStringForMultipleTermsWithDotSeparator()
        {
            var product = new SymbolProduct(symbols, "·");

            Assert.AreEqual("kg·m·s<sup>-2</sup>", product.ToString());
        }

        [Test]
        public void TestToStringFormattedAsRtfForSingleTerm()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("N") }));

            Assert.AreEqual("N", product.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForMultipleTerms()
        {
            var product = new SymbolProduct(symbols);

            Assert.AreEqual("kg m s{\\super -2}", product.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForMultipleTermsWithCrossSeparator()
        {
            var product = new SymbolProduct(symbols, "×");

            Assert.AreEqual("kg×m×s{\\super -2}", product.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForMultipleTermsWithDotSeparator()
        {
            var product = new SymbolProduct(symbols, "·");

            Assert.AreEqual("kg·m·s{\\super -2}", product.ToString(new RtfFormatter()));
        }
    }
}
