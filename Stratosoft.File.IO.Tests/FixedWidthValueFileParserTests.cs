namespace Stratosoft.File.IO
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="FixedWidthValueFileParser"/> class.
    /// </summary>
    public class FixedWidthValueFileParserTests : ParserTests
    {
        /// <summary>
        /// Test that the <see cref="FixedWidthValueFileParser(string, bool)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructorWithFileNameCharDelimiterAndCharTextDelimiter()
        {
            var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [1], false);

            Assert.That(parser, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FixedWidthValueFileParser.Parse()"/> method works correctly with a fixed width file when the option to remove white-space characters is disabled.
        /// </summary>
        [Test]
        public void TestParseWithTrimWhitespaceDisabled()
        {
            using (var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [2, 3, 8, 24, 4, 12], false))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(6));
                Assert.That(data[0], Is.EqualTo("1 "));
                Assert.That(data[1], Is.EqualTo("1.1"));
                Assert.That(data[2], Is.EqualTo(" 111.111"));
                Assert.That(data[3], Is.EqualTo("This is some text       "));
                Assert.That(data[4], Is.EqualTo("   1"));
                Assert.That(data[5], Is.EqualTo(" 01-Jun-1991"));
            }
        }

        /// <summary>
        /// Test that the <see cref="FixedWidthValueFileParser.Parse()"/> method works correctly with a fixed width file when the option to remove white-space characters is enabled.
        /// </summary>
        [Test]
        public void TestParseWithTrimWhitespaceEnabled()
        {
            using (var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [2, 3, 8, 24, 4, 12], true))
            {
                string[] data = parser.Parse();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Length, Is.EqualTo(6));
                Assert.That(data[0], Is.EqualTo("1"));
                Assert.That(data[1], Is.EqualTo("1.1"));
                Assert.That(data[2], Is.EqualTo("111.111"));
                Assert.That(data[3], Is.EqualTo("This is some text"));
                Assert.That(data[4], Is.EqualTo("1"));
                Assert.That(data[5], Is.EqualTo("01-Jun-1991"));
            }
        }

        /// <summary>
        /// Test that the <see cref="FixedWidthValueFileParser.Parse(int)"/> method works correctly with a fixed width file when the option to remove white-space characters is disabled.
        /// </summary>
        [Test]
        public void TestParseTwoLinesWithTrimWhitespaceDisabled()
        {
            using (var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [2, 3, 8, 24, 4, 12], false))
            {
                List<string[]> data = parser.Parse(2);

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(2));

                Assert.That(data[0].Length, Is.EqualTo(6));

                Assert.That(data[0][0], Is.EqualTo("1 "));
                Assert.That(data[0][1], Is.EqualTo("1.1"));
                Assert.That(data[0][2], Is.EqualTo(" 111.111"));
                Assert.That(data[0][3], Is.EqualTo("This is some text       "));
                Assert.That(data[0][4], Is.EqualTo("   1"));
                Assert.That(data[0][5], Is.EqualTo(" 01-Jun-1991"));

                Assert.That(data[1].Length, Is.EqualTo(6));

                Assert.That(data[1][0], Is.EqualTo("2 "));
                Assert.That(data[1][1], Is.EqualTo("2.1"));
                Assert.That(data[1][2], Is.EqualTo("  22.222"));
                Assert.That(data[1][3], Is.EqualTo("This is some more text  "));
                Assert.That(data[1][4], Is.EqualTo("  22"));
                Assert.That(data[1][5], Is.EqualTo(" 20-Mar-1992"));
            }
        }

        /// <summary>
        /// Test that the <see cref="FixedWidthValueFileParser.Parse(int)"/> method works correctly with a fixed width file when the option to remove white-space characters is enabled.
        /// </summary>
        [Test]
        public void TestParseTwoLinesWithTrimWhitespaceEnabled()
        {
            using (var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [2, 3, 8, 24, 4, 12], true))
            {
                List<string[]> data = parser.Parse(2);

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(2));

                Assert.That(data[0].Length, Is.EqualTo(6));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("1.1"));
                Assert.That(data[0][2], Is.EqualTo("111.111"));
                Assert.That(data[0][3], Is.EqualTo("This is some text"));
                Assert.That(data[0][4], Is.EqualTo("1"));
                Assert.That(data[0][5], Is.EqualTo("01-Jun-1991"));

                Assert.That(data[1].Length, Is.EqualTo(6));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("2.1"));
                Assert.That(data[1][2], Is.EqualTo("22.222"));
                Assert.That(data[1][3], Is.EqualTo("This is some more text"));
                Assert.That(data[1][4], Is.EqualTo("22"));
                Assert.That(data[1][5], Is.EqualTo("20-Mar-1992"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.ParseAll()"/> method works correctly with a fixed width file when the option to remove white-space characters is disabled.
        /// </summary>
        [Test]
        public void TestParseAllWithTrimWhitespaceDisabled()
        {
            using (var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [2, 3, 8, 24, 4, 12], false))
            {
                List<string[]> data = parser.ParseAll();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(3));

                Assert.That(data[0].Length, Is.EqualTo(6));

                Assert.That(data[0][0], Is.EqualTo("1 "));
                Assert.That(data[0][1], Is.EqualTo("1.1"));
                Assert.That(data[0][2], Is.EqualTo(" 111.111"));
                Assert.That(data[0][3], Is.EqualTo("This is some text       "));
                Assert.That(data[0][4], Is.EqualTo("   1"));
                Assert.That(data[0][5], Is.EqualTo(" 01-Jun-1991"));

                Assert.That(data[1].Length, Is.EqualTo(6));

                Assert.That(data[1][0], Is.EqualTo("2 "));
                Assert.That(data[1][1], Is.EqualTo("2.1"));
                Assert.That(data[1][2], Is.EqualTo("  22.222"));
                Assert.That(data[1][3], Is.EqualTo("This is some more text  "));
                Assert.That(data[1][4], Is.EqualTo("  22"));
                Assert.That(data[1][5], Is.EqualTo(" 20-Mar-1992"));

                Assert.That(data[2].Length, Is.EqualTo(6));

                Assert.That(data[2][0], Is.EqualTo("3 "));
                Assert.That(data[2][1], Is.EqualTo("3.1"));
                Assert.That(data[2][2], Is.EqualTo("   3.333"));
                Assert.That(data[2][3], Is.EqualTo("This is some longer text"));
                Assert.That(data[2][4], Is.EqualTo(" 333"));
                Assert.That(data[2][5], Is.EqualTo(" 30-Oct-1993"));
            }
        }

        /// <summary>
        /// Test that the <see cref="DelimitedValueFileParser.ParseAll()"/> method works correctly with a fixed width file when the option to remove white-space characters is enabled.
        /// </summary>
        [Test]
        public void TestParseAllWithTrimWhitespaceEnabled()
        {
            using (var parser = new FixedWidthValueFileParser(Path.Combine(resources, "Fixed.txt"), [2, 3, 8, 24, 4, 12], true))
            {
                List<string[]> data = parser.ParseAll();

                Assert.That(data, Is.Not.Null);
                Assert.That(data.Count, Is.EqualTo(3));

                Assert.That(data[0].Length, Is.EqualTo(6));

                Assert.That(data[0][0], Is.EqualTo("1"));
                Assert.That(data[0][1], Is.EqualTo("1.1"));
                Assert.That(data[0][2], Is.EqualTo("111.111"));
                Assert.That(data[0][3], Is.EqualTo("This is some text"));
                Assert.That(data[0][4], Is.EqualTo("1"));
                Assert.That(data[0][5], Is.EqualTo("01-Jun-1991"));

                Assert.That(data[1].Length, Is.EqualTo(6));

                Assert.That(data[1][0], Is.EqualTo("2"));
                Assert.That(data[1][1], Is.EqualTo("2.1"));
                Assert.That(data[1][2], Is.EqualTo("22.222"));
                Assert.That(data[1][3], Is.EqualTo("This is some more text"));
                Assert.That(data[1][4], Is.EqualTo("22"));
                Assert.That(data[1][5], Is.EqualTo("20-Mar-1992"));

                Assert.That(data[2].Length, Is.EqualTo(6));

                Assert.That(data[2][0], Is.EqualTo("3"));
                Assert.That(data[2][1], Is.EqualTo("3.1"));
                Assert.That(data[2][2], Is.EqualTo("3.333"));
                Assert.That(data[2][3], Is.EqualTo("This is some longer text"));
                Assert.That(data[2][4], Is.EqualTo("333"));
                Assert.That(data[2][5], Is.EqualTo("30-Oct-1993"));
            }
        }
    }
}
