using Stratosoft.Nomenclature.Serialisation;

namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public class TermTests
    {
        private Guid id = Guid.Parse("0aefa0b8-624a-4a37-926a-ac6a93ee11c4");

        [TestMethod]
        public void TestConstructionFromNameAndDescription()
        {
            Term term = new Term("Term-1", "This is a term.");

            Assert.IsNotNull(term);
        }

        [TestMethod]
        public void TestConstructionFromName()
        {
            Term term = new Term("Term-1");

            Assert.IsNotNull(term);
        }

        [TestMethod]
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

            Assert.IsNotNull(term);
            Assert.AreEqual(id, term.ID);
            Assert.AreEqual("Term-1", term.Name);
            Assert.AreEqual("This is a term", term.Description);
            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(0, term.Properties.Count);
        }

        [TestMethod]
        public void TestGetDescription()
        {
            Term term = new Term("Term-1", "This is a term.");

            Assert.AreEqual("This is a term.", term.Description);
        }

        [TestMethod]
        public void TestGetId()
        {
            Term term = new Term("Term-1");

            Assert.IsNotNull(term.ID);
        }

        [TestMethod]
        public void TestGetName()
        {
            Term term = new Term("Term-1");

            Assert.AreEqual("Term-1", term.Name);
        }

        [TestMethod]
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

            Assert.AreEqual(property1, term["Property-1"]);
            Assert.AreEqual(property2, term["Property-2"]);
            Assert.AreEqual(property3, term["Property-3"]);
        }

        [TestMethod]
        public void TestGetProperties()
        {
            //Property property1 = new Property("Property-1");
            //Property property2 = new Property("Property-2");
            //Property property3 = new Property("Property-3");

            //Term term = new TermBuilder("Term-1")
            //    .AddProperty(property1)
            //    .AddProperty(property2)
            //    .AddProperty(property3)
            //    .CreateTerm();

            //Assert.IsNotNull(term.Properties);
            //Assert.AreEqual(3, term.Properties.Count);
            //Assert.AreEqual(property1, term.Properties[0]);
            //Assert.AreEqual(property2, term.Properties[1]);
            //Assert.AreEqual(property3, term.Properties[2]);
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsDifferentType()
        {
            Term term = new Term("Term-1");

            Assert.IsFalse(term.Equals("Term-1"));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsEqual()
        {
            Term term1 = new Term("Term-1");
            Term term2 = term1;

            Assert.IsTrue(term1.Equals(term2));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNotEqual()
        {
            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-1");

            Assert.IsFalse(term1.Equals(term2));
        }

        [TestMethod]
        public void TestEqualsWhenArgumentIsNull()
        {
            Term term = new Term("Term-1");

            Assert.IsFalse(term.Equals(null));
        }

        [TestMethod]
        public void TestHashCodesDoNotMatchForDifferentTerms()
        {
            Term term1 = new Term("Term-1");
            Term term2 = new Term("Term-1");

            Assert.AreNotEqual(term1.GetHashCode(), term2.GetHashCode());
        }

        [TestMethod]
        public void TestHashCodesMatchForSameTerms()
        {
            Term term1 = new Term("Term-1");
            Term term2 = term1;

            Assert.AreEqual(term1.GetHashCode(), term2.GetHashCode());
        }

        [TestMethod]
        public void TestToString()
        {
            Term term = new Term("Term-1");

            Assert.AreEqual("Term-1 " + term.ID.ToString(), term.ToString());
        }
    }
}
