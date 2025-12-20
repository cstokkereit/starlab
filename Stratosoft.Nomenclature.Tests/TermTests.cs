using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    /// <summary>
    /// TODO
    /// </summary>
    public class TermTests
    {
        private Guid id = Guid.Parse("0aefa0b8-624a-4a37-926a-ac6a93ee11c4");

        /// <summary>
        /// TODO - Test descriptions
        /// </summary>
        [Test]
        public void TestConstructionFromNameAndDescription()
        {
            Term term = new Term("Term-1", "This is a term.");

            Assert.That(term, Is.Not.Null);
        }

        [Test]
        public void TestConstructionFromName()
        {
            Term term = new Term("Term-1");

            Assert.That(term, Is.Not.Null);
        }

        [Test]
        public void TestConstructionFromXmlTerm()
        {
            XmlTerm xmlTerm = new()
            {
                ID = id,
                Name = "Term-1",
                Description = "This is a term",
                Properties = new List<XmlProperty>()
            };

            Term term = new Term(xmlTerm);

            Assert.That(term, Is.Not.Null);
            Assert.That(term.ID, Is.EqualTo(id));
            Assert.That(term.Name, Is.EqualTo("Term-1"));
            Assert.That(term.Description, Is.EqualTo("This is a term"));
            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestGetDescription()
        {
            Term term = new Term("Term-1", "This is a term.");

            Assert.That(term.Description, Is.EqualTo("This is a term."));
        }

        [Test]
        public void TestGetName()
        {
            Term term = new Term("Term-1");

            Assert.That(term.Name, Is.EqualTo("Term-1"));
        }

        [Test]
        public void TestGetPropertyByName()
        {
            Property property1 = new Property("Property-1");
            Property property2 = new Property("Property-2");
            Property property3 = new Property("Property-3");

            Term term = new TermBuilder()
                .AddProperty(property1)
                .AddProperty(property2)
                .AddProperty(property3)
                .CreateTerm("Term-1");

            Assert.That(term["Property-1"], Is.EqualTo(property1));
            Assert.That(term["Property-2"], Is.EqualTo(property2));
            Assert.That(term["Property-3"], Is.EqualTo(property3));
        }

        [Test]
        public void TestGetProperties()
        {
            Property property1 = new Property("Property-1");
            Property property2 = new Property("Property-2");
            Property property3 = new Property("Property-3");

            Term term = new TermBuilder()
                .AddProperty(property1)
                .AddProperty(property2)
                .AddProperty(property3)
                .CreateTerm("Term-1");

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(3));
            Assert.That(term.Properties[0], Is.EqualTo(property1));
            Assert.That(term.Properties[1], Is.EqualTo(property2));
            Assert.That(term.Properties[2], Is.EqualTo(property3));
        }

        [Test]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            Term term = new Term("Term-1");

            Assert.That(term.Equals("Term-1"), Is.False);
        }

        [Test]
        public void TestEqualsWhenArgumentIsEqual()
        {
            Term term1 = new Term("Term-1");
            Term term2 = term1;

            Assert.That(term1.Equals(term2), Is.True);
        }

        [Test]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-1");

            Assert.That(term1.Equals(term2), Is.False);
        }

        [Test]
        public void TestEqualsWhenArgumentIsNull()
        {
            Term term = new Term("Term-1");

            Assert.That(term.Equals(null), Is.False);
        }

        [Test]
        public void TestHashCodesDoNotMatchForDifferentTerms()
        {
            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-1");

            Assert.That(term1.GetHashCode(), Is.Not.EqualTo(term2.GetHashCode()));
        }

        [Test]
        public void TestHashCodesMatchForSameTerms()
        {
            Term term1 = new Term("Term-1");
            Term term2 = term1;

            Assert.That(term1.GetHashCode(), Is.EqualTo(term2.GetHashCode()));
        }

        [Test]
        public void TestToString()
        {
            Term term = new Term("Term-1");

            Assert.That(term.ToString(), Is.EqualTo("Term-1 " + term.ID.ToString()));
        }
    }
}
