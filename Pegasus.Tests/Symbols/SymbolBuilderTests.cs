﻿namespace Pegasus.Symbols
{
    [TestClass]
    public class SymbolBuilderTests
    {
        [TestMethod]
        public void TestCreateSymbol()
        {
            var symbol = SymbolBuilder.CreateSymbol("theta");

            Assert.AreEqual("θ", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateBoldSymbol()
        {
            var symbol = SymbolBuilder.CreateSymbol("theta", true, false);

            Assert.AreEqual("<b>θ</b>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateItalicSymbol()
        {
            var symbol = SymbolBuilder.CreateSymbol("theta", false, true);

            Assert.AreEqual("<i>θ</i>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromIEnumerableOfISymbol()
        {
            IEnumerable<ISymbol> symbols = new[] { SymbolBuilder.CreateSymbol("kg"),
                                                   SymbolBuilder.CreateSymbol("m"),
                                                   SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2) };

            var product = SymbolBuilder.CreateProduct(symbols);

            Assert.AreEqual("kg m s<sup>-2</sup>", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromIEnumerableOfString()
        {
            IEnumerable<string> symbols = new[] { "i", "j", "k" };

            var product = SymbolBuilder.CreateProduct(symbols);

            Assert.AreEqual("i j k", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromIEnumerableOfISymbolWithCrossSeparator()
        {
            IEnumerable<ISymbol> symbols = new[] { SymbolBuilder.CreateSymbol("kg"),
                                                   SymbolBuilder.CreateSymbol("m"),
                                                   SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2) };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Cross);

            Assert.AreEqual("kg×m×s<sup>-2</sup>", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromIEnumerableOfISymbolWithDotSeparator()
        {
            IEnumerable<ISymbol> symbols = new[] { SymbolBuilder.CreateSymbol("kg"),
                                                   SymbolBuilder.CreateSymbol("m"),
                                                   SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2) };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Dot);

            Assert.AreEqual("kg·m·s<sup>-2</sup>", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromIEnumerableOfStringWithCrossSeparator()
        {
            IEnumerable<string> symbols = new[] { "i", "j", "k" };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Cross);

            Assert.AreEqual("i×j×k", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromIEnumerableOfStringWithDotSeparator()
        {
            IEnumerable<string> symbols = new[] { "i", "j", "k" };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Dot);

            Assert.AreEqual("i·j·k", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromStringArray()
        {
            var product = SymbolBuilder.CreateProduct("i", "j", "k");

            Assert.AreEqual("i j k", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolProductFromISymbolArray()
        {
            var product = SymbolBuilder.CreateProduct(SymbolBuilder.CreateSymbol("kg"),
                                                      SymbolBuilder.CreateSymbol("m"),
                                                      SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2));

            Assert.AreEqual("kg m s<sup>-2</sup>", product.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolQuotientFromISymbols()
        {
            var quotient = SymbolBuilder.CreateQuotient(SymbolBuilder.CreateSymbol("m"), SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, 2));

            Assert.AreEqual("m/s<sup>2</sup>", quotient.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolQuotientFromIEnumerableAndISymbol()
        {
            var quotient = SymbolBuilder.CreateQuotient(new[] { SymbolBuilder.CreateSymbol("kg"), SymbolBuilder.CreateSymbol("m") }, 
                                                                SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, 2));

            Assert.AreEqual("(kg m)/s<sup>2</sup>", quotient.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolQuotientFromISymbolAndIEnumerable()
        {
            var quotient = SymbolBuilder.CreateQuotient(SymbolBuilder.CreateSymbol("J"), 
                                                        new[] { SymbolBuilder.CreateSymbol("s"), SymbolBuilder.CreateSymbolWithSuperscript("A", false, false, 2) });

            Assert.AreEqual("J/(s A<sup>2</sup>)", quotient.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolQuotientFromIEnumerables()
        {
            var quotient = SymbolBuilder.CreateQuotient(new[] { SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, 4), SymbolBuilder.CreateSymbolWithSuperscript("A", false, false, 2) },
                                                        new[] { SymbolBuilder.CreateSymbol("kg"), SymbolBuilder.CreateSymbolWithSuperscript("m", false, false, 3) });

            Assert.AreEqual("(s<sup>4</sup> A<sup>2</sup>)/(kg m<sup>3</sup>)", quotient.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolQuotientFromNumerics()
        {
            var quotient = SymbolBuilder.CreateQuotient(3, 4);

            Assert.AreEqual("3/4", quotient.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolQuotientFromStrings()
        {
            var quotient = SymbolBuilder.CreateQuotient("m", "s");

            Assert.AreEqual("m/s", quotient.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithNumericSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscript("theta", false, false, 1);

            Assert.AreEqual("θ<sub>1</sub>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithNumericSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscriptAndSuperscript("alpha", false, false, 1, 2);

            Assert.AreEqual("α<sub>1</sub><sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithNumericSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("theta", false, false, 2);

            Assert.AreEqual("θ<sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefix()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefix("Delta", "T", false, false);

            Assert.AreEqual("ΔT", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefixAndNumericSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSubscript("Delta", "T", false, false, 1);

            Assert.AreEqual("ΔT<sub>1</sub>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefixAndNumericSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSuperscript("Delta", "T", false, false, 2);

            Assert.AreEqual("ΔT<sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefixNumericSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript("Delta", "T", false, false, 1, 2);

            Assert.AreEqual("ΔT<sub>1</sub><sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefixAndStringSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSubscript("Delta", "T", false, false, "1");

            Assert.AreEqual("ΔT<sub>1</sub>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefixAndStringSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSuperscript("Delta", "T", false, false, "2");

            Assert.AreEqual("ΔT<sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithPrefixStringSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript("Delta", "T", false, false, "1", "2");

            Assert.AreEqual("ΔT<sub>1</sub><sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithProductSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("e", false, false, SymbolBuilder.CreateProduct(SymbolBuilder.CreateSymbol("i"), SymbolBuilder.CreateSymbolWithPrefix("sin", "θ", false, false)));

            Assert.AreEqual("e<sup>i sinθ</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithQuotientSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("theta", false, false, SymbolBuilder.CreateQuotient(3, 4));

            Assert.AreEqual("θ<sup>3/4</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithStringSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscript("theta", false, false, "i");

            Assert.AreEqual("θ<sub>i</sub>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithStringSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscriptAndSuperscript("alpha", false, false, "i", "2");

            Assert.AreEqual("α<sub>i</sub><sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithStringSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("theta", false, false, "n");

            Assert.AreEqual("θ<sup>n</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithSymbolPrefix()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefix(SymbolBuilder.CreateSymbol("Delta"), "T", false, false);

            Assert.AreEqual("ΔT", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithSymbolPrefixAndSymbolSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSubscript(SymbolBuilder.CreateSymbol("Delta"), "T", false, false, SymbolBuilder.CreateSymbol("2"));

            Assert.AreEqual("ΔT<sub>2</sub>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithSymbolPrefixSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript(SymbolBuilder.CreateSymbol("Delta"), "T", false, false, SymbolBuilder.CreateSymbol("1"), SymbolBuilder.CreateSymbol("2"));

            Assert.AreEqual("ΔT<sub>1</sub><sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestCreateSymbolWithSymbolPrefixAndSymbolSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSuperscript(SymbolBuilder.CreateSymbol("Delta"), "T", false, false, SymbolBuilder.CreateSymbol("2"));

            Assert.AreEqual("ΔT<sup>2</sup>", symbol.ToString());
        }

        [TestMethod]
        public void TestGetCross()
        {
            Assert.AreEqual("×", SymbolBuilder.Cross.ToString());
        }

        [TestMethod]
        public void TestGetDot()
        {
            Assert.AreEqual("·", SymbolBuilder.Dot.ToString());
        }
    }
}