namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Connection"/> class.
    /// </summary>
    public class ConnectionTests
    {
        /// <summary>
        /// Test that the static <see cref="Connection.OpenConnection(string, int)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenConnectionGivenHostAndPort()
        {
            var connection = new Connection("localhost", 27017);

            Assert.That(connection, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Connection.ConnectionString"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetConnectionString()
        {
            var connection = new Connection("localhost", 27017);

            Assert.That(connection.ConnectionString, Is.EqualTo("mongodb://localhost:27017"));
        }

        /// <summary>
        /// Test that the <see cref="Connection.Name"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetDatabaseName()
        {
            var connection = new Connection("localhost", 27017);

            Assert.That(connection.Name, Is.EqualTo("localhost:27017"));
        }
    }
}
