using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    public class NomenclatureTests
    {
        private Guid id = Guid.Parse("f2ea28f1-4ce5-469f-bfe1-80f4c7c4B6b8");

        [Test]
        public void TestConstructionFromNameAndDescription()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1", "This is a nomenclature.");

            Assert.That(nomenclature, Is.Not.Null);
        }

        [Test]
        public void TestConstructionFromName()
        {
            Nomenclature nomenclature = new Nomenclature("Dictionary-1");

            Assert.That(nomenclature, Is.Not.Null);
        }

        [Test]
        public void TestConstructionFromXmlNomenclature()
        {
            XmlNomenclature xmlNomenclature = new ()
            {
                ID = id,
                Name = "Nomenclature-1",
                Description = "This is a nomenclature"
            };

            Nomenclature nomenclature = new Nomenclature(xmlNomenclature);

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.ID, Is.EqualTo(id));
            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-1"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a nomenclature"));
        }

        [Test]
        public void TestGetDescription()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1", "This is a nomenclature.");

            Assert.That(nomenclature.Description, Is.EqualTo("This is a nomenclature."));
        }

        [Test]
        public void TestGetID()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Assert.That(nomenclature.ID, Is.Not.EqualTo(Guid.Empty));
        }

        [Test]
        public void TestGetName()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-1"));
        }

        [Test]
        public void TestGetTerms()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-2");
            Term term3 = new Term("Term-3");
            Term term4 = new Term("Term-4");

            nomenclature.Add(term1);
            nomenclature.Add(term2);
            nomenclature.Add(term3);
            nomenclature.Add(term4);

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            Assert.That(terms.Count, Is.EqualTo(4));
            Assert.That(terms.Values.Contains(term1), Is.True);
            Assert.That(terms.Values.Contains(term2), Is.True);
            Assert.That(terms.Values.Contains(term3), Is.True);
            Assert.That(terms.Values.Contains(term4), Is.True);
        }

        [Test]
        public void TestAdd()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-2");

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            nomenclature.Add(term1);

            Assert.That(terms.Count, Is.EqualTo(1));
            Assert.That(terms["Term-1"], Is.EqualTo(term1));

            nomenclature.Add(term2);

            Assert.That(terms.Count, Is.EqualTo(2));
            Assert.That(terms["Term-2"], Is.EqualTo(term2));
        }

        [Test]
        public void TestRemove()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-2");
            Term term3 = new Term("Term-3");

            nomenclature.Add(term1);
            nomenclature.Add(term2);
            nomenclature.Add(term3);

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            Assert.That(terms.Count, Is.EqualTo(3));

            nomenclature.Remove(term2);

            Assert.That(terms.Count, Is.EqualTo(2));
            Assert.That(terms["Term-1"], Is.EqualTo(term1));
            Assert.That(terms["Term-3"], Is.EqualTo(term3));
        }

        [Test]
        public void TestRemoveByName()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-2");
            Term term3 = new Term("Term-3");

            nomenclature.Add(term1);
            nomenclature.Add(term2);
            nomenclature.Add(term3);

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            Assert.That(terms.Count, Is.EqualTo(3));

            nomenclature.Remove("Term-1");

            Assert.That(terms.Count, Is.EqualTo(2));
            Assert.That(terms["Term-2"], Is.EqualTo(term2));
            Assert.That(terms["Term-3"], Is.EqualTo(term3));
        }
    }
}