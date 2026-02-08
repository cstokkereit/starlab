using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Property"/> class.
    /// </summary>
    public class PropertyTests
    {
        private Guid id = Guid.Parse("efa0b0a8-6324-4be7-91b7-8c6a9d4511c4");

        /// <summary>
        /// Test that the <see cref="Property(string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromNameAndDescription()
        {
            // Act
            var property = new Property("Property-1", "This is a property.");

            // Assert
            Assert.That(property, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Property(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromName()
        {
            // Act
            var property = new Property("Property-1");

            // Assert
            Assert.That(property, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Property(XmlProperty)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromXmlProperty()
        {
            // Arrange
            XmlProperty xmlProperty = new()
            {
                ID = id,
                Name = "Property-1",
                Description = "This is a property"
            };

            // Act
            Property property = new Property(xmlProperty);

            // Assert
            Assert.That(property, Is.Not.Null);
            Assert.That(property.ID, Is.EqualTo(id));
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo("This is a property"));
        }

        /// <summary>
        /// Test that the <see cref="Property.Attach(Term)"/> method works correctly.
        /// </summary>
        public void TestAttach()
        {
            // Arrange
            var term = new Term("Term-1", "This is a term.");
            var property = new Property("Property-1", "This is a property.");

            // Act
            property.Attach(term);

            // Assert
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        /// <summary>
        /// Test that the <see cref="Property.Description"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetDescription()
        {
            // Arrange
            var property = new Property("Property-1", "This is a property.");

            // Assert
            Assert.That(property.Description, Is.EqualTo("This is a property."));
        }

        /// <summary>
        /// Test that the <see cref="Property.ID"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetId()
        {
            // Arrange
            var property = new Property("Property-1");

            // Assign
            Assert.That(property.ID, Is.Not.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// Test that the <see cref="Property.Name"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetName()
        {
            // Arrange
            var property = new Property("Property-1");

            // Assert
            Assert.That(property.Name, Is.EqualTo("Property-1"));
        }

        /// <summary>
        /// Test that the <see cref="Property.Equals(object?))"/> function returns false when the argument is not a <see cref="Property"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            // Arrange
            var property = new Property("Property-1");

            // Assert
            Assert.That(property.Equals("Property-1"), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Property.Equals(Property?))"/> function returns true when the argument is equal to the <see cref="Property"/> under test.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            // Arrange
            var xml = new XmlProperty()
            {
                ID = id,
                Name = "Property-1",
                Description = "This is a test property."
            };

            // Act
            var property1 = new Property(xml);
            var property2 = new Property(xml);

            // Assert
            Assert.That(property1.Equals(property2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Property.Equals(Property?))"/> function returns true when the argument is the same <see cref="Property"/> instance.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsSameInstance()
        {
            // Arrange
            var property1 = new Property("Property-1");
            var property2 = property1;

            // Assert
            Assert.That(property1.Equals(property2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Property.Equals(Property?))"/> function returns false when the argument is not equal to the <see cref="Property"/> under test.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            // Arrange
            var property1 = new Property("Property-1");
            var property2 = new Property("Property-1");

            // Assert
            Assert.That(property1.Equals(property2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Property.Equals(Property?))"/> function returns false when the argument is <see cref="null"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            // Arrange
            var property = new Property("Property-1");

            // Assert
            Assert.That(property.Equals(null), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Property.GetHashCode())"/> function generates different hash codes for properties with a different ID and Name.
        /// </summary>
        [Test]
        public void TestHashCodesDoNotMatchForDifferentProperties()
        {
            // Arrange
            var property1 = new Property("Property-1");
            var property2 = new Property("Property-1");

            // Assert
            Assert.That(property1.GetHashCode(), Is.Not.EqualTo(property2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="Property.GetHashCode())"/> function generates identical hash codes for properties with the same ID and Name.
        /// </summary>
        [Test]
        public void TestHashCodesMatchForSameProperties()
        {
            // Arrange
            var xml1 = new XmlProperty()
            {
                ID = id,
                Name = "Property-1",
                Description = "This is a test property."
            };

            var xml2 = new XmlProperty()
            {
                ID = id,
                Name = "Property-1",
                Description = "This is another test property."
            };

            // Act
            var property1 = new Property(xml1);
            var property2 = new Property(xml2);

            // Assert
            Assert.That(property2.GetHashCode(), Is.EqualTo(property1.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="Property.ToString())"/> function returns a <see cref="string"/> that includes the Name and ID of the <see cref="Property"/>.
        /// </summary>
        [Test]
        public void TestToString()
        {
            // Arrange
            var property = new Property("Property-1");

            // Assert
            Assert.That(property.ToString(), Is.EqualTo($"{property.Name} ({property.ID})"));
        }
    }
}