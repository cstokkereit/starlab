namespace Pegasus.Symbols
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="SymbolProduct"/> class.
    /// </summary>
    public class SymbolProductTests
    {
        private readonly IEnumerable<ISymbol> symbols = new List<ISymbol>(new[] { new Symbol("kg"), new Symbol("m"), new Symbol("s", false, false, string.Empty, "-2") });

        /// <summary>
        /// Test that the <see cref="SymbolProduct(IEnumerable{ISymbol}"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestIEnumerableConstructor()
        {
            var product = new SymbolProduct(symbols);

            Assert.That(product, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct(IEnumerable{ISymbol}, string"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestIEnumerableAndStringConstructor()
        {
            var product = new SymbolProduct(symbols, "x");

            Assert.That(product, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct(IEnumerable{ISymbol}, ISymbol"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestIEnumerableAndISymbolConstructor()
        {
            var product = new SymbolProduct(symbols, new Symbol("x"));

            Assert.That(product, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.Equals(object?)"/> function works correctly when the argument is not a <see cref="SymbolProduct"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.That(product.Equals("m s<sup>-1</sup>"), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.Equals(ISymbol?)"/> function works correctly when the argument is a <see cref="SymbolProduct"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.That(product1.Equals(product2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.Equals(ISymbol?)"/> function works correctly when the argument is a different <see cref="SymbolProduct"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-2") }));

            Assert.That(product1.Equals(product2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.Equals(ISymbol?)"/> function works correctly when the argument is null.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.That(product.Equals(null), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.Equals(ISymbol?)"/> function works correctly when the argument is the same <see cref="SymbolProduct"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = product1;

            Assert.That(product1.Equals(product2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.GetHashCode()"/> function returns different values for different symbol products.
        /// </summary>
        [Test]
        public void TestHashCodesDoNotMatchForDifferentSymbolProducts()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-2") }));

            Assert.That(product2.GetHashCode(), Is.Not.EqualTo(product1.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.GetHashCode()"/> function returns the same values for identical symbol products.
        /// </summary>
        [Test]
        public void TestHashCodesMatchForSameSymbolProducts()
        {
            var product1 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));
            var product2 = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "-1") }));

            Assert.That(product1.GetHashCode(), Is.EqualTo(product2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains a single term.
        /// </summary>
        [Test]
        public void TestToStringForSingleTerm()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("N") }));

            Assert.That(product.ToString(), Is.EqualTo("N"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains multiple terms.
        /// </summary>
        [Test]
        public void TestToStringForMultipleTerms()
        {
            var product = new SymbolProduct(symbols);

            Assert.That(product.ToString(), Is.EqualTo("kg m s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains multiple terms and the separator has been specified.
        /// </summary>
        [Test]
        public void TestToStringForMultipleTermsWithCrossSeparator()
        {
            var product = new SymbolProduct(symbols, "×");

            Assert.That(product.ToString(), Is.EqualTo("kg×m×s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains multiple terms and the separator has been specified.
        /// </summary>
        [Test]
        public void TestToStringForMultipleTermsWithDotSeparator()
        {
            var product = new SymbolProduct(symbols, "·");

            Assert.That(product.ToString(), Is.EqualTo("kg·m·s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString(IFormatter)"/> function works correctly when the symbol product contains a single term and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForSingleTerm()
        {
            var product = new SymbolProduct(new List<ISymbol>(new[] { new Symbol("N") }));

            Assert.That(product.ToString(new RtfFormatter()), Is.EqualTo("N"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains multiple terms and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForMultipleTerms()
        {
            var product = new SymbolProduct(symbols);

            Assert.That(product.ToString(new RtfFormatter()), Is.EqualTo("kg m s{\\super -2}"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains multiple terms, the separator has been specified and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForMultipleTermsWithCrossSeparator()
        {
            var product = new SymbolProduct(symbols, "×");

            Assert.That(product.ToString(new RtfFormatter()), Is.EqualTo("kg×m×s{\\super -2}"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolProduct.ToString()"/> function works correctly when the symbol product contains multiple terms, the separator has been specified and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForMultipleTermsWithDotSeparator()
        {
            var product = new SymbolProduct(symbols, "·");

            Assert.That(product.ToString(new RtfFormatter()), Is.EqualTo("kg·m·s{\\super -2}"));
        }
    }
}
