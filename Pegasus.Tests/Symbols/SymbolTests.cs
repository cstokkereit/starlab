namespace Pegasus.Symbols
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Symbol"/> class.
    /// </summary>
    public class SymbolTests
    {
        /// <summary>
        /// Test that an exception is thrown when the <see cref="Symbol(string, string, bool, bool, string, string)"/> constructor is called with an empty symbol argument.
        /// </summary>
        [Test]
        public void TestConstructorWithStringArgumentsAndEmptySymbol()
        {
            Assert.Throws<ArgumentException>(() => new Symbol(string.Empty, string.Empty, false, false, string.Empty, string.Empty));
        }

        /// <summary>
        /// Test that an exception is thrown when the <see cref="Symbol(ISymbol, string, bool, bool, ISymbol, ISymbol)"/> constructor is called with an empty symbol argument.
        /// </summary>
        [Test]
        public void TestConstructorWithSymbolArgumentsAndEmptySymbol()
        {
            Assert.Throws<ArgumentException>(() => new Symbol(Symbol.Empty, string.Empty, false, false, Symbol.Empty, Symbol.Empty));
        }

        /// <summary>
        /// Test that the <see cref="Symbol(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithStringArgument()
        {
            var symbol = new Symbol("T");

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol(string, bool, bool)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithStringAndTwoBooleanArguments()
        {
            var symbol = new Symbol("T", false, false);

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol(string, bool, bool, int, int)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithStringTwoBooleanAndTwoIntegerArguments()
        {
            var symbol = new Symbol("T", false, false, 1, 2);

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol(string, bool, bool, string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithStringTwoBooleanAndTwoStringArguments()
        {
            var symbol = new Symbol("T", false, false, "1", "2");

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol(Symbol, string, bool, bool, Symbol, Symbol)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithStringTwoBooleanAndThreeSymbolArguments()
        {
            var symbol = new Symbol(Symbol.Empty, "T", false, false, Symbol.Empty, Symbol.Empty);

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol(string, string, bool, bool, int, int)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithTwoStringTwoBooleanAndTwoIntegerArguments()
        {
            var symbol = new Symbol("Δ", "T", false, false, 1, 2);

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol(string, string, bool, bool, string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFourStringAndTwoBooleanArguments()
        {
            var symbol = new Symbol("Δ", "T", false, false, "1", "2");

            Assert.That(symbol, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.Empty"/> property works correctly.
        /// </summary>
        [Test]
        public void TestEmptySymbolPropertyIsNotNull()
        {
            var empty = Symbol.Empty;

            Assert.That(empty, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.Equals(object?)"/> function works correctly when the argument is not a <see cref="Symbol"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var symbol = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.That(symbol.Equals("ΔT<sub>1</sub><sup>2</sup>"), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.Equals(ISymbol?)"/> function works correctly when the argument is an <see cref="ISymbol"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.That(symbol1.Equals(symbol2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.Equals(ISymbol?)"/> function works correctly when the argument is a different <see cref="ISymbol"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("Delta", "T", false, false, 2, 2);

            Assert.That(symbol1.Equals(symbol2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.Equals(ISymbol?)"/> function works correctly when the argument is null.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            var symbol = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.That(symbol.Equals(null), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.Equals(ISymbol?)"/> function works correctly when the argument is the same <see cref="ISymbol"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = symbol1;

            Assert.That(symbol1.Equals(symbol2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Symbol.GetHashCode()"/> function returns different values for different symbols.
        /// </summary>
        [Test]
        public void TestHashCodesDoNotMatchForDifferentSymbols()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("T", false, false, 2, 2);

            Assert.That(symbol2.GetHashCode(), Is.Not.EqualTo(symbol1.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.GetHashCode()"/> function returns the same values for identical symbols.
        /// </summary>
        [Test]
        public void TestHashCodesMatchForSameSymbols()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.That(symbol2.GetHashCode(), Is.EqualTo(symbol1.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the bold flag is set.
        /// </summary>
        [Test]
        public void TestToStringForBoldSymbol()
        {
            var symbol = new Symbol("Delta", "F", true, false, "1", string.Empty);

            Assert.That(symbol.ToString(), Is.EqualTo("Δ<b>F</b><sub>1</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the italic flag is set.
        /// </summary>
        [Test]
        public void TestToStringForItalicSymbol()
        {
            var symbol = new Symbol("Delta", "T", false, true, "1", string.Empty);

            Assert.That(symbol.ToString(), Is.EqualTo("Δ<i>T</i><sub>1</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the symbol is lower case.
        /// </summary>
        [Test]
        public void TestToStringForLowerCaseTheta()
        {
            var symbol = new Symbol("theta");

            Assert.That(symbol.ToString(), Is.EqualTo("θ"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the symbol is upper case.
        /// </summary>
        [Test]
        public void TestToStringForUpperCaseTheta()
        {
            var symbol = new Symbol("Theta");

            Assert.That(symbol.ToString(), Is.EqualTo("Θ"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the symbol has a prefix.
        /// </summary>
        [Test]
        public void TestToStringForDeltaTheta()
        {
            var symbol = new Symbol("Delta", "theta", false, false);

            Assert.That(symbol.ToString(), Is.EqualTo("Δθ"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the symbol has a subscript.
        /// </summary>
        [Test]
        public void TestToStringForThetaSubscriptOne()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, "1", string.Empty);

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sub>1</sub>"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the symbol has a subscript and a superscript.
        /// </summary>
        [Test]
        public void TestToStringForThetaSubscriptOneSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, 1, 2);

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sub>1</sub><sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString()"/> function works correctly when the symbol has a superscript.
        /// </summary>
        [Test]
        public void TestToStringForThetaSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, string.Empty, "2");

            Assert.That(symbol.ToString(), Is.EqualTo("θ<sup>2</sup>"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the bold flag is set and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForBoldSymbol()
        {
            var symbol = new Symbol("Delta", "F", true, false, "1", string.Empty);

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("Δ\\bF\\b0{\\sub 1}"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the italic flag is set and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForItalicSymbol()
        {
            var symbol = new Symbol("Delta", "T", false, true, "1", string.Empty);

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("Δ\\iT\\i0{\\sub 1}"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the symbol is lower case and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForLowerCaseTheta()
        {
            var symbol = new Symbol("theta");

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("θ"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the symbol is upper case and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForUpperCaseTheta()
        {
            var symbol = new Symbol("Theta");

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("Θ"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the symbol has a prefix and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForDeltaTheta()
        {
            var symbol = new Symbol("Delta", "theta", false, false);

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("Δθ"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the symbol has a subscript and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForThetaSubscriptOne()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, "1", string.Empty);

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("θ{\\sub 1}"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the symbol has a subscript and a superscript and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForThetaSubscriptOneSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, 1, 2);

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("θ{\\sub 1}{\\super 2}"));
        }

        /// <summary>
        /// Test that the <see cref="Symbol.ToString(IFormatter)"/> works correctly when the symbol has a superscript and the formatter is an <see cref="RtfFormatter"/>.
        /// </summary>
        [Test]
        public void TestToStringFormattedAsRtfForThetaSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, string.Empty, "2");

            Assert.That(symbol.ToString(new RtfFormatter()), Is.EqualTo("θ{\\super 2}"));
        }
    }
}
