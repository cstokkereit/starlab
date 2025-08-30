namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public class NomenclatureBuilderTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            NomenclatureBuilder builder = new NomenclatureBuilder();
            Assert.IsNotNull(builder);
        }

        [TestMethod]
        public void TestAddTerms()
        {
            List<Term> terms = new List<Term>();

            terms.Add(new Term("Term-1", "This is a test term"));
            terms.Add(new Term("Term-2", "This is a test term"));
            terms.Add(new Term("Term-3", "This is a test term"));

            Nomenclature nomenclature = new NomenclatureBuilder().AddTerms(terms).CreateNomenclature("Test");

            Assert.IsNotNull(nomenclature);
            Assert.IsNotNull(nomenclature.ID);
            Assert.AreEqual("Test", nomenclature.Name);
            Assert.AreEqual(string.Empty, nomenclature.Description);

            Assert.IsNotNull(nomenclature.Terms);
            Assert.AreEqual(3, nomenclature.Terms.Count);

            Term term1 = nomenclature.Terms["Term-1"];

            Assert.IsNotNull(term1);
            Assert.IsNotNull(term1.ID);
            Assert.AreEqual("Term-1", term1.Name);
            Assert.AreEqual("This is a test term", term1.Description);
            Assert.AreEqual(nomenclature.ID, term1.NomenclatureID);

            Term term2 = nomenclature.Terms["Term-2"];

            Assert.IsNotNull(term2);
            Assert.IsNotNull(term2.ID);
            Assert.AreEqual("Term-2", term2.Name);
            Assert.AreEqual("This is a test term", term2.Description);
            Assert.AreEqual(nomenclature.ID, term2.NomenclatureID);

            Term term3 = nomenclature.Terms["Term-3"];

            Assert.IsNotNull(term3);
            Assert.IsNotNull(term3.ID);
            Assert.AreEqual("Term-3", term3.Name);
            Assert.AreEqual("This is a test term", term3.Description);
            Assert.AreEqual(nomenclature.ID, term3.NomenclatureID);
        }

        [TestMethod]
        public void TestAddTerm()
        {
            Nomenclature nomenclature = new NomenclatureBuilder().AddTerm(new TermBuilder().CreateTerm("Term")).CreateNomenclature("Test");

            Assert.IsNotNull(nomenclature);
            Assert.IsNotNull(nomenclature.ID);
            Assert.AreEqual("Test", nomenclature.Name);
            Assert.AreEqual(string.Empty, nomenclature.Description);

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            Assert.IsNotNull(terms);
            Assert.AreEqual(1, terms.Count);

            Term term = terms["Term"];

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Term", term.Name);
            Assert.AreEqual(string.Empty, term.Description);
            Assert.AreEqual(nomenclature.ID, term.NomenclatureID);
        }

        [TestMethod]
        public void TestCreateDictionaryClearsStoredTerms()
        {
            NomenclatureBuilder dictionaryBuilder = new NomenclatureBuilder();
            TermBuilder termBuilder = new TermBuilder();

            Nomenclature nomenclature = dictionaryBuilder.AddTerm(termBuilder.CreateTerm("Term-1"))
                                                     .AddTerm(termBuilder.CreateTerm("Term-2"))
                                                     .AddTerm(termBuilder.CreateTerm("Term-3"))
                                                     .CreateNomenclature("Nomenclature-1", "This is a test nomenclature");

            Assert.IsNotNull(nomenclature);

            nomenclature = dictionaryBuilder.AddTerm(termBuilder.CreateTerm("Term-4"))
                                          .CreateNomenclature("Nomenclature-2", "This is a test nomenclature");

            Assert.IsNotNull(nomenclature);
            Assert.IsNotNull(nomenclature.ID);
            Assert.AreEqual("Nomenclature-2", nomenclature.Name);
            Assert.AreEqual("This is a test nomenclature", nomenclature.Description);

            Assert.IsNotNull(nomenclature.Terms);
            Assert.AreEqual(1, nomenclature.Terms.Count);

            Term term = nomenclature.Terms["Term-4"];

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Term-4", term.Name);
            Assert.AreEqual(string.Empty, term.Description);
            Assert.AreEqual(nomenclature.ID, term.NomenclatureID);
        }

        [TestMethod]
        public void TestCreateDictionaryWithNameOnly()
        {
            Nomenclature nomenclature = new NomenclatureBuilder().CreateNomenclature("Test");

            Assert.IsNotNull(nomenclature);
            Assert.IsNotNull(nomenclature.ID);
            Assert.AreEqual("Test", nomenclature.Name);
            Assert.AreEqual(string.Empty, nomenclature.Description);

            Assert.IsNotNull(nomenclature.Terms);
            Assert.AreEqual(0, nomenclature.Terms.Count);
        }

        [TestMethod]
        public void TestCreateDictionaryWithMultipleTerms()
        {
            TermBuilder builder = new TermBuilder();

            Nomenclature nomenclature = new NomenclatureBuilder()
                .AddTerm(builder.CreateTerm("Term-1", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-2", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-3", "This is a test term"))
                .CreateNomenclature("Test");

            Assert.IsNotNull(nomenclature);
            Assert.IsNotNull(nomenclature.ID);
            Assert.AreEqual("Test", nomenclature.Name);
            Assert.AreEqual(string.Empty, nomenclature.Description);

            Assert.IsNotNull(nomenclature.Terms);
            Assert.AreEqual(3, nomenclature.Terms.Count);

            Term term1 = nomenclature.Terms["Term-1"];

            Assert.IsNotNull(term1);
            Assert.IsNotNull(term1.ID);
            Assert.AreEqual("Term-1", term1.Name);
            Assert.AreEqual("This is a test term", term1.Description);
            Assert.AreEqual(nomenclature.ID, term1.NomenclatureID);

            Term term2 = nomenclature.Terms["Term-2"];

            Assert.IsNotNull(term2);
            Assert.IsNotNull(term2.ID);
            Assert.AreEqual("Term-2", term2.Name);
            Assert.AreEqual("This is a test term", term2.Description);
            Assert.AreEqual(nomenclature.ID, term2.NomenclatureID);

            Term term3 = nomenclature.Terms["Term-3"];

            Assert.IsNotNull(term3);
            Assert.IsNotNull(term3.ID);
            Assert.AreEqual("Term-3", term3.Name);
            Assert.AreEqual("This is a test term", term3.Description);
            Assert.AreEqual(nomenclature.ID, term3.NomenclatureID);
        }

        [TestMethod]
        public void TestCreateDictionaryWithNameAndDescription()
        {
            Nomenclature nomenclature = new NomenclatureBuilder().CreateNomenclature("Test", "This is a test nomenclature");

            Assert.IsNotNull(nomenclature);
            Assert.IsNotNull(nomenclature.ID);
            Assert.AreEqual("Test", nomenclature.Name);
            Assert.AreEqual("This is a test nomenclature", nomenclature.Description);

            Assert.IsNotNull(nomenclature.Terms);
            Assert.AreEqual(0, nomenclature.Terms.Count);
        }
    }
}
