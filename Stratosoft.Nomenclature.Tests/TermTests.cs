using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Term"/> class.
    /// </summary>
    public class TermTests
    {
        private Guid id = Guid.Parse("0aefa0b8-624a-4a37-926a-ac6a93ee11c4");

        /// <summary>
        /// Test that the <see cref="Term(string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromNameAndDescription()
        {
            // Act
            var term = new Term("Term-1", "This is a term.");

            // Assert
            Assert.That(term, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Term(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromName()
        {
            // Act
            var term = new Term("Term-1");

            // Assert
            Assert.That(term, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Term(XmlTerm)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromXmlTerm()
        {
            // Arrange
            XmlTerm xmlTerm = new()
            {
                ID = id,
                Name = "Term-1",
                Description = "This is a term",
                Properties = new List<XmlProperty>()
            };

            // Act
            var term = new Term(xmlTerm);

            // Assert
            Assert.That(term, Is.Not.Null);
            Assert.That(term.ID, Is.EqualTo(id));
            Assert.That(term.Name, Is.EqualTo("Term-1"));
            Assert.That(term.Description, Is.EqualTo("This is a term"));
            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="Term.Description"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetDescription()
        {
            // Arrange
            var term = new Term("Term-1", "This is a term.");

            // Assert
            Assert.That(term.Description, Is.EqualTo("This is a term."));
        }

        /// <summary>
        /// Test that the <see cref="Term.ID"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetId()
        {
            // Arrange
            var term = new Term("Term-1");

            // Assign
            Assert.That(term.ID, Is.Not.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// Test that the <see cref="Term.Name"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetName()
        {
            // Arrange
            var term = new Term("Term-1");

            // Assert
            Assert.That(term.Name, Is.EqualTo("Term-1"));
        }

        /// <summary>
        /// Test that the <see cref="Term.GetProperty(string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestGetProperty()
        {
            // Arrange
            Property property1 = new Property("Property-1");
            Property property2 = new Property("Property-2");
            Property property3 = new Property("Property-3");

            var term = new TermBuilder()
                .AddProperty(property1)
                .AddProperty(property2)
                .AddProperty(property3)
                .CreateTerm("Term-1");

            // Assert
            Assert.That(term.GetProperty("Property-1"), Is.EqualTo(property1));
            Assert.That(term.GetProperty("Property-2"), Is.EqualTo(property2));
            Assert.That(term.GetProperty("Property-3"), Is.EqualTo(property3));
        }

        /// <summary>
        /// Test that the <see cref="Term.Properties"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetProperties()
        {
            // Arrange
            Property property1 = new Property("Property-1");
            Property property2 = new Property("Property-2");
            Property property3 = new Property("Property-3");

            var term = new TermBuilder()
                .AddProperty(property1)
                .AddProperty(property2)
                .AddProperty(property3)
                .CreateTerm("Term-1");

            // Act
            var properties = new List<Property>(term.Properties);

            // Assert
            Assert.That(properties, Is.Not.Null);
            Assert.That(properties.Count, Is.EqualTo(3));
            Assert.That(properties[0], Is.EqualTo(property1));
            Assert.That(properties[1], Is.EqualTo(property2));
            Assert.That(properties[2], Is.EqualTo(property3));
        }

        /// <summary>
        /// Test that the <see cref="Term.Equals(object?))"/> function returns false when the argument is not a <see cref="Term"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            // Arrange
            var term = new Term("Term-1");

            // Assert
            Assert.That(term.Equals("Term-1"), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Term.Equals(Term?))"/> function returns true when the argument is equal to the <see cref="Term"/> under test.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            // Arrange
            var xml = new XmlTerm()
            {
                ID = id,
                Name = "Term-1",
                Description = "This is a test term."
            };

            // Act
            var term1 = new Term(xml);
            var term2 = new Term(xml);

            // Assert
            Assert.That(term1.Equals(term2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Term.Equals(Term?))"/> function returns true when the argument is the same <see cref="Term"/> instance.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsSameInstance()
        {
            // Arrange
            var term1 = new Term("Term-1");
            var term2 = term1;

            // Assert
            Assert.That(term1.Equals(term2), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Term.Equals(Term?))"/> function returns false when the argument is not equal to the <see cref="Term"/> under test.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            // Arrange
            var term1 = new Term("Term-1");
            var term2 = new Term("Term-1");

            // Assert
            Assert.That(term1.Equals(term2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Term.Equals(Term?))"/> function returns false when the argument is <see cref="null"/>.
        /// </summary>
        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            // Arrange
            var term = new Term("Term-1");

            // Assert
            Assert.That(term.Equals(null), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Term.GetHashCode())"/> function generates different hash codes for terms with a different ID and Name.
        /// </summary>
        [Test]
        public void TestHashCodesDoNotMatchForDifferentTerms()
        {
            // Arrange
            var term1 = new Term("Term-1");
            var term2 = new Term("Term-1");

            // Assert
            Assert.That(term1.GetHashCode(), Is.Not.EqualTo(term2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="Term.GetHashCode())"/> function generates identical hash codes for terms with the same ID and Name.
        /// </summary>
        [Test]
        public void TestHashCodesMatchForSameTerms()
        {
            // Arrange
            var xml1 = new XmlTerm()
            {
                ID = id,
                Name = "Term-1",
                Description = "This is a test term."
            };

            var xml2 = new XmlTerm()
            {
                ID = id,
                Name = "Term-1",
                Description = "This is another test term."
            };

            // Act
            var term1 = new Term(xml1);
            var term2 = new Term(xml2);

            // Assert
            Assert.That(term1.GetHashCode(), Is.EqualTo(term2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="Term.ToString())"/> function returns a <see cref="string"/> that includes the Name and ID of the <see cref="Term"/>.
        /// </summary>
        [Test]
        public void TestToString()
        {
            // Arrange
            var term = new Term("Term-1");

            // Assert
            Assert.That(term.ToString(), Is.EqualTo("Term-1 " + term.ID.ToString()));
        }
    }
}
