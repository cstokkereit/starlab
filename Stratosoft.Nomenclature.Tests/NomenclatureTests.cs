using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public class NomenclatureTests
    {
        private Guid id = Guid.Parse("f2ea28f1-4ce5-469f-bfe1-80f4c7c4B6b8");

        [TestMethod]
        public void TestConstructionFromNameAndDescription()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1", "This is a nomenclature.");

            Assert.IsNotNull(nomenclature);
        }

        [TestMethod]
        public void TestConstructionFromName()
        {
            Nomenclature nomenclature = new Nomenclature("Dictionary-1");

            Assert.IsNotNull(nomenclature);
        }

        [TestMethod]
        public void TestConstructionFromXmlNomenclature()
        {
            XmlNomenclature xmlNomenclature = new ()
            {
                ID = id,
                Name = "Nomenclature-1",
                Description = "This is a nomenclature"
            };

            Nomenclature nomenclature = new Nomenclature(xmlNomenclature);

            Assert.IsNotNull(nomenclature);
            Assert.AreEqual(id, nomenclature.ID);
            Assert.AreEqual("Nomenclature-1", nomenclature.Name);
            Assert.AreEqual("This is a nomenclature", nomenclature.Description);
        }

        [TestMethod]
        public void TestGetDescription()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1", "This is a nomenclature.");

            Assert.AreEqual("This is a nomenclature.", nomenclature.Description);
        }

        [TestMethod]
        public void TestGetID()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Assert.IsNotNull(nomenclature.ID);
            Assert.AreNotEqual(Guid.Empty, nomenclature.ID);
        }

        [TestMethod]
        public void TestGetName()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Assert.AreEqual("Nomenclature-1", nomenclature.Name);
        }

        [TestMethod]
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

            Assert.AreEqual(4, terms.Count);
            Assert.IsTrue(terms.Values.Contains(term1));
            Assert.IsTrue(terms.Values.Contains(term2));
            Assert.IsTrue(terms.Values.Contains(term3));
            Assert.IsTrue(terms.Values.Contains(term4));
        }

        [TestMethod]
        public void TestAdd()
        {
            Nomenclature nomenclature = new Nomenclature("Nomenclature-1");

            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-2");

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            nomenclature.Add(term1);

            Assert.AreEqual(1, terms.Count);
            Assert.AreEqual(term1, terms["Term-1"]);

            nomenclature.Add(term2);

            Assert.AreEqual(2, terms.Count);
            Assert.AreEqual(term2, terms["Term-2"]);
        }

        [TestMethod]
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

            Assert.AreEqual(3, terms.Count);

            nomenclature.Remove(term2);

            Assert.AreEqual(2, terms.Count);
            Assert.AreEqual(term1, terms["Term-1"]);
            Assert.AreEqual(term3, terms["Term-3"]);
        }

        [TestMethod]
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

            Assert.AreEqual(3, terms.Count);

            nomenclature.Remove("Term-1");

            Assert.AreEqual(2, terms.Count);
            Assert.AreEqual(term2, terms["Term-2"]);
            Assert.AreEqual(term3, terms["Term-3"]);
        }
    }
}