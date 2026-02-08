using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Nomenclature"/> class.
    /// </summary>
    public class NomenclatureTests
    {
        private Guid id = Guid.Parse("f2ea28f1-4ce5-469f-bfe1-80f4c7c4B6b8");

        /// <summary>
        /// Test that the <see cref="Nomenclature(string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromNameAndDescription()
        {
            // Act
            var nomenclature = new Nomenclature("Nomenclature-1", "This is a nomenclature.");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromName()
        {
            // Act
            var nomenclature = new Nomenclature("Dictionary-1");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature(XmlNomenclature)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromXmlNomenclature()
        {
            // Arrange
            XmlNomenclature xmlNomenclature = new ()
            {
                ID = id,
                Name = "Nomenclature-1",
                Description = "This is a nomenclature"
            };

            // Act
            var nomenclature = new Nomenclature(xmlNomenclature);

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.ID, Is.EqualTo(id));
            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-1"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a nomenclature"));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.Description"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetDescription()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1", "This is a nomenclature.");

            // Assert
            Assert.That(nomenclature.Description, Is.EqualTo("This is a nomenclature."));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.ID"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1");

            // Assert
            Assert.That(nomenclature.ID, Is.Not.EqualTo(Guid.Empty));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.Name"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetName()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1");

            // Assert
            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-1"));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.GetTerm(string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestGetTerm()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1");

            var term1 = new Term("Term-1");
            var term2 = new Term("Term-2");

            nomenclature.Add(term1);
            nomenclature.Add(term2);

            // Assert
            Assert.That(nomenclature.GetTerm("Term-1"), Is.SameAs(term1));
            Assert.That(nomenclature.GetTerm("Term-2"), Is.SameAs(term2));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.Properties"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetTerms()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1");

            var term1 = new Term("Term-1");
            var term2 = new Term("Term-2");
            var term3 = new Term("Term-3");
            var term4 = new Term("Term-4");

            nomenclature.Add(term1);
            nomenclature.Add(term2);
            nomenclature.Add(term3);
            nomenclature.Add(term4);

            // Act
            var terms = new List<Term>(nomenclature.Terms);

            // Assert
            Assert.That(terms.Count, Is.EqualTo(4));
            Assert.That(terms.Contains(term1), Is.True);
            Assert.That(terms.Contains(term2), Is.True);
            Assert.That(terms.Contains(term3), Is.True);
            Assert.That(terms.Contains(term4), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.Add(Term)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1");

            var term1 = new Term("Term-1");
            var term2 = new Term("Term-2");

            // Act
            nomenclature.Add(term1);

            var terms1 = new List<Term>(nomenclature.Terms);

            nomenclature.Add(term2);

            var terms2 = new List<Term>(nomenclature.Terms);

            // Assert
            Assert.That(terms1.Count, Is.EqualTo(1));
            Assert.That(terms1[0], Is.EqualTo(term1));

            Assert.That(terms2.Count, Is.EqualTo(2));
            Assert.That(terms2[1], Is.EqualTo(term2));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.Remove(Term)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRemove()
        {
            // Arrange
            var nomenclature = new Nomenclature("Nomenclature-1");

            var term1 = new Term("Term-1");
            var term2 = new Term("Term-2");
            var term3 = new Term("Term-3");

            nomenclature.Add(term1);
            nomenclature.Add(term2);
            nomenclature.Add(term3);

            Assert.That(nomenclature.Terms.Count, Is.EqualTo(3));

            // Act
            nomenclature.Remove(term2);

            var terms = new List<Term>(nomenclature.Terms);

            // Assert
            Assert.That(terms.Count, Is.EqualTo(2));
            Assert.That(terms[0], Is.EqualTo(term1));
            Assert.That(terms[1], Is.EqualTo(term3));
        }

        /// <summary>
        /// Test that the <see cref="Nomenclature.Remove(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRemoveByName()
        {
            // Arrange
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            var term1 = new Term("Term-1");
            var term2 = new Term("Term-2");
            var term3 = new Term("Term-3");

            nomenclature.Add(term1);
            nomenclature.Add(term2);
            nomenclature.Add(term3);

            var terms1 = new List<Term>(nomenclature.Terms);

            Assert.That(terms1.Count, Is.EqualTo(3));

            // Act
            nomenclature.Remove("Term-1");

            var terms2 = new List<Term>(nomenclature.Terms);

            // Assert
            Assert.That(terms2.Count, Is.EqualTo(2));
            Assert.That(terms2[0], Is.EqualTo(term2));
            Assert.That(terms2[1], Is.EqualTo(term3));
        }
    }
}