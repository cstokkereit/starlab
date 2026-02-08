namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="NomenclatureBuilder"/> class.
    /// </summary>
    public class NomenclatureBuilderTests
    {
        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            // Act
            var builder = new NomenclatureBuilder();
            
            // Assert
            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerms(IEnumerable{Term})"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddTerms()
        {
            // Arrange
            List<Term> terms =
            [
                new Term("Term-1", "This is a test term"),
                new Term("Term-2", "This is a test term"),
                new Term("Term-3", "This is a test term")
            ];

            // Act
            var nomenclature = new NomenclatureBuilder().AddTerms(terms).CreateNomenclature("Test");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(3));

            var term1 = nomenclature.GetTerm("Term-1");

            Assert.That(term1, Is.Not.Null);
            Assert.That(term1.Name, Is.EqualTo("Term-1"));
            Assert.That(term1.Description, Is.EqualTo("This is a test term"));
            Assert.That(term1.NomenclatureID, Is.EqualTo(nomenclature.ID));

            var term2 = nomenclature.GetTerm("Term-2");

            Assert.That(term2, Is.Not.Null);
            Assert.That(term2.Name, Is.EqualTo("Term-2"));
            Assert.That(term2.Description, Is.EqualTo("This is a test term"));
            Assert.That(term2.NomenclatureID, Is.EqualTo(nomenclature.ID));

            var term3 = nomenclature.GetTerm("Term-3");

            Assert.That(term3, Is.Not.Null);
            Assert.That(term3.Name, Is.EqualTo("Term-3"));
            Assert.That(term3.Description, Is.EqualTo("This is a test term"));
            Assert.That(term3.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerms(IEnumerable{Term}})"/> method throws an <see cref="ArgumentNullException"/> when the argument is <see cref="null"/>.
        /// </summary>
        [Test]
        public void TestAddNullTermsThrowsException()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Arrange
            var builder = new NomenclatureBuilder();

            // Assert
            Assert.Throws<ArgumentNullException>(() => builder.AddTerms(null));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerms(IEnumerable{Term})"/> method throws an <see cref="ArgumentException"/> when terms contains multiple terms with the same name.
        /// </summary>
        [Test]
        public void TestAddTermsWithDuplicatedNameThrowsException()
        {
            // Arrange
            var builder = new NomenclatureBuilder();

            List<Term> terms =
            [
                new Term("Term-1", "This is a test term"),
                new Term("Term-2", "This is a test term"),
                new Term("Term-1", "This is a test term")
            ];

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => builder.AddTerms(terms));

            Assert.That(exception.Message, Is.EqualTo("A term with the name 'Term-1' already exists. (Parameter 'term')"));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerms(IEnumerable{Term})"/> method throws an <see cref="ArgumentException"/> when terms contains a null value.
        /// </summary>
        [Test]
        public void TestAddTermsWithNullTermThrowsException()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Arrange
            var builder = new NomenclatureBuilder();

            List<Term> terms =
            [
                new Term("Term-1", "This is a test term"),
                null,
                new Term("Term-1", "This is a test term")
            ];

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(() => builder.AddTerms(terms));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerm(Term)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddTerm()
        {
            // Act
            var nomenclature = new NomenclatureBuilder().AddTerm(new TermBuilder().CreateTerm("Term")).CreateNomenclature("Test");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            var term = nomenclature.GetTerm("Term");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));
            Assert.That(term.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerm(Term)"/> method throws an <see cref="ArgumentNullException"/> when the term being added is <see cref="null"/>.
        /// </summary>
        [Test]
        public void TestAddNullTermThrowsException()
        {
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Arrange
            var builder = new NomenclatureBuilder();

            // Assert
            Assert.Throws<ArgumentNullException>(() => builder.AddTerm(null));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.AddTerm(Term)"/> method throws an <see cref="ArgumentException"/> when a term with the same name as the term being added already exists.
        /// </summary>
        [Test]
        public void TestAddExistingTermThrowsException()
        {
            // Arrange
            var nomenclatureBuilder = new NomenclatureBuilder();
            var termBuilder = new TermBuilder();

            nomenclatureBuilder.AddTerm(termBuilder.CreateTerm("Term-1", "This is a test term"));

            // Assert
            var exception = Assert.Throws<ArgumentException>(() => nomenclatureBuilder.AddTerm(termBuilder.CreateTerm("Term-1", "This is a test term with the same name")));

            Assert.That(exception.Message, Is.EqualTo("A term with the name 'Term-1' already exists. (Parameter 'term')"));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.CreateNomenclature(string, string)"/> function works correctly when no terms have been added.
        /// </summary>
        [Test]
        public void TestCreateEmptyNomenclatureWithNameAndDescription()
        {
            // Act
            var nomenclature = new NomenclatureBuilder().CreateNomenclature("Test", "This is a test nomenclature");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a test nomenclature"));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.CreateNomenclature(string)"/> function works correctly when no terms have been added.
        /// </summary>
        [Test]
        public void TestCreateEmptyNomenclatureWithNameOnly()
        {
            // Act
            var nomenclature = new NomenclatureBuilder().CreateNomenclature("Test");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.CreateNomenclature(string, string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateNomenclatureWithNameAndDescription()
        {
            // Arrange
            var builder = new TermBuilder();

            // Act
            Nomenclature nomenclature = new NomenclatureBuilder()
                .AddTerm(builder.CreateTerm("Term-1", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-2", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-3", "This is a test term"))
                .CreateNomenclature("Test", "This is a test nomenclature");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a test nomenclature"));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(3));

            var term1 = nomenclature.GetTerm("Term-1");

            Assert.That(term1, Is.Not.Null);
            Assert.That(term1.Name, Is.EqualTo("Term-1"));
            Assert.That(term1.Description, Is.EqualTo("This is a test term"));
            Assert.That(term1.NomenclatureID, Is.EqualTo(nomenclature.ID));

            var term2 = nomenclature.GetTerm("Term-2");

            Assert.That(term2, Is.Not.Null);
            Assert.That(term2.Name, Is.EqualTo("Term-2"));
            Assert.That(term2.Description, Is.EqualTo("This is a test term"));
            Assert.That(term2.NomenclatureID, Is.EqualTo(nomenclature.ID));

            var term3 = nomenclature.GetTerm("Term-3");

            Assert.That(term3, Is.Not.Null);
            Assert.That(term3.Name, Is.EqualTo("Term-3"));
            Assert.That(term3.Description, Is.EqualTo("This is a test term"));
            Assert.That(term3.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.CreateNomenclature(string, string)"/> function resets the <see cref="NomenclatureBuilder"/>.
        /// </summary>
        [Test]
        public void TestCreateNomenclatureWithNameAndDescriptionClearsStoredTerms()
        {
            // Arrange
            var nomenclatureBuilder = new NomenclatureBuilder();
            var termBuilder = new TermBuilder();

            var nomenclature = nomenclatureBuilder.AddTerm(termBuilder.CreateTerm("Term-1"))
                                                  .AddTerm(termBuilder.CreateTerm("Term-2"))
                                                  .AddTerm(termBuilder.CreateTerm("Term-3"))
                                                  .CreateNomenclature("Nomenclature-1", "This is a test nomenclature");

            // Act
            nomenclature = nomenclatureBuilder.AddTerm(termBuilder.CreateTerm("Term-4"))
                                              .CreateNomenclature("Nomenclature-2", "This is a test nomenclature");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-2"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a test nomenclature"));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(1));

            var term = nomenclature.GetTerm("Term-4");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term-4"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));
            Assert.That(term.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.CreateNomenclature(string)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateNomenclatureWithNameOnly()
        {
            // Arrange
            var builder = new TermBuilder();

            // Act
            Nomenclature nomenclature = new NomenclatureBuilder()
                .AddTerm(builder.CreateTerm("Term-1", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-2", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-3", "This is a test term"))
                .CreateNomenclature("Test");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.Empty);

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(3));

            var term1 = nomenclature.GetTerm("Term-1");

            Assert.That(term1, Is.Not.Null);
            Assert.That(term1.Name, Is.EqualTo("Term-1"));
            Assert.That(term1.Description, Is.EqualTo("This is a test term"));
            Assert.That(term1.NomenclatureID, Is.EqualTo(nomenclature.ID));

            var term2 = nomenclature.GetTerm("Term-2");

            Assert.That(term2, Is.Not.Null);
            Assert.That(term2.Name, Is.EqualTo("Term-2"));
            Assert.That(term2.Description, Is.EqualTo("This is a test term"));
            Assert.That(term2.NomenclatureID, Is.EqualTo(nomenclature.ID));

            var term3 = nomenclature.GetTerm("Term-3");

            Assert.That(term3, Is.Not.Null);
            Assert.That(term3.Name, Is.EqualTo("Term-3"));
            Assert.That(term3.Description, Is.EqualTo("This is a test term"));
            Assert.That(term3.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        /// <summary>
        /// Test that the <see cref="NomenclatureBuilder.CreateNomenclature(string)"/> function resets the <see cref="NomenclatureBuilder"/>.
        /// </summary>
        [Test]
        public void TestCreateNomenclatureWithNameOnlyClearsStoredTerms()
        {
            // Arrange
            var nomenclatureBuilder = new NomenclatureBuilder();
            var termBuilder = new TermBuilder();

            var nomenclature = nomenclatureBuilder.AddTerm(termBuilder.CreateTerm("Term-1"))
                                                  .AddTerm(termBuilder.CreateTerm("Term-2"))
                                                  .AddTerm(termBuilder.CreateTerm("Term-3"))
                                                  .CreateNomenclature("Nomenclature-1", "This is a test nomenclature");

            // Act
            nomenclature = nomenclatureBuilder.AddTerm(termBuilder.CreateTerm("Term-4"))
                                              .CreateNomenclature("Nomenclature-2");

            // Assert
            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-2"));
            Assert.That(nomenclature.Description, Is.Empty);

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(1));

            var term = nomenclature.GetTerm("Term-4");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term-4"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));
            Assert.That(term.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }
    }
}
