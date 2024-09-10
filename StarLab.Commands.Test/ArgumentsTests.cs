namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Arguments"/> class.
    /// </summary>
    [TestClass]
    public class ArgumentsTests
    {
        /// <summary>
        /// Test that the Arguments() constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var arguments = new Arguments();

            Assert.IsNotNull(arguments);
        }

        /// <summary>
        /// Test that the Count property returns the correct value.
        /// </summary>
        [TestMethod]
        public void TestGetCount()
        {
            var arguments = new Arguments();

            Assert.AreEqual(0, arguments.Count);

            arguments.Add("N1", "V1");

            Assert.AreEqual(1, arguments.Count);
        }

        /// <summary>
        /// Test that the Names property returns a list containing the correct names.
        /// </summary>
        [TestMethod]
        public void TestGetNames()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N2", "V2");
            arguments.Add("N3", "V3");

            var names = arguments.Names;

            Assert.AreEqual("N1", names[0]);
            Assert.AreEqual("N2", names[1]);
            Assert.AreEqual("N3", names[2]);
        }

        /// <summary>
        /// Test that the Names property returns an empty list when no arguments have been added.
        /// </summary>
        [TestMethod]
        public void TestGetNamesWhenEmpty()
        {
            var arguments = new Arguments();

            var names = arguments.Names;

            Assert.IsNotNull(names);
            Assert.AreEqual(0, names.Count);
        }

        /// <summary>
        /// Test that the this[string] property returns the correct value.
        /// </summary>
        [TestMethod]
        public void TestGetThis()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N2", "V2");
            arguments.Add("N3", "V3");

            Assert.AreEqual("V2", arguments["N2"]);
        }

        /// <summary>
        /// Test that the this[string] property throws an exception when an invalid key is used.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestGetThisWithInvalidName()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N2", "V2");
            arguments.Add("N3", "V3");

            var value = arguments["N4"];
        }

        /// <summary>
        /// Test that the Add(string, object) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestAdd()
        {
            var arguments = new Arguments();

            Assert.AreEqual(0, arguments.Count);

            arguments.Add("N1", "V1");

            Assert.AreEqual(1, arguments.Count);
            Assert.AreEqual("V1", arguments["N1"]);

            arguments.Add("N2", "V2");

            Assert.AreEqual(2, arguments.Count);
            Assert.AreEqual("V1", arguments["N1"]);
            Assert.AreEqual("V2", arguments["N2"]);

            arguments.Add("N3", "V3");

            Assert.AreEqual(3, arguments.Count);
            Assert.AreEqual("V1", arguments["N1"]);
            Assert.AreEqual("V2", arguments["N2"]);
            Assert.AreEqual("V3", arguments["N3"]);
        }

        /// <summary>
        /// Test that the Add(string, object) method throws an exception when adding an argument with the same name as an existing argument.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddWithExistingName()
        {
            var arguments = new Arguments();

            arguments.Add("N1", "V1");
            arguments.Add("N1", "V2");
        }
    }
}
