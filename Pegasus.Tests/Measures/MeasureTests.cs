using Pegasus.Symbols;
using Pegasus.Units;

namespace Pegasus.Measures
{
    public class MeasureTests
    {
        [Test]
        public void TestToStringForKiloMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.kilo));
            Assert.AreEqual("23.5 km", d.ToString());
        }

        [Test]
        public void TestGetValueForKiloMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.kilo));
            Assert.AreEqual(23.5, d.Value);
        }

        [Test]
        public void TestToStringForMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m")));
            Assert.AreEqual("23.5 m", d.ToString());
        }

        [Test]
        public void TestGetValueForMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m")));
            Assert.AreEqual(23.5, d.Value);
        }

        [Test]
        public void TestToStringForMicroMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.micro));
            Assert.AreEqual("23.5 μm", d.ToString());
        }

        [Test]
        public void TestGetValueForMicroMetre()
        {
            var d = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.micro));
            Assert.AreEqual(23.5, d.Value);
        }

        [Test]
        public void TestImplicitConversionToDouble()
        {
            var d0 = new Measure(23.5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.micro));

            double d1 = d0;

            Assert.AreEqual(0.0000235, d1);
        }

        [Test]
        public void TestAddition()
        {
            var d0 = new Measure(2, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.micro));
            var d1 = new Measure(5, new SIUnit("metre", new Symbol("m"), SIUnitPrefixes.micro));

            var d2 = d0 + d1;

            Assert.AreEqual(0.000007, d2, Math.Pow(10, -21));
        }
    }
}