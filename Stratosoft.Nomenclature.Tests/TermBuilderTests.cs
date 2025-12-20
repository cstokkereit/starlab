namespace Stratosoft.Nomenclature.Tests
{
    public class TermBuilderTests
    {
        [Test]
        public void TestConstructor()
        {
            TermBuilder builder = new TermBuilder();
            Assert.That(builder, Is.Not.Null);
        }

        [Test]
        public void TestAddProperties()
        {
            List<Property> properties = new List<Property>();

            properties.Add(new Property("Property-1", "This is a test property"));
            properties.Add(new Property("Property-2", "This is a test property"));
            properties.Add(new Property("Property-3", "This is a test property"));

            Term term = new TermBuilder().AddProperties(properties).CreateTerm("Test");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(3));

            Property property1 = term["Property-1"];

            Assert.That(property1, Is.Not.Null);
            Assert.That(property1.Name, Is.EqualTo("Property-1"));
            Assert.That(property1.Description, Is.EqualTo("This is a test property"));
            Assert.That(property1.TermID, Is.EqualTo(term.ID));

            Property property2 = term["Property-2"];

            Assert.That(property2, Is.Not.Null);
            Assert.That(property2.Name, Is.EqualTo("Property-2"));
            Assert.That(property2.Description, Is.EqualTo("This is a test property"));
            Assert.That(property2.TermID, Is.EqualTo(term.ID));

            Property property3 = term["Property-3"];

            Assert.That(property3, Is.Not.Null);
            Assert.That(property3.Name, Is.EqualTo("Property-3"));
            Assert.That(property3.Description, Is.EqualTo("This is a test property"));
            Assert.That(property3.TermID, Is.EqualTo(term.ID));
        }

        [Test]
        public void TestAddProperty()
        {
            Term term = new TermBuilder().AddProperty(new Property("Property-1", "This is a test property")).CreateTerm("Test");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term["Property-1"];

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo("This is a test property"));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        [Test]
        public void TestAddPropertyWithNameAndDescription()
        {
            Term term = new TermBuilder().AddProperty("Property-1", "This is a test property").CreateTerm("Test");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term["Property-1"];

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo("This is a test property"));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        [Test]
        public void TestAddPropertyWithNameOnly()
        {
            Term term = new TermBuilder().AddProperty("Property-1").CreateTerm("Test");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term["Property-1"];

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-1"));
            Assert.That(property.Description, Is.EqualTo(string.Empty));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        [Test]
        public void TestCreateTermClearsStoredProperties()
        {
            TermBuilder builder = new TermBuilder();

            Term term = builder.AddProperty("Property-1")
                               .AddProperty("Property-2")
                               .AddProperty("Property-3")
                               .CreateTerm("Term-1", "This is a test term");

            Assert.That(term, Is.Not.Null);

            term = builder.AddProperty("Property-4")
                          .CreateTerm("Term-2", "This is a test term");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Term-2"));
            Assert.That(term.Description, Is.EqualTo("This is a test term"));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(1));

            Property property = term["Property-4"];

            Assert.That(property, Is.Not.Null);
            Assert.That(property.Name, Is.EqualTo("Property-4"));
            Assert.That(property.Description, Is.EqualTo(string.Empty));
            Assert.That(property.TermID, Is.EqualTo(term.ID));
        }

        [Test]
        public void TestCreateTermWithMultipleProperties()
        {
            Term term = new TermBuilder()
                .AddProperty("Property-1", "This is a test property")
                .AddProperty("Property-2", "This is a test property")
                .AddProperty("Property-3", "This is a test property")
                .CreateTerm("Test");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(3));

            Property property1 = term["Property-1"];

            Assert.That(property1, Is.Not.Null);
            Assert.That(property1.Name, Is.EqualTo("Property-1"));
            Assert.That(property1.Description, Is.EqualTo("This is a test property"));
            Assert.That(property1.TermID, Is.EqualTo(term.ID));

            Property property2 = term["Property-2"];

            Assert.That(property2, Is.Not.Null);
            Assert.That(property2.Name, Is.EqualTo("Property-2"));
            Assert.That(property2.Description, Is.EqualTo("This is a test property"));
            Assert.That(property2.TermID, Is.EqualTo(term.ID));

            Property property3 = term["Property-3"];

            Assert.That(property3, Is.Not.Null);
            Assert.That(property3.Name, Is.EqualTo("Property-3"));
            Assert.That(property3.Description, Is.EqualTo("This is a test property"));
            Assert.That(property3.TermID, Is.EqualTo(term.ID));
        }

        [Test]
        public void TestCreateTermWithNameAndDescription()
        {
            Term term = new TermBuilder().CreateTerm("Test", "This is a test term");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo("This is a test term"));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestCreateTermWithNameOnly()
        {
            Term term = new TermBuilder().CreateTerm("Test");

            Assert.That(term, Is.Not.Null);
            Assert.That(term.Name, Is.EqualTo("Test"));
            Assert.That(term.Description, Is.EqualTo(string.Empty));

            Assert.That(term.Properties, Is.Not.Null);
            Assert.That(term.Properties.Count, Is.EqualTo(0));
        }
    }
}
