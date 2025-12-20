namespace Stratosoft.Nomenclature.Tests
{
    public  class NumericValueTests
    {
        [Test]
        public void TestCreateNumericValueFromDouble()
        {
            var value = new NumericValue(1.20);

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void TestCreateNumericValueFromInteger()
        {
            var value = new NumericValue(120);

            Assert.That(value, Is.Not.Null);
        }

        [Test]
        public void TestToStringForZeroValue()
        {
            var value = new NumericValue(0);

            Assert.That(value.ToString(), Is.EqualTo("0"));
        }

        [Test]
        public void TestToStringForDoubleValue()
        {
            var value = new NumericValue(3.14159);

            Assert.That(value.ToString(), Is.EqualTo("3.14159"));
        }

        [Test]
        public void TestToStringForIntegerValue()
        {
            var value = new NumericValue(1024);

            Assert.That(value.ToString(), Is.EqualTo("1024"));
        }
    }
}
