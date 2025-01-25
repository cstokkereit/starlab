namespace Pegasus.Symbols
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="SymbolQuotient"/> class.
    /// </summary>
    public class SymbolQuotientTests
    {
        /// <summary>
        /// Test that the <see cref="SymbolQuotient(IEnumerable{ISymbol}, IEnumerable{ISymbol})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithIEnumerables()
        {
            var numerator = new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") };
            var denominator = new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") };

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient(IEnumerable{ISymbol}, IEnumerable{ISymbol}, ISymbol)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithIEnumerablesAndSymbol()
        {
            var numerator = new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") };
            var denominator = new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") };

            var quotient = new SymbolQuotient(numerator, denominator, new Symbol("·"));

            Assert.That(quotient, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient(ISymbol, ISymbol)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithSymbolProducts()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") });
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient(ISymbol, ISymbol)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithSymbolProductAndSymbol()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m") });
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient(ISymbol, ISymbol)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithSymbolAndSymbolProduct()
        {
            var numerator = new Symbol("kg");
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient(ISymbol, ISymbol)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithSymbols()
        {
            var numerator = new Symbol("m");
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.Equals(object?)"/> function works correctly when the argument is not a <see cref="SymbolQuotient"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var quotient = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.That(quotient.Equals("(kg m)/s<sup>2</sup>"), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.Equals(ISymbol?)"/> function works correctly when the argument is a <see cref="SymbolQuotient"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.That(quotient1.Equals(quotient2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.Equals(ISymbol?)"/> function works correctly when the argument is a different <see cref="SymbolQuotient"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("m"), new Symbol("s"));

            Assert.That(quotient1.Equals(quotient2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.Equals(ISymbol?)"/> function works correctly when the argument is null.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            var quotient = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.That(quotient.Equals(null), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.Equals(ISymbol?)"/> function works correctly when the argument is the same <see cref="SymbolQuotient"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = quotient1;

            Assert.That(quotient1.Equals(quotient2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.GetHashCode()"/> function returns different values for different symbol quotients.
        /// </summary>
        [Test]
        public void TestHashCodesDoNotMatchForDifferentSymbolQuotients()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s") }));

            Assert.That(quotient2.GetHashCode(), Is.Not.EqualTo(quotient1.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.GetHashCode()"/> function returns the same values for identical symbol quotients.
        /// </summary>
        [Test]
        public void TestHashCodesMatchForSameSymbolQuotients()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.That(quotient2.GetHashCode(), Is.EqualTo(quotient1.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString()"/> function works correctly when there are brackets in the denominator.
        /// </summary>
        [Test]
        public void TestToStringWithBracketsInTheDenominator()
        {
            var numerator = new Symbol("kg");
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(), Is.EqualTo("kg/(m s<sup>2</sup>)"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString()"/> function works correctly when there are brackets in the numerator.
        /// </summary>
        [Test]
        public void TestToStringWithBracketsInTheNumerator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m") });
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(), Is.EqualTo("(kg m)/s"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString()"/> function works correctly when there are multiple terms in both the numerator and the denominator.
        /// </summary>
        [Test]
        public void TestToStringWithMultipleTermsInBothNumeratorAndDenominator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") });
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(), Is.EqualTo("(kg m<sup>2</sup>)/(s<sup>3</sup> A)"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString()"/> function works correctly when there is a single term in both the numerator and the denominator.
        /// </summary>
        [Test]
        public void TestToStringWithASingleTermInBothNumeratorAndDenominator()
        {
            var numerator = new Symbol("m");
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(), Is.EqualTo("m/s"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString(IFormatter)"/> function works correctly when there are brackets in the denominator and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfWithBracketsInTheDenominator()
        {
            var numerator = new Symbol("kg");
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(new RtfFormatter()), Is.EqualTo("kg/(m s{\\super 2})"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString(IFormatter)"/> function works correctly when there are brackets in the numerator and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfWithBracketsInTheNumerator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m") });
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(new RtfFormatter()), Is.EqualTo("(kg m)/s"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString(IFormatter)"/> function works correctly when there are multiple terms in both the numerator and the denominator and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfWithMultipleTermsInBothNumeratorAndDenominator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") });
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(new RtfFormatter()), Is.EqualTo("(kg m{\\super 2})/(s{\\super 3} A)"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolQuotient.ToString(IFormatter)"/> function works correctly when there is a single term in both the numerator and the denominator and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfWithASingleTermInBothNumeratorAndDenominator()
        {
            var numerator = new Symbol("m");
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.That(quotient.ToString(new RtfFormatter()), Is.EqualTo("m/s"));
        }
    }
}
