﻿namespace Pegasus.Symbols
{
    [TestClass]
    public class SymbolQuotientTests
    {
        [TestMethod]
        public void TestConstructorWithIEnumerables()
        {
            var numerator = new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") };
            var denominator = new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") };

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.IsNotNull(quotient);
        }

        [TestMethod]
        public void TestConstructorWithIEnumerablesAndSymbol()
        {
            var numerator = new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") };
            var denominator = new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") };

            var quotient = new SymbolQuotient(numerator, denominator, new Symbol("·"));

            Assert.IsNotNull(quotient);
        }

        [TestMethod]
        public void TestConstructorWithSymbolProducts()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") });
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("s", false, false, string.Empty,"3"), new Symbol("A") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.IsNotNull(quotient);
        }

        [TestMethod]
        public void TestConstructorWithSymbolProductAndSymbol()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m") });
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.IsNotNull(quotient);
        }

        [TestMethod]
        public void TestConstructorWithSymbolAndSymbolProduct()
        {
            var numerator = new Symbol("kg");
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.IsNotNull(quotient);
        }

        [TestMethod]
        public void TestConstructorWithSymbols()
        {
            var numerator = new Symbol("m");
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.IsNotNull(quotient);
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var quotient = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.IsFalse(quotient.Equals("(kg m)/s<sup>2</sup>"));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.IsTrue(quotient1.Equals(quotient2));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("m"), new Symbol("s"));

            Assert.IsFalse(quotient1.Equals(quotient2));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNull()
        {
            var quotient = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.IsFalse(quotient.Equals(null));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = quotient1;

            Assert.IsTrue(quotient1.Equals(quotient2));
        }

        [TestMethod]
        public void TestHashCodesDoNotMatchForDifferentSymbolQuotients()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s") }));
            
            Assert.AreNotEqual(quotient1.GetHashCode(), quotient2.GetHashCode());
        }

        [TestMethod]
        public void TestHashCodesMatchForSameSymbolQuotients()
        {
            var quotient1 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));
            var quotient2 = new SymbolQuotient(new Symbol("kg"), new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") }));

            Assert.AreEqual(quotient1.GetHashCode(), quotient2.GetHashCode());
        }

        [TestMethod]
        public void TestToStringWithBracketsInTheDenominator()
        {
            var numerator = new Symbol("kg");
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("kg/(m s<sup>2</sup>)", quotient.ToString());
        }

        [TestMethod]
        public void TestToStringWithBracketsInTheNumerator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m") });
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("(kg m)/s", quotient.ToString());
        }

        [TestMethod]
        public void TestToStringWithMultipleTermsInBothNumeratorAndDenominator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") });
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("(kg m<sup>2</sup>)/(s<sup>3</sup> A)", quotient.ToString());
        }

        [TestMethod]
        public void TestToStringWithSingleTermsInBothNumeratorAndDenominator()
        {
            var numerator = new Symbol("m");
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("m/s", quotient.ToString());
        }

        [TestMethod]
        public void TestToStringFormattedAsRtfWithBracketsInTheDenominator()
        {
            var numerator = new Symbol("kg");
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("m"), new Symbol("s", false, false, string.Empty, "2") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("kg/(m s{\\super 2})", quotient.ToString(new RtfFormatter()));
        }

        [TestMethod]
        public void TestToStringFormattedAsRtfWithBracketsInTheNumerator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m") });
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("(kg m)/s", quotient.ToString(new RtfFormatter()));
        }

        [TestMethod]
        public void TestToStringFormattedAsRtfWithMultipleTermsInBothNumeratorAndDenominator()
        {
            var numerator = new SymbolProduct(new ISymbol[] { new Symbol("kg"), new Symbol("m", false, false, string.Empty, "2") });
            var denominator = new SymbolProduct(new ISymbol[] { new Symbol("s", false, false, string.Empty, "3"), new Symbol("A") });

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("(kg m{\\super 2})/(s{\\super 3} A)", quotient.ToString(new RtfFormatter()));
        }

        [TestMethod]
        public void TestToStringFormattedAsRtfWithSingleTermsInBothNumeratorAndDenominator()
        {
            var numerator = new Symbol("m");
            var denominator = new Symbol("s");

            var quotient = new SymbolQuotient(numerator, denominator);

            Assert.AreEqual("m/s", quotient.ToString(new RtfFormatter()));
        }
    }
}