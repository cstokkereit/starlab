namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Arguments"/> class.
    /// </summary>
    public class ArgumentsTests
    {
        /// <summary>
        /// Test that the <see cref="Arguments()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var arguments = new Arguments();

            Assert.That(arguments, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Arguments.Count"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetCount()
        {
            var arguments = new Arguments();

            Assert.That(arguments.Count, Is.EqualTo(0));

            arguments.Add("N1", "V1");

            Assert.That(arguments.Count, Is.EqualTo(1));
        }

        /// <summary>
        /// Test that the <see cref="Arguments.Names"/> property returns a list containing the correct names.
        /// </summary>
        [Test]
        public void TestGetNames()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N2", "V2");
            arguments.Add("N3", "V3");

            var names = arguments.Names;

            Assert.That(names[0], Is.EqualTo("N1"));
            Assert.That(names[1], Is.EqualTo("N2"));
            Assert.That(names[2], Is.EqualTo("N3"));
        }

        /// <summary>
        /// Test that the <see cref="Arguments.Names"/> property returns an empty list when no arguments have been added.
        /// </summary>
        [Test]
        public void TestGetNamesWhenEmpty()
        {
            var arguments = new Arguments();

            var names = arguments.Names;

            Assert.IsNotNull(names);
            Assert.That(names, Has.Count.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="Arguments.this[string]"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetThis()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N2", "V2");
            arguments.Add("N3", "V3");

            Assert.That(arguments["N2"], Is.EqualTo("V2"));
        }

        /// <summary>
        /// Test that the <see cref="Arguments.this[string]"/> property throws an exception when an invalid key is used.
        /// </summary>
        [Test]
        public void TestGetThisWithInvalidName()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N2", "V2");
            arguments.Add("N3", "V3");

            Assert.Throws<KeyNotFoundException>(() => { var arg = arguments["N4"]; });
        }

        /// <summary>
        /// Test that the <see cref="Arguments.Add(string, object)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAdd()
        {
            var arguments = new Arguments();

            Assert.That(arguments, Has.Count.EqualTo(0));

            arguments.Add("N1", "V1");

            Assert.That(arguments, Has.Count.EqualTo(1));
            Assert.That(arguments["N1"], Is.EqualTo("V1"));

            arguments.Add("N2", "V2");

            Assert.That(arguments, Has.Count.EqualTo(2));
            Assert.That(arguments["N1"], Is.EqualTo("V1"));
            Assert.That(arguments["N2"], Is.EqualTo("V2"));

            arguments.Add("N3", "V3");

            Assert.That(arguments, Has.Count.EqualTo(3));
            Assert.That(arguments["N1"], Is.EqualTo("V1"));
            Assert.That(arguments["N2"], Is.EqualTo("V2"));
            Assert.That(arguments["N3"], Is.EqualTo("V3"));
        }

        /// <summary>
        /// Test that the <see cref="Arguments.Add(string, object)"/> method throws an exception when adding an argument with the same name as an existing argument.
        /// </summary>
        [Test]
        public void TestAddWithExistingName()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");

            Assert.Throws<ArgumentException>(() => arguments.Add("N1", "V2"));
        }
    }
}
