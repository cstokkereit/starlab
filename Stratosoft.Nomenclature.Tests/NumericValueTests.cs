using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public  class NumericValueTests
    {
        [TestMethod]
        public void TestCreateNumericValueFromDouble()
        {
            var value = new NumericValue(1.20);

            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void TestCreateNumericValueFromInteger()
        {
            var value = new NumericValue(120);

            Assert.IsNotNull(value);
        }

        [TestMethod]
        public void TestToStringForZeroValue()
        {
            var value = new NumericValue(0);

            Assert.AreEqual("0", value.ToString());
        }

        [TestMethod]
        public void TestToStringForDoubleValue()
        {
            var value = new NumericValue(3.14159);

            Assert.AreEqual("3.14159", value.ToString());
        }

        [TestMethod]
        public void TestToStringForIntegerValue()
        {
            var value = new NumericValue(1024);

            Assert.AreEqual("1024", value.ToString());
        }
    }
}
