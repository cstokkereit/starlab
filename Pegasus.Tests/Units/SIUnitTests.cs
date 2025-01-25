namespace Pegasus.Units
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="SIUnit"/> class.
    /// </summary>
    public class SIUnitTests
    {
        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Yocto"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForYocto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Yocto);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000000000000000000000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Zepto"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForZepto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Zepto);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000000000000000000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Atto"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForAtto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Atto);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000000000000000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Femto"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForFemto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Femto);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000000000000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Pico"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForPico()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Pico);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000000000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Nano"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForNano()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Nano);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Micro"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForMicro()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Micro);
            Assert.That(unit.Multiplier, Is.EqualTo(0.000001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Milli"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForMilli()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Milli);
            Assert.That(unit.Multiplier, Is.EqualTo(0.001));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is not specified.
        /// </summary>
        [Test]
        public void TestGetMultiplierForNone()
        {
            var unit = new SIUnit("metre", "m");
            Assert.That(unit.Multiplier, Is.EqualTo(1));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Kilo"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForKilo()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Kilo);
            Assert.That(unit.Multiplier, Is.EqualTo(1000));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Mega"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForMega()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Mega);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Giga"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForGiga()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Giga);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000000));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Tera"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForTera()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Tera);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000000000));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Peta"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForPeta()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Peta);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000000000000));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Exa"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForExa()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Exa);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000000000000000));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Zetta"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForZetta()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Zetta);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000000000000000000d));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.Multiplier"/> property returns the correct value when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Yotta"/>.
        /// </summary>
        [Test]
        public void TestGetMultiplierForYotta()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Yotta);
            Assert.That(unit.Multiplier, Is.EqualTo(1000000000000000000000000d));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.ToString()"/> function works correctly when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Micro"/>.
        /// </summary>
        [Test]
        public void TestToStringForMicro()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Micro);
            Assert.That(unit.ToString(), Is.EqualTo("μm"));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.ToString()"/> function works correctly when the SI unit prefix is not specified.
        /// </summary>
        [Test]
        public void TestToStringForNone()
        {
            var unit = new SIUnit("metre", "m");
            Assert.That(unit.ToString(), Is.EqualTo("m"));
        }

        /// <summary>
        /// Test that the <see cref="SIUnit.ToString()"/> function works correctly when the SI unit prefix is specified as <see cref="SIUnitPrefixes.Mega"/>.
        /// </summary>
        [Test]
        public void TestToStringForMega()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.Mega);
            Assert.That(unit.ToString(), Is.EqualTo("Mm"));
        }
    }
}