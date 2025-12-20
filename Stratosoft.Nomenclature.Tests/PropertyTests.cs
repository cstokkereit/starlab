using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    public class PropertyTests
    {
        private Guid id = Guid.Parse("efa0b0a8-6324-4be7-91b7-8c6a9d4511c4");

        [Test]
        public void TestConstructionFromNameAndDescription()
        {
            var property = new Property("Property-1", "This is a property.");

            Assert.That(property, Is.Not.Null);
        }

        [Test]
        public void TestConstructionFromName()
        {
            var property = new Property("Property-1");

            Assert.That(property, Is.Not.Null);
        }

        [Test]
        public void TestConstructionFromXmlProperty()
        {
            XmlProperty xmlProperty = new()
            {
                ID = id,
                Name = "Property-1",
                Description = "This is a property"
            };

            Property property = new Property(xmlProperty);

            Assert.That(property, Is.Not.Null);
            Assert.That(property.ID, Is.EqualTo(id));
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo("This is a property"));
        }

        [Test]
        public void TestGetDescription()
        {
            var property = new Property("Property-1", "This is a property.");

            Assert.That(property.Description, Is.EqualTo("This is a property."));
        }

        [Test]
        public void TestGetId()
        {
            var property = new Property("Property-1");

            Assert.That(property.ID, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void TestGetName()
        {
            var property = new Property("Property-1");

            Assert.That(property.Name, Is.EqualTo("Property-1"));
        }

        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            var property = new Property("Property-1");

            Assert.That(property.Equals("Property-1"), Is.False);
        }

        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            var property1 = new Property("Property-1");
            var property2 = property1;

            Assert.That(property1.Equals(property2), Is.True);
        }

        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            var property1 = new Property("Property-1");
            var property2 = new Property("Property-1");

            Assert.That(property1.Equals(property2), Is.False);
        }

        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            var property = new Property("Property-1");

            Assert.That(property.Equals(null), Is.False);
        }

        [Test]
        public void TestHashCodesDoNotMatchForDifferentProperties()
        {
            var property1 = new Property("Property-1");
            var property2 = new Property("Property-1");

            Assert.That(property1.GetHashCode(), Is.Not.EqualTo(property2.GetHashCode()));
        }

        [Test]
        public void TestHashCodesMatchForSameProperties()
        {
            var property1 = new Property("Property-1");
            var property2 = property1;

            Assert.That(property2.GetHashCode(), Is.EqualTo(property1.GetHashCode()));
        }

        [Test]
        public void TestToString()
        {
            var property = new Property("Property-1");

            Assert.That(property.ToString(), Is.EqualTo(property.ID.ToString()));
        }
    }
}