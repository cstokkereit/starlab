namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="TermBuilder"/> class.
    /// </summary>
    public class TermBuilderTests
    {
        /// <summary>
        /// Test that the <see cref="TermBuilder()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            // Act
            TermBuilder builder = new TermBuilder();

            // Assert
            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddProperties(IEnumerable{Property})"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddProperties()
        {
            // Arrange
            List<Property> properties =
            [
                new Property("Property-1", "This is a test property"),
                new Property("Property-2", "This is a test property"),
                new Property("Property-3", "This is a test property")
            ];

            // Act
            Term term = new TermBuilder().AddProperties(properties).CreateTerm("Test");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(3));

            Property property1 = term.GetProperty("Property-1");

            Assert.That(property1, Is.Not.Null);
            Assert.That(property1.Name, Is.EqualTo("Property-1"));
            Assert.That(property1.Description, Is.EqualTo("This is a test property"));
            Assert.That(property1.TermID, Is.EqualTo(term.ID));

            Property property2 = term.GetProperty("Property-2");

            Assert.That(property2, Is.Not.Null);
            Assert.That(property2.Name, Is.EqualTo("Property-2"));
            Assert.That(property2.Description, Is.EqualTo("This is a test property"));
            Assert.That(property2.TermID, Is.EqualTo(term.ID));

            Property property3 = term.GetProperty("Property-3");

            Assert.That(property3, Is.Not.Null);
            Assert.That(property3.Name, Is.EqualTo("Property-3"));
            Assert.That(property3.Description, Is.EqualTo("This is a test property"));
            Assert.That(property3.TermID, Is.EqualTo(term.ID));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddProperty(Property)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddProperty()
        {
            // Act
            Term term = new TermBuilder().AddProperty(new Property("Property-1", "This is a test property")).CreateTerm("Test");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term.GetProperty("Property-1");

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo("This is a test property"));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperty(Property)"/> method throws an <see cref="ArgumentNullException"/> when the property being added is <see cref="null"/>.
        /// </summary>
        [Test]
        public void TestAddNullPropertyThrowsException()
        {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Arrange
            var builder = new TermBuilder();

            // Assert

            Assert.Throws<ArgumentNullException>(() => builder.AddProperty((Property)null));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperty(Property)"/> method throws an <see cref="ArgumentException"/> when a property with the same name as the property being added already exists.
        /// </summary>
        [Test]
        public void TestAddExistingPropertyThrowsException()
        {
            // Arrange
            var builder = new TermBuilder();

            builder.AddProperty(new Property("Property-1", "This is a test property"));

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => builder.AddProperty(new Property("Property-1", "This is a test property with the same name.")));

            Assert.That(exception.Message, Is.EqualTo("A property with the name 'Property-1' already exists. (Parameter 'property')"));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperties(IEnumerable{Property}})"/> method throws an <see cref="ArgumentNullException"/> when the argument is <see cref="null"/>.
        /// </summary>
        [Test]
        public void TestAddNullPropertiesThrowsException()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Arrange
            var builder = new TermBuilder();

            // Assert
            Assert.Throws<ArgumentNullException>(() => builder.AddProperties(null));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperties(IEnumerable{Property})"/> method throws an <see cref="ArgumentException"/> when properties contains multiple properties with the same name.
        /// </summary>
        [Test]
        public void TestAddTermsWithDuplicatedNameThrowsException()
        {
            // Arrange
            var builder = new TermBuilder();

            List<Property> properties =
            [
                new Property("Property-1", "This is a test property"),
                new Property("Property-2", "This is a test property"),
                new Property("Property-1", "This is a test property")
            ];

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => builder.AddProperties(properties));

            Assert.That(exception.Message, Is.EqualTo("A property with the name 'Property-1' already exists. (Parameter 'property')"));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperties(IEnumerable{Property})"/> method throws an <see cref="ArgumentException"/> when properties contains a null value.
        /// </summary>
        [Test]
        public void TestAddPropertiesWithNullPropertyThrowsException()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Arrange
            var builder = new TermBuilder();

            List<Property> properties =
            [
                new Property("Property-1", "This is a test property"),
                null,
                new Property("Property-1", "This is a test property")
            ];

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => builder.AddProperties(properties));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperty(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddPropertyWithNameAndDescription()
        {
            // Arrange
            var builder = new TermBuilder();

            // Act
            Term term = builder.AddProperty("Property-1", "This is a test property").CreateTerm("Test");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term.GetProperty("Property-1");

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo("This is a test property"));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.AddProperty(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddPropertyWithNameOnly()
        {
            // Arrange
            var builder = new TermBuilder();

            // Act
            Term term = builder.AddProperty("Property-1").CreateTerm("Test");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term.GetProperty("Property-1");

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo(string.Empty));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestCreateTermWithNameAndDescription()
        {
            // Act
            Term term = new TermBuilder().CreateTerm("Test", "This is a test term");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo("This is a test term"));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.CreateTerm(string, string)"/> function resets the <see cref="TermBuilder"/>.
        /// </summary>
        [Test]
        public void TestCreateTermWithNameAndDescriptionClearsStoredProperties()
        {
            // Arrange
            TermBuilder builder = new TermBuilder();

            Term term = builder.AddProperty("Property-1")
                               .AddProperty("Property-2")
                               .AddProperty("Property-3")
                               .CreateTerm("Term-1", "This is a test term");

            // Act
            term = builder.AddProperty("Property-4")
                          .CreateTerm("Term-2", "This is a test term");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term-2"));
            Assert.That(term.Description, Is.EqualTo("This is a test term"));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term.GetProperty("Property-4");

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-4"));
            Assert.That(property.Description, Is.EqualTo(string.Empty));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.CreateTerm(string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateTermWithNameOnly()
        {
            // Act
            Term term = new TermBuilder().CreateTerm("Test");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="TermBuilder.CreateTerm(string)"/> function resets the <see cref="TermBuilder"/>.
        /// </summary>
        [Test]
        public void TestCreateTermWithNameOnlyClearsStoredProperties()
        {
            // Arrange
            TermBuilder builder = new TermBuilder();

            Term term = builder.AddProperty("Property-1")
                               .AddProperty("Property-2")
                               .AddProperty("Property-3")
                               .CreateTerm("Term-1");

            // Act
            term = builder.AddProperty("Property-4")
                          .CreateTerm("Term-2", "This is a test term");

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term-2"));
            Assert.That(term.Description, Is.EqualTo("This is a test term"));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term.GetProperty("Property-4");

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-4"));
            Assert.That(property.Description, Is.EqualTo(string.Empty));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }
    }
}
