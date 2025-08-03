using System.Reflection;

namespace Stratosoft.File.IO
{
    /// <summary>
    /// Abstract base class for performing unit tests on file parsers.
    /// </summary>
    public abstract class ParserTests
    {
        protected readonly string resources; // The path to the test resources folder.

        /// <summary>
        /// Initialises a new instance of the <see cref="DelimitedValueFileParserTests"/> class.
        /// </summary>
        public ParserTests()
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
        }
    }
}
