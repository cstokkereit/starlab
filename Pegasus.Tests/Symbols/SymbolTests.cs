﻿namespace Pegasus.Symbols
{
    public class SymbolTests
    {
        [Test]
        public void TestConstructorWithStringArgumentsAndEmptySymbol()
        {
            Assert.Throws<ArgumentException>(() => new Symbol(string.Empty, string.Empty, false, false, string.Empty, string.Empty));
        }

        [Test]
        public void TestConstructorWithSymbolArgumentsAndEmptySymbol()
        {
            Assert.Throws<ArgumentException>(() => new Symbol(Symbol.Empty, string.Empty, false, false, Symbol.Empty, Symbol.Empty));
        }

        [Test]
        public void TestConstructorWithStringArgument()
        {
            var symbol = new Symbol("T");

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestConstructorWithStringAndTwoBooleanArguments()
        {
            var symbol = new Symbol("T", false, false);

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestConstructorWithStringTwoBooleanAndTwoIntegerArguments()
        {
            var symbol = new Symbol("T", false, false, 1, 2);

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestConstructorWithStringTwoBooleanAndTwoStringArguments()
        {
            var symbol = new Symbol("T", false, false, "1", "2");

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestConstructorWithStringTwoBooleanAndThreeSymbolArguments()
        {
            var symbol = new Symbol(Symbol.Empty, "T", false, false, Symbol.Empty, Symbol.Empty);

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestConstructorWithTwoStringTwoBooleanAndTwoIntegerArguments()
        {
            var symbol = new Symbol("Δ", "T", false, false, 1, 2);

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestConstructorWithFourStringAndTwoBooleanArguments()
        {
            var symbol = new Symbol("Δ", "T", false, false, "1", "2");

            Assert.IsNotNull(symbol);
        }

        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var symbol = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.IsFalse(symbol.Equals("ΔT<sub>1</sub><sup>2</sup>"));
        }

        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.IsTrue(symbol1.Equals(symbol2));
        }

        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("Delta", "T", false, false, 2, 2);

            Assert.IsFalse(symbol1.Equals(symbol2));
        }

        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            var symbol = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.IsFalse(symbol.Equals(null));
        }

        [Test]
        public void TestEqualsWhenArgumentIsSameObject()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = symbol1;

            Assert.IsTrue(symbol1.Equals(symbol2));
        }

        [Test]
        public void TestHashCodesDoNotMatchForDifferentSymbols()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("T", false, false, 2, 2);

            Assert.AreNotEqual(symbol1.GetHashCode(), symbol2.GetHashCode());
        }

        [Test]
        public void TestHashCodesMatchForSameSymbols()
        {
            var symbol1 = new Symbol("Delta", "T", false, false, 1, 2);
            var symbol2 = new Symbol("Delta", "T", false, false, 1, 2);

            Assert.AreEqual(symbol1.GetHashCode(), symbol2.GetHashCode());
        }

        [Test]
        public void TestToStringForBoldSymbol()
        {
            var symbol = new Symbol("Delta", "F", true, false, "1", string.Empty);

            Assert.AreEqual("Δ<b>F</b><sub>1</sub>", symbol.ToString());
        }

        [Test]
        public void TestToStringForItalicSymbol()
        {
            var symbol = new Symbol("Delta", "T", false, true, "1", string.Empty);

            Assert.AreEqual("Δ<i>T</i><sub>1</sub>", symbol.ToString());
        }

        [Test]
        public void TestToStringForLowerCaseTheta()
        {
            var symbol = new Symbol("theta");

            Assert.AreEqual("θ", symbol.ToString());
        }

        [Test]
        public void TestToStringForUpperCaseTheta()
        {
            var symbol = new Symbol("Theta");

            Assert.AreEqual("Θ", symbol.ToString());
        }

        [Test]
        public void TestToStringForDeltaTheta()
        {
            var symbol = new Symbol("Delta", "theta", false, false);

            Assert.AreEqual("Δθ", symbol.ToString());
        }

        [Test]
        public void TestToStringForThetaSubscriptOne()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, "1", string.Empty);

            Assert.AreEqual("θ<sub>1</sub>", symbol.ToString());
        }

        [Test]
        public void TestToStringForThetaSubscriptOneSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, 1, 2);

            Assert.AreEqual("θ<sub>1</sub><sup>2</sup>", symbol.ToString());
        }

        [Test]
        public void TestToStringForThetaSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, string.Empty, "2");

            Assert.AreEqual("θ<sup>2</sup>", symbol.ToString());
        }

        [Test]
        public void TestToStringFormattedAsRtfForBoldSymbol()
        {
            var symbol = new Symbol("Delta", "F", true, false, "1", string.Empty);

            Assert.AreEqual("Δ\\bF\\b0{\\sub 1}", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForItalicSymbol()
        {
            var symbol = new Symbol("Delta", "T", false, true, "1", string.Empty);

            Assert.AreEqual("Δ\\iT\\i0{\\sub 1}", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForLowerCaseTheta()
        {
            var symbol = new Symbol("theta");

            Assert.AreEqual("θ", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForUpperCaseTheta()
        {
            var symbol = new Symbol("Theta");

            Assert.AreEqual("Θ", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForDeltaTheta()
        {
            var symbol = new Symbol("Delta", "theta", false, false);

            Assert.AreEqual("Δθ", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForThetaSubscriptOne()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, "1", string.Empty);

            Assert.AreEqual("θ{\\sub 1}", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForThetaSubscriptOneSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, 1, 2);

            Assert.AreEqual("θ{\\sub 1}{\\super 2}", symbol.ToString(new RtfFormatter()));
        }

        [Test]
        public void TestToStringFormattedAsRtfForThetaSuperscriptTwo()
        {
            var symbol = new Symbol(string.Empty, "theta", false, false, string.Empty, "2");

            Assert.AreEqual("θ{\\super 2}", symbol.ToString(new RtfFormatter()));
        }
    }
}
