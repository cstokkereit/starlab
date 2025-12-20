using System.Linq;

namespace Stratosoft.Nomenclature.Tests
{
    public class NomenclatureBuilderTests
    {
        [Test]
        public void TestConstructor()
        {
            NomenclatureBuilder builder = new NomenclatureBuilder();
            Assert.That(builder, Is.Not.Null);
        }

        [Test]
        public void TestAddTerms()
        {
            List<Term> terms = new List<Term>();

            terms.Add(new Term("Term-1", "This is a test term"));
            terms.Add(new Term("Term-2", "This is a test term"));
            terms.Add(new Term("Term-3", "This is a test term"));

            Nomenclature nomenclature = new NomenclatureBuilder().AddTerms(terms).CreateNomenclature("Test");

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(3));

            Term term1 = nomenclature.Terms["Term-1"];

            Assert.That(term1, Is.Not.Null);
            Assert.That(term1.Name, Is.EqualTo("Term-1"));
            Assert.That(term1.Description, Is.EqualTo("This is a test term"));
            Assert.That(term1.NomenclatureID, Is.EqualTo(nomenclature.ID));

            Term term2 = nomenclature.Terms["Term-2"];

            Assert.That(term2, Is.Not.Null);
            Assert.That(term2.Name, Is.EqualTo("Term-2"));
            Assert.That(term2.Description, Is.EqualTo("This is a test term"));
            Assert.That(term2.NomenclatureID, Is.EqualTo(nomenclature.ID));

            Term term3 = nomenclature.Terms["Term-3"];

            Assert.That(term3, Is.Not.Null);
            Assert.That(term3.Name, Is.EqualTo("Term-3"));
            Assert.That(term3.Description, Is.EqualTo("This is a test term"));
            Assert.That(term3.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        [Test]
        public void TestAddTerm()
        {
            Nomenclature nomenclature = new NomenclatureBuilder().AddTerm(new TermBuilder().CreateTerm("Term")).CreateNomenclature("Test");

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            IReadOnlyDictionary<string, Term> terms = nomenclature.Terms;

            Assert.That(terms, Is.Not.Null);
            Assert.That(terms.Count, Is.EqualTo(1));

            Term term = terms["Term"];

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));
            Assert.That(term.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        [Test]
        public void TestCreateDictionaryClearsStoredTerms()
        {
            NomenclatureBuilder dictionaryBuilder = new NomenclatureBuilder();
            TermBuilder termBuilder = new TermBuilder();

            Nomenclature nomenclature = dictionaryBuilder.AddTerm(termBuilder.CreateTerm("Term-1"))
                                                     .AddTerm(termBuilder.CreateTerm("Term-2"))
                                                     .AddTerm(termBuilder.CreateTerm("Term-3"))
                                                     .CreateNomenclature("Nomenclature-1", "This is a test nomenclature");

            Assert.That(nomenclature, Is.Not.Null);

            nomenclature = dictionaryBuilder.AddTerm(termBuilder.CreateTerm("Term-4"))
                                          .CreateNomenclature("Nomenclature-2", "This is a test nomenclature");

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Nomenclature-2"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a test nomenclature"));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(1));

            Term term = nomenclature.Terms["Term-4"];

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term-4"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));
            Assert.That(term.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        [Test]
        public void TestCreateDictionaryWithNameOnly()
        {
            Nomenclature nomenclature = new NomenclatureBuilder().CreateNomenclature("Test");

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestCreateDictionaryWithMultipleTerms()
        {
            TermBuilder builder = new TermBuilder();

            Nomenclature nomenclature = new NomenclatureBuilder()
                .AddTerm(builder.CreateTerm("Term-1", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-2", "This is a test term"))
                .AddTerm(builder.CreateTerm("Term-3", "This is a test term"))
                .CreateNomenclature("Test");

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo(string.Empty));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(3));

            Term term1 = nomenclature.Terms["Term-1"];

            Assert.That(term1, Is.Not.Null);
            Assert.That(term1.Name, Is.EqualTo("Term-1"));
            Assert.That(term1.Description, Is.EqualTo("This is a test term"));
            Assert.That(term1.NomenclatureID, Is.EqualTo(nomenclature.ID));

            Term term2 = nomenclature.Terms["Term-2"];

            Assert.That(term2, Is.Not.Null);
            Assert.That(term2.Name, Is.EqualTo("Term-2"));
            Assert.That(term2.Description, Is.EqualTo("This is a test term"));
            Assert.That(term2.NomenclatureID, Is.EqualTo(nomenclature.ID));

            Term term3 = nomenclature.Terms["Term-3"];

            Assert.That(term3, Is.Not.Null);
            Assert.That(term3.Name, Is.EqualTo("Term-3"));
            Assert.That(term3.Description, Is.EqualTo("This is a test term"));
            Assert.That(term3.NomenclatureID, Is.EqualTo(nomenclature.ID));
        }

        [Test]
        public void TestCreateDictionaryWithNameAndDescription()
        {
            Nomenclature nomenclature = new NomenclatureBuilder().CreateNomenclature("Test", "This is a test nomenclature");

            Assert.That(nomenclature, Is.Not.Null);
            Assert.That(nomenclature.Name, Is.EqualTo("Test"));
            Assert.That(nomenclature.Description, Is.EqualTo("This is a test nomenclature"));

            Assert.That(nomenclature.Terms, Is.Not.Null);
            Assert.That(nomenclature.Terms.Count, Is.EqualTo(0));
        }
    }
}
