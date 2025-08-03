namespace Stratosoft.File.IO
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DelimitedValueFileParser"/> class.
    /// </summary>
    public class DelimitedValueFileParserTests : ParserTests
    {
        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser(string, char, char)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileNameCharDelimiterAndCharTextDelimiter()
        {
            var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), ',', '\"');

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser(string, string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileNameStringDelimiterAndStringTextDelimiter()
        {
            var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), "  ", "'");

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser(string, char)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileNameAndCharDelimiter()
        {
            var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), ',');

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser(string, string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileNameAndStringDelimiter()
        {
            var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), "  ");

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse()"/> method works correctly with a comma delimited file.
        /// </summary>
        [Test]
        public void TestParseCsv()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), ','))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(4));
                Assert.That(data[0], Is.EqualTo("1"));
                Assert.That(data[1], Is.EqualTo("Value-1.1"));
                Assert.That(data[2], Is.EqualTo("Value-1.2"));
                Assert.That(data[3], Is.EqualTo("Value-1.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse()"/> method works correctly with a file that uses two pipes as the delimiter.
        /// </summary>
        [Test]
        public void TestParsePipes()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Pipes.txt"), "||"))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(4));
                Assert.That(data[0], Is.EqualTo("1"));
                Assert.That(data[1], Is.EqualTo("Value-1.1"));
                Assert.That(data[2], Is.EqualTo("Value-1.2"));
                Assert.That(data[3], Is.EqualTo("Value-1.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse()"/> method works correctly with a tab delimited file.
        /// </summary>
        [Test]
        public void TestParseTab()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Tab.txt"), '\t'))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(4));
                Assert.That(data[0], Is.EqualTo("1"));
                Assert.That(data[1], Is.EqualTo("Value-1.1"));
                Assert.That(data[2], Is.EqualTo("Value-1.2"));
                Assert.That(data[3], Is.EqualTo("Value-1.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse()"/> method works correctly with a space delimited text file that uses single quotes to enclose text values.
        /// </summary>
        [Test]
        public void TestParseSingleQuotedText()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Text1.txt"), " ", "'"))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(10));
                Assert.That(data[0], Is.EqualTo("1"));
                Assert.That(data[1], Is.EqualTo("Value-1.1"));
                Assert.That(data[2], Is.EqualTo("This is text value 1.2."));
                Assert.That(data[3], Is.EqualTo("1.3"));
                Assert.That(data[4], Is.EqualTo(""));
                Assert.That(data[5], Is.EqualTo(" This  is text   value 1.4. "));
                Assert.That(data[6], Is.EqualTo(""));
                Assert.That(data[7], Is.EqualTo("This is text value 1.5."));
                Assert.That(data[8], Is.EqualTo(""));
                Assert.That(data[9], Is.EqualTo("1.6"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse()"/> method works correctly with a space delimited text file that uses double quotes to enclose text values.
        /// </summary>
        [Test]
        public void TestParseDoubleQuotedText()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Text2.txt"), ' ', '\"'))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(10));
                Assert.That(data[0], Is.EqualTo("1"));
                Assert.That(data[1], Is.EqualTo("Value-1.1"));
                Assert.That(data[2], Is.EqualTo("This is text value 1.2."));
                Assert.That(data[3], Is.EqualTo("1.3"));
                Assert.That(data[4], Is.EqualTo(""));
                Assert.That(data[5], Is.EqualTo(" This  is text   value 1.4. "));
                Assert.That(data[6], Is.EqualTo(""));
                Assert.That(data[7], Is.EqualTo("This is text value 1.5."));
                Assert.That(data[8], Is.EqualTo(""));
                Assert.That(data[9], Is.EqualTo("1.6"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse(int)"/> method works correctly with a comma delimited file.
        /// </summary>
        [Test]
        public void TestParseTwoLinesCsv()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), ','))
            {
                List<string[]> data = parser.Parse(2);

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(2));

                Assert.That(data[0].Length, Is.EqualTo(4));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("Value-1.1"));
                Assert.That(data[0][2], Is.EqualTo("Value-1.2"));
                Assert.That(data[0][3], Is.EqualTo("Value-1.3"));

                Assert.That(data[1].Length, Is.EqualTo(4));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("Value-2.1"));
                Assert.That(data[1][2], Is.EqualTo("Value-2.2"));
                Assert.That(data[1][3], Is.EqualTo("Value-2.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse(int)"/> method works correctly with a tab delimited file.
        /// </summary>
        [Test]
        public void TestParseTwoLinesTab()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Tab.txt"), '\t'))
            {
                List<string[]> data = parser.Parse(2);

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(2));

                Assert.That(data[0].Length, Is.EqualTo(4));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("Value-1.1"));
                Assert.That(data[0][2], Is.EqualTo("Value-1.2"));
                Assert.That(data[0][3], Is.EqualTo("Value-1.3"));

                Assert.That(data[1].Length, Is.EqualTo(4));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("Value-2.1"));
                Assert.That(data[1][2], Is.EqualTo("Value-2.2"));
                Assert.That(data[1][3], Is.EqualTo("Value-2.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse(int)"/> method works correctly with a space delimited text file that uses single quotes to enclose text values.
        /// </summary>
        [Test]
        public void TestParseTwoLinesText()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Text1.txt"), " ", "'"))
            {
                List<string[]> data = parser.Parse(2);

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(2));

                Assert.That(data[0].Length, Is.EqualTo(10));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("Value-1.1"));
                Assert.That(data[0][2], Is.EqualTo("This is text value 1.2."));
                Assert.That(data[0][3], Is.EqualTo("1.3"));
                Assert.That(data[0][4], Is.EqualTo(""));
                Assert.That(data[0][5], Is.EqualTo(" This  is text   value 1.4. "));
                Assert.That(data[0][6], Is.EqualTo(""));
                Assert.That(data[0][7], Is.EqualTo("This is text value 1.5."));
                Assert.That(data[0][8], Is.EqualTo(""));
                Assert.That(data[0][9], Is.EqualTo("1.6"));

                Assert.That(data[1].Length, Is.EqualTo(10));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("Value-2.1"));
                Assert.That(data[1][2], Is.EqualTo("This is text value 2.2."));
                Assert.That(data[1][3], Is.EqualTo("2.3"));
                Assert.That(data[1][4], Is.EqualTo(""));
                Assert.That(data[1][5], Is.EqualTo(" This  is text   value 2.4. "));
                Assert.That(data[1][6], Is.EqualTo(""));
                Assert.That(data[1][7], Is.EqualTo("This is text value 2.5."));
                Assert.That(data[1][8], Is.EqualTo(""));
                Assert.That(data[1][9], Is.EqualTo("2.6"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.Parse(int)"/> method works correctly with a comma delimited file when there are fewer unparsed lines remaining than specified.
        /// </summary>
        [Test]
        public void TestParseTwoLinesWhenOnlyOneLineToRead()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), ','))
            {
                parser.Parse(2);

                List<string[]> data = parser.Parse(2);

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(1));

                Assert.That(data[0].Length, Is.EqualTo(4));

                Assert.That(data[0][0], Is.EqualTo("3"));
                Assert.That(data[0][1], Is.EqualTo("Value-3.1"));
                Assert.That(data[0][2], Is.EqualTo("Value-3.2"));
                Assert.That(data[0][3], Is.EqualTo("Value-3.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.ParseAll()"/> method works correctly with a comma delimited file.
        /// </summary>
        [Test]
        public void TestParseAllCsv()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Csv.txt"), ','))
            {
                List<string[]> data = parser.ParseAll();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(3));

                Assert.That(data[0].Length, Is.EqualTo(4));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("Value-1.1"));
                Assert.That(data[0][2], Is.EqualTo("Value-1.2"));
                Assert.That(data[0][3], Is.EqualTo("Value-1.3"));

                Assert.That(data[1].Length, Is.EqualTo(4));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("Value-2.1"));
                Assert.That(data[1][2], Is.EqualTo("Value-2.2"));
                Assert.That(data[1][3], Is.EqualTo("Value-2.3"));

                Assert.That(data[2].Length, Is.EqualTo(4));

                Assert.That(data[2][0], Is.EqualTo("3"));
                Assert.That(data[2][1], Is.EqualTo("Value-3.1"));
                Assert.That(data[2][2], Is.EqualTo("Value-3.2"));
                Assert.That(data[2][3], Is.EqualTo("Value-3.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.ParseAll()"/> method works correctly with a comma delimited file.
        /// </summary>
        [Test]
        public void TestParseAllTab()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Tab.txt"), '\t'))
            {
                List<string[]> data = parser.ParseAll();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(3));

                Assert.That(data[0].Length, Is.EqualTo(4));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("Value-1.1"));
                Assert.That(data[0][2], Is.EqualTo("Value-1.2"));
                Assert.That(data[0][3], Is.EqualTo("Value-1.3"));

                Assert.That(data[1].Length, Is.EqualTo(4));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("Value-2.1"));
                Assert.That(data[1][2], Is.EqualTo("Value-2.2"));
                Assert.That(data[1][3], Is.EqualTo("Value-2.3"));

                Assert.That(data[2].Length, Is.EqualTo(4));

                Assert.That(data[2][0], Is.EqualTo("3"));
                Assert.That(data[2][1], Is.EqualTo("Value-3.1"));
                Assert.That(data[2][2], Is.EqualTo("Value-3.2"));
                Assert.That(data[2][3], Is.EqualTo("Value-3.3"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.ParseAll()"/> method works correctly with a comma delimited text file that uses single quotes to enclose text values.
        /// </summary>
        [Test]
        public void TestParseAllText()
        {
            using (var parser = new DelimitedValueFileParser(Path.Combine(resources, "Text1.txt"), " ", "'"))
            {
                List<string[]> data = parser.ParseAll();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(3));

                Assert.That(data[0].Length, Is.EqualTo(10));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("Value-1.1"));
                Assert.That(data[0][2], Is.EqualTo("This is text value 1.2."));
                Assert.That(data[0][3], Is.EqualTo("1.3"));
                Assert.That(data[0][4], Is.EqualTo(""));
                Assert.That(data[0][5], Is.EqualTo(" This  is text   value 1.4. "));
                Assert.That(data[0][6], Is.EqualTo(""));
                Assert.That(data[0][7], Is.EqualTo("This is text value 1.5."));
                Assert.That(data[0][8], Is.EqualTo(""));
                Assert.That(data[0][9], Is.EqualTo("1.6"));

                Assert.That(data[1].Length, Is.EqualTo(10));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("Value-2.1"));
                Assert.That(data[1][2], Is.EqualTo("This is text value 2.2."));
                Assert.That(data[1][3], Is.EqualTo("2.3"));
                Assert.That(data[1][4], Is.EqualTo(""));
                Assert.That(data[1][5], Is.EqualTo(" This  is text   value 2.4. "));
                Assert.That(data[1][6], Is.EqualTo(""));
                Assert.That(data[1][7], Is.EqualTo("This is text value 2.5."));
                Assert.That(data[1][8], Is.EqualTo(""));
                Assert.That(data[1][9], Is.EqualTo("2.6"));

                Assert.That(data[2].Length, Is.EqualTo(10));

                Assert.That(data[2][0], Is.EqualTo("3"));
                Assert.That(data[2][1], Is.EqualTo("Value-3.1"));
                Assert.That(data[2][2], Is.EqualTo("This is text value 3.2."));
                Assert.That(data[2][3], Is.EqualTo("3.3"));
                Assert.That(data[2][4], Is.EqualTo(""));
                Assert.That(data[2][5], Is.EqualTo(" This  is text   value 3.4. "));
                Assert.That(data[2][6], Is.EqualTo(""));
                Assert.That(data[2][7], Is.EqualTo("This is text value 3.5."));
                Assert.That(data[2][8], Is.EqualTo(""));
                Assert.That(data[2][9], Is.EqualTo("3.6"));
            }
        }
    }
}
