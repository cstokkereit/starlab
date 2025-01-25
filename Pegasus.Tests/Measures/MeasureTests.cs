using Pegasus.Symbols;
using Pegasus.Units;

namespace Pegasus.Measures
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Measure"/> class.
    /// </summary>
    public class MeasureTests
    {
        /// <summary>
        /// Test that the <see cref="Measure.ToString()"/> function works correctly when the SI unit prefix is <see cref="SIUnitPrefixes.Kilo"/>.
        /// </summary>
        [Test]
        public void TestToStringForKiloMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Kilo));
            Assert.That(d.ToString(), Is.EqualTo("23.5 km"));
        }

        /// <summary>
        /// Test that the <see cref="Measure.Value"/> property works correctly when the SI unit prefix is <see cref="SIUnitPrefixes.Kilo"/>.
        /// </summary>
        [Test]
        public void TestGetValueForKiloMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Kilo));
            Assert.That(d.Value, Is.EqualTo(23.5));
        }

        /// <summary>
        /// Test that the <see cref="Measure.ToString()"/> function works correctly when there is no SI unit prefix.
        /// </summary>
        [Test]
        public void TestToStringForMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m")));
            Assert.That(d.ToString(), Is.EqualTo("23.5 m"));
        }

        /// <summary>
        /// Test that the <see cref="Measure.Value"/> property works correctly when there is no SI unit prefix.
        /// </summary>
        [Test]
        public void TestGetValueForMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m")));
            Assert.That(d.Value, Is.EqualTo(23.5));
        }

        /// <summary>
        /// Test that the <see cref="Measure.ToString()"/> function works correctly when the SI unit prefix is <see cref="SIUnitPrefixes.Micro"/>.
        /// </summary>
        [Test]
        public void TestToStringForMicroMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Micro));
            Assert.That(d.ToString(), Is.EqualTo("23.5 μm"));
        }

        /// <summary>
        /// Test that the <see cref="Measure.Value"/> property works correctly when the SI unit prefix is <see cref="SIUnitPrefixes.Micro"/>.
        /// </summary>
        [Test]
        public void TestGetValueForMicroMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Micro));
            Assert.That(d.Value, Is.EqualTo(23.5));
        }

        /// <summary>
        /// Test that implicit conversion from a <see cref="Measure"/> to a <see cref="double"/> works correctly.
        /// </summary>
        [Test]
        public void TestImplicitConversionToDouble()
        {
            var d0 = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Micro));

            double d1 = d0;

            Assert.That(d1, Is.EqualTo(0.0000235));
        }

        /// <summary>
        /// Test that addition of one <see cref="Measure"/> to another works correctly.
        /// </summary>
        [Test]
        public void TestAddition()
        {
            var d0 = new Measure(2, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Micro));
            var d1 = new Measure(5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.Micro));

            var d2 = d0 + d1;

            Assert.That(d2, Is.EqualTo(0.000007).Within(Math.Pow(10, -21)));
        }
    }
}