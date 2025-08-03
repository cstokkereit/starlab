using Stratosoft.File.IO;
using System.Reflection;

namespace StarLab.Domain
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="CatalogueParser"/> class.
    /// </summary>
    public class CatalogueParserTests
    {
        private readonly Dictionary<string, int> map = new Dictionary<string, int>(); // Maps field names to array indices.

        private readonly string resources; // The path to the test resources folder.

        /// <summary>
        /// Initialises a new instance of the <see cref="CatalogueParserTests"/> class.
        /// </summary>
        public CatalogueParserTests()
        {
            resources = string.Empty;

            try
            {
                var location = Directory.GetParent(Assembly.GetExecutingAssembly().Location);

                if (location != null)
                {
                    resources = Path.GetFullPath($"{location.FullName}..\\..\\..\\..\\Resources");
                }
            }
            catch (Exception)
            {
                Assert.Fail("Initialisation Failed.");
            }

            map.Add("F1", 0);
            map.Add("F2", 1);
            map.Add("F3", 2);
            map.Add("F4", 3);
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser(DelimitedValueFileParser, Dictionary{string, int})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileParserAndFieldMap()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","), map);

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser(DelimitedValueFileParser)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileParser()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","));

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.GetValue(int)"/> method throws an exception if the end of the file has been reached.
        /// </summary>
        [Test]
        public void TestGetValueByIndexThrowsIfEndOfFileReached()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","));

            parser.Parse();
            parser.Parse();
            parser.Parse();
            parser.Parse();

            var e = Assert.Throws<InvalidOperationException>(() => parser.GetValue(0));

            Assert.That(e.Message, Is.EqualTo("Cannot read past the end of the file."));
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.GetValue(int)"/> method returns the value of the field with the index.
        /// </summary>
        [Test]
        public void TestGetValueByIndex()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","));

            parser.Parse();

            Assert.That(parser.GetValue(0), Is.EqualTo("1"));
            Assert.That(parser.GetValue(1), Is.EqualTo("1.2"));
            Assert.That(parser.GetValue(2), Is.EqualTo("1.3"));
            Assert.That(parser.GetValue(3), Is.EqualTo("1.4"));
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.GetValue(string)"/> method throws an exception if the field map has not been set.
        /// </summary>
        [Test]
        public void TestGetValueByNameThrowsIfFieldMapNotSet()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","));

            parser.Parse();

            var e = Assert.Throws<InvalidOperationException>(() => parser.GetValue("F1"));

            Assert.That(e.Message, Is.EqualTo("The field map has not been set."));
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.GetValue(string)"/> method throws an exception if the end of the file has been reached.
        /// </summary>
        [Test]
        public void TestGetValueByNameThrowsIfEndOfFileReached()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","), map);

            parser.Parse();
            parser.Parse();
            parser.Parse();
            parser.Parse();

            var e = Assert.Throws<InvalidOperationException>(() => parser.GetValue("F1"));

            Assert.That(e.Message, Is.EqualTo("Cannot read past the end of the file."));
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.GetValue(string)"/> method returns the value of the field with the specified name.
        /// </summary>
        [Test]
        public void TestGetValueByName()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","), map);

            parser.Parse();

            Assert.That(parser.GetValue("F1"), Is.EqualTo("1"));
            Assert.That(parser.GetValue("F2"), Is.EqualTo("1.2"));
            Assert.That(parser.GetValue("F3"), Is.EqualTo("1.3"));
            Assert.That(parser.GetValue("F4"), Is.EqualTo("1.4"));
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.Parse()"/> method works correctly with a comma delimited file and mapped field names.
        /// </summary>
        [Test]
        public void TestParse()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","), map);

            parser.Parse();

            Assert.That(parser.GetValue(0), Is.EqualTo("1"));
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.EOF"/> property returns <see cref="false"/> when the end of the file has not been reached.
        /// </summary>
        [Test]
        public void TestEOFReturnsFalseWhenEndOfFileNotYetReached()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","), map);

            parser.Parse();
            parser.Parse();
            parser.Parse();

            Assert.That(parser.EOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.EOF"/> property returns <see cref="true"/> when the end of the file has been reached.
        /// </summary>
        [Test]
        public void TestEOFReturnsTrueWhenEndOfFileReached()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "Catalogue.txt"), ","), map);

            parser.Parse();
            parser.Parse();
            parser.Parse();
            parser.Parse();

            Assert.That(parser.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="CatalogueParser.EOF"/> property returns <see cref="true"/> when the file contains no data.
        /// </summary>
        [Test]
        public void TestEOFReturnsTrueWhenFileEmpty()
        {
            var parser = new CatalogueParser(new DelimitedValueFileParser(Path.Combine(resources, "EmptyCatalogue.txt"), ","), map);

            parser.Parse();

            Assert.That(parser.EOF, Is.True);
        }
    }
}
