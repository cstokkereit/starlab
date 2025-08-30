using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public class PropertyTests
    {
        private Guid id = Guid.Parse("efa0b0a8-6324-4be7-91b7-8c6a9d4511c4");

        [TestMethod]
        public void TestConstructionFromNameAndDescription()
        {
            var property = new Property("Property-1", "This is a property.");

            Assert.IsNotNull(property);
        }

        [TestMethod]
        public void TestConstructionFromName()
        {
            var property = new Property("Property-1");

            Assert.IsNotNull(property);
        }

        public void TestConstructionFromXmlProperty()
        {
            XmlProperty xmlProperty = new()
            {
                ID = id,
                Name = "Property-1",
                Description = "This is a property"
            };

            Property property = new Property(xmlProperty);

            Assert.IsNotNull(property);
            Assert.AreEqual(id, property.ID);
            Assert.AreEqual("Property-1", property.Name);
            Assert.AreEqual("This is a property", property.Description);
        }

        [TestMethod]
        public void TestGetDescription()
        {
            var property = new Property("Property-1", "This is a property.");

            Assert.AreEqual("This is a property.", property.Description);
        }

        [TestMethod]
        public void TestGetId()
        {
            var property = new Property("Property-1");

            Assert.IsNotNull(property.ID);
            Assert.AreNotEqual(Guid.Empty, property.ID);
        }

        [TestMethod]
        public void TestGetName()
        {
            var property = new Property("Property-1");

            Assert.AreEqual("Property-1", property.Name);
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var property = new Property("Property-1");

            Assert.IsFalse(property.Equals("Property-1"));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var property1 = new Property("Property-1");
            var property2 = property1;

            Assert.IsTrue(property1.Equals(property2));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var property1 = new Property("Property-1");
            var property2 = new Property("Property-1");

            Assert.IsFalse(property1.Equals(property2));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNull()
        {
            var property = new Property("Property-1");

            Assert.IsFalse(property.Equals(null));
        }

        [TestMethod]
        public void TestHashCodesDoNotMatchForDifferentProperties()
        {
            var property1 = new Property("Property-1");
            var property2 = new Property("Property-1");

            Assert.AreNotEqual(property1.GetHashCode(), property2.GetHashCode());
        }

        [TestMethod]
        public void TestHashCodesMatchForSameProperties()
        {
            var property1 = new Property("Property-1");
            var property2 = property1;

            Assert.AreEqual(property1.GetHashCode(), property2.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            var property = new Property("Property-1");

            Assert.AreEqual(property.ID.ToString(), property.ToString());
        }
    }
}