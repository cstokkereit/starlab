namespace Stratosoft.Nomenclature.Tests
{
    [TestClass]
    public class TermBuilderTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            TermBuilder builder = new TermBuilder();
            Assert.IsNotNull(builder);
        }

        [TestMethod]
        public void TestAddProperties()
        {
            List<Property> properties = new List<Property>();

            properties.Add(new Property("Property-1", "This is a test property"));
            properties.Add(new Property("Property-2", "This is a test property"));
            properties.Add(new Property("Property-3", "This is a test property"));

            Term term = new TermBuilder().AddProperties(properties).CreateTerm("Test");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual(string.Empty, term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(3, term.Properties.Count);

            Property property1 = term["Property-1"];

            Assert.IsNotNull(property1);
            Assert.IsNotNull(property1.ID);
            Assert.AreEqual("Property-1", property1.Name);
            Assert.AreEqual("This is a test property", property1.Description);
            Assert.AreEqual(term.ID, property1.TermID);

            Property property2 = term["Property-2"];

            Assert.IsNotNull(property2);
            Assert.IsNotNull(property2.ID);
            Assert.AreEqual("Property-2", property2.Name);
            Assert.AreEqual("This is a test property", property2.Description);
            Assert.AreEqual(term.ID, property2.TermID);

            Property property3 = term["Property-3"];

            Assert.IsNotNull(property3);
            Assert.IsNotNull(property3.ID);
            Assert.AreEqual("Property-3", property3.Name);
            Assert.AreEqual("This is a test property", property3.Description);
            Assert.AreEqual(term.ID, property3.TermID);
        }

        [TestMethod]
        public void TestAddProperty()
        {
            Term term = new TermBuilder().AddProperty(new Property("Property-1", "This is a test property")).CreateTerm("Test");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual(string.Empty, term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(1, term.Properties.Count);

            Property property = term["Property-1"];

            Assert.IsNotNull(property);
            Assert.IsNotNull(property.ID);
            Assert.AreEqual("Property-1", property.Name);
            Assert.AreEqual("This is a test property", property.Description);
            Assert.AreEqual(term.ID, property.TermID);
        }

        [TestMethod]
        public void TestAddPropertyWithNameAndDescription()
        {
            Term term = new TermBuilder().AddProperty("Property-1", "This is a test property").CreateTerm("Test");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual(string.Empty, term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(1, term.Properties.Count);

            Property property = term["Property-1"];

            Assert.IsNotNull(property);
            Assert.IsNotNull(property.ID);
            Assert.AreEqual("Property-1", property.Name);
            Assert.AreEqual("This is a test property", property.Description);
            Assert.AreEqual(term.ID, property.TermID);
        }

        [TestMethod]
        public void TestAddPropertyWithNameOnly()
        {
            Term term = new TermBuilder().AddProperty("Property-1").CreateTerm("Test");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual(string.Empty, term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(1, term.Properties.Count);

            Property property = term["Property-1"];

            Assert.IsNotNull(property);
            Assert.IsNotNull(property.ID);
            Assert.AreEqual("Property-1", property.Name);
            Assert.AreEqual(string.Empty, property.Description);
            Assert.AreEqual(term.ID, property.TermID);
        }

        [TestMethod]
        public void TestCreateTermClearsStoredProperties()
        {
            TermBuilder builder = new TermBuilder();

            Term term = builder.AddProperty("Property-1")
                               .AddProperty("Property-2")
                               .AddProperty("Property-3")
                               .CreateTerm("Term-1", "This is a test term");

            Assert.IsNotNull(term);

            term = builder.AddProperty("Property-4")
                          .CreateTerm("Term-2", "This is a test term");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Term-2", term.Name);
            Assert.AreEqual("This is a test term", term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(1, term.Properties.Count);

            Property property = term["Property-4"];

            Assert.IsNotNull(property);
            Assert.IsNotNull(property.ID);
            Assert.AreEqual("Property-4", property.Name);
            Assert.AreEqual(string.Empty, property.Description);
            Assert.AreEqual(term.ID, property.TermID);
        }

        [TestMethod]
        public void TestCreateTermWithMultipleProperties()
        {
            Term term = new TermBuilder()
                .AddProperty("Property-1", "This is a test property")
                .AddProperty("Property-2", "This is a test property")
                .AddProperty("Property-3", "This is a test property")
                .CreateTerm("Test");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual(string.Empty, term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(3, term.Properties.Count);

            Property property1 = term["Property-1"];

            Assert.IsNotNull(property1);
            Assert.IsNotNull(property1.ID);
            Assert.AreEqual("Property-1", property1.Name);
            Assert.AreEqual("This is a test property", property1.Description);
            Assert.AreEqual(term.ID, property1.TermID);

            Property property2 = term["Property-2"];

            Assert.IsNotNull(property2);
            Assert.IsNotNull(property2.ID);
            Assert.AreEqual("Property-2", property2.Name);
            Assert.AreEqual("This is a test property", property2.Description);
            Assert.AreEqual(term.ID, property2.TermID);

            Property property3 = term["Property-3"];

            Assert.IsNotNull(property3);
            Assert.IsNotNull(property3.ID);
            Assert.AreEqual("Property-3", property3.Name);
            Assert.AreEqual("This is a test property", property3.Description);
            Assert.AreEqual(term.ID, property3.TermID);
        }

        [TestMethod]
        public void TestCreateTermWithNameAndDescription()
        {
            Term term = new TermBuilder().CreateTerm("Test", "This is a test term");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual("This is a test term", term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(0, term.Properties.Count);
        }

        [TestMethod]
        public void TestCreateTermWithNameOnly()
        {
            Term term = new TermBuilder().CreateTerm("Test");

            Assert.IsNotNull(term);
            Assert.IsNotNull(term.ID);
            Assert.AreEqual("Test", term.Name);
            Assert.AreEqual(string.Empty, term.Description);

            Assert.IsNotNull(term.Properties);
            Assert.AreEqual(0, term.Properties.Count);
        }
    }
}
