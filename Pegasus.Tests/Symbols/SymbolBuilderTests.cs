namespace Pegasus.Symbols
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="SymbolBuilder"/> class.
    /// </summary>
    public class SymbolBuilderTests
    {
        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbol(string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbol()
        {
            var symbol = SymbolBuilder.CreateSymbol("theta");

            Assert.That(symbol.ToString(), Is.EqualTo("θ"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbol(string, bool, bool)"/> function works correctly when the bold flag is set.
        /// </summary>
        [Test]
        public void TestCreateBoldSymbol()
        {
            var symbol = SymbolBuilder.CreateSymbol("theta", true, false);

            Assert.That(symbol.ToString(), Is.EqualTo("<b>θ</b>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbol(string, bool, bool)"/> function works correctly when the italic flag is set.
        /// </summary>
        [Test]
        public void TestCreateItalicSymbol()
        {
            var symbol = SymbolBuilder.CreateSymbol("theta", false, true);

            Assert.That(symbol.ToString(), Is.EqualTo("<i>θ</i>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(IEnumerable{ISymbol})"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromIEnumerableOfISymbol()
        {
            IEnumerable<ISymbol> symbols = new[] { SymbolBuilder.CreateSymbol("kg"),
                                                   SymbolBuilder.CreateSymbol("m"),
                                                   SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2) };

            var product = SymbolBuilder.CreateProduct(symbols);

            Assert.That(product.ToString(), Is.EqualTo("kg m s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(IEnumerable{string})"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromIEnumerableOfString()
        {
            IEnumerable<string> symbols = new[] { "i", "j", "k" };

            var product = SymbolBuilder.CreateProduct(symbols);

            Assert.That(product.ToString(), Is.EqualTo("i j k"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(IEnumerable{ISymbol}, ISymbol)"/> function works correctly when the separator is specified.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromIEnumerableOfISymbolWithCrossSeparator()
        {
            IEnumerable<ISymbol> symbols = new[] { SymbolBuilder.CreateSymbol("kg"),
                                                   SymbolBuilder.CreateSymbol("m"),
                                                   SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2) };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Cross);

            Assert.That(product.ToString(), Is.EqualTo("kg×m×s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(IEnumerable{ISymbol}, ISymbol)"/> function works correctly when the separator is specified.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromIEnumerableOfISymbolWithDotSeparator()
        {
            IEnumerable<ISymbol> symbols = new[] { SymbolBuilder.CreateSymbol("kg"),
                                                   SymbolBuilder.CreateSymbol("m"),
                                                   SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2) };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Dot);

            Assert.That(product.ToString(), Is.EqualTo("kg·m·s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(IEnumerable{string}, ISymbol)"/> function works correctly when the separator is specified.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromIEnumerableOfStringWithCrossSeparator()
        {
            IEnumerable<string> symbols = new[] { "i", "j", "k" };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Cross);

            Assert.That(product.ToString(), Is.EqualTo("i×j×k"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(IEnumerable{string}, ISymbol)"/> function works correctly when the separator is specified.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromIEnumerableOfStringWithDotSeparator()
        {
            IEnumerable<string> symbols = new[] { "i", "j", "k" };

            var product = SymbolBuilder.CreateProduct(symbols, SymbolBuilder.Dot);

            Assert.That(product.ToString(), Is.EqualTo("i·j·k"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(string[])"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromStringArray()
        {
            var product = SymbolBuilder.CreateProduct("i", "j", "k");

            Assert.That(product.ToString(), Is.EqualTo("i j k"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateProduct(ISymbol[])"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolProductFromISymbolArray()
        {
            var product = SymbolBuilder.CreateProduct(SymbolBuilder.CreateSymbol("kg"),
                                                      SymbolBuilder.CreateSymbol("m"),
                                                      SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, -2));

            Assert.That(product.ToString(), Is.EqualTo("kg m s<sup>-2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateQuotient(ISymbol, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolQuotientFromISymbols()
        {
            var quotient = SymbolBuilder.CreateQuotient(SymbolBuilder.CreateSymbol("m"), SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, 2));

            Assert.That(quotient.ToString(), Is.EqualTo("m/s<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateQuotient(IEnumerable{ISymbol}, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolQuotientFromIEnumerableAndISymbol()
        {
            var quotient = SymbolBuilder.CreateQuotient(new[] { SymbolBuilder.CreateSymbol("kg"), SymbolBuilder.CreateSymbol("m") },
                                                                SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, 2));

            Assert.That(quotient.ToString(), Is.EqualTo("(kg m)/s<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateQuotient(ISymbol, IEnumerable{ISymbol})"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolQuotientFromISymbolAndIEnumerable()
        {
            var quotient = SymbolBuilder.CreateQuotient(SymbolBuilder.CreateSymbol("J"),
                                                        new[] { SymbolBuilder.CreateSymbol("s"), SymbolBuilder.CreateSymbolWithSuperscript("A", false, false, 2) });

            Assert.That(quotient.ToString(), Is.EqualTo("J/(s A<sup>2</sup>)"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateQuotient(IEnumerable{ISymbol}, IEnumerable{ISymbol})"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolQuotientFromIEnumerables()
        {
            var quotient = SymbolBuilder.CreateQuotient(new[] { SymbolBuilder.CreateSymbolWithSuperscript("s", false, false, 4), SymbolBuilder.CreateSymbolWithSuperscript("A", false, false, 2) },
                                                        new[] { SymbolBuilder.CreateSymbol("kg"), SymbolBuilder.CreateSymbolWithSuperscript("m", false, false, 3) });

            Assert.That(quotient.ToString(), Is.EqualTo("(s<sup>4</sup> A<sup>2</sup>)/(kg m<sup>3</sup>)"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateQuotient(int, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolQuotientFromNumerics()
        {
            var quotient = SymbolBuilder.CreateQuotient(3, 4);

            Assert.That(quotient.ToString(), Is.EqualTo("3/4"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateQuotient(string, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolQuotientFromStrings()
        {
            var quotient = SymbolBuilder.CreateQuotient("m", "s");

            Assert.That(quotient.ToString(), Is.EqualTo("m/s"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSubscript(string, bool, bool, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithNumericSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscript("theta", false, false, 1);

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sub>1</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSubscriptAndSuperscript(string, bool, bool, int, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithNumericSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscriptAndSuperscript("alpha", false, false, 1, 2);

            Assert.That(symbol.ToString(), Is.EqualTo("α<sub>1</sub><sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSuperscript(string, bool, bool, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithNumericSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("theta", false, false, 2);

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefix(string, string, bool, bool)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefix()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefix("Delta", "T", false, false);

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixAndSubscript(string, string, bool, bool, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefixAndNumericSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSubscript("Delta", "T", false, false, 1);

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sub>1</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixAndSuperscript(string, string, bool, bool, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefixAndNumericSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSuperscript("Delta", "T", false, false, 2);

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript(string, string, bool, bool, int, int)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefixNumericSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript("Delta", "T", false, false, 1, 2);

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sub>1</sub><sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixAndSubscript(string, string, bool, bool, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefixAndStringSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSubscript("Delta", "T", false, false, "1");

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sub>1</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixAndSuperscript(string, string, bool, bool, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefixAndStringSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSuperscript("Delta", "T", false, false, "2");

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript(string, string, bool, bool, string, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithPrefixStringSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript("Delta", "T", false, false, "1", "2");

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sub>1</sub><sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSuperscript(string, bool, bool, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithProductSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("e", false, false, SymbolBuilder.CreateProduct(SymbolBuilder.CreateSymbol("i"), SymbolBuilder.CreateSymbolWithPrefix("sin", "θ", false, false)));

            Assert.That(symbol.ToString(), Is.EqualTo("e<sup>i sinθ</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSuperscript(string, bool, bool, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithQuotientSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("theta", false, false, SymbolBuilder.CreateQuotient(3, 4));

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sup>3/4</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSubscript(string, bool, bool, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithStringSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscript("theta", false, false, "i");

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sub>i</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSubscriptAndSuperscript(string, bool, bool, string, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithStringSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSubscriptAndSuperscript("alpha", false, false, "i", "2");

            Assert.That(symbol.ToString(), Is.EqualTo("α<sub>i</sub><sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithSuperscript(string, bool, bool, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithStringSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithSuperscript("theta", false, false, "n");

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sup>n</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefix(ISymbol, string, bool, bool)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithSymbolPrefix()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefix(SymbolBuilder.CreateSymbol("Delta"), "T", false, false);

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixAndSubscript(ISymbol, string, bool, bool, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithSymbolPrefixAndSymbolSubscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSubscript(SymbolBuilder.CreateSymbol("Delta"), "T", false, false, SymbolBuilder.CreateSymbol("2"));

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sub>2</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript(ISymbol, string, bool, bool, ISymbol, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithSymbolPrefixSubscriptAndSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixSubscriptAndSuperscript(SymbolBuilder.CreateSymbol("Delta"), "T", false, false, SymbolBuilder.CreateSymbol("1"), SymbolBuilder.CreateSymbol("2"));

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sub>1</sub><sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.CreateSymbolWithPrefixAndSuperscript(ISymbol, string, bool, bool, ISymbol)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSymbolWithSymbolPrefixAndSymbolSuperscript()
        {
            var symbol = SymbolBuilder.CreateSymbolWithPrefixAndSuperscript(SymbolBuilder.CreateSymbol("Delta"), "T", false, false, SymbolBuilder.CreateSymbol("2"));

            Assert.That(symbol.ToString(), Is.EqualTo("ΔT<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.Cross"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetCross()
        {
            Assert.That(SymbolBuilder.Cross.ToString(), Is.EqualTo("×"));
        }

        /// <summary>
        /// Test that the <see cref="SymbolBuilder.Dot"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetDot()
        {
            Assert.That(SymbolBuilder.Dot.ToString(), Is.EqualTo("·"));
        }
    }
}
