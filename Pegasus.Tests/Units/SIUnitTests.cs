﻿namespace Pegasus.Units
{
    public class SIUnitTests
    {
        [Test]
        public void TestGetMultiplierForYocto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.yocto);
            Assert.AreEqual(0.000000000000000000000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForZepto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.zepto);
            Assert.AreEqual(0.000000000000000000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForAtto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.atto);
            Assert.AreEqual(0.000000000000000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForFemto()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.femto);
            Assert.AreEqual(0.000000000000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForPico()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.pico);
            Assert.AreEqual(0.000000000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForNano()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.nano);
            Assert.AreEqual(0.000000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForMicro()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.micro);
            Assert.AreEqual(0.000001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForMilli()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.milli);
            Assert.AreEqual(0.001, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForNone()
        {
            var unit = new SIUnit("metre", "m");
            Assert.AreEqual(1, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForKilo()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.kilo);
            Assert.AreEqual(1000, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForMega()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.mega);
            Assert.AreEqual(1000000, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForGiga()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.giga);
            Assert.AreEqual(1000000000, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForTera()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.tera);
            Assert.AreEqual(1000000000000, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForPeta()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.peta);
            Assert.AreEqual(1000000000000000, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForExa()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.exa);
            Assert.AreEqual(1000000000000000000, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForZetta()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.zetta);
            Assert.AreEqual(1000000000000000000000d, unit.Multiplier);
        }

        [Test]
        public void TestGetMultiplierForYotta()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.yotta);
            Assert.AreEqual(1000000000000000000000000d, unit.Multiplier);
        }

        [Test]
        public void TestToStringForMicro()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.micro);
            Assert.AreEqual("μm", unit.ToString());
        }

        [Test]
        public void TestToStringForNone()
        {
            var unit = new SIUnit("metre", "m");
            Assert.AreEqual("m", unit.ToString());
        }

        [Test]
        public void TestToStringForMega()
        {
            var unit = new SIUnit("metre", "m", SIUnitPrefixes.mega);
            Assert.AreEqual("Mm", unit.ToString());
        }
    }
}