using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Application.Data.Import;
using StarLab.Data.Import;

namespace StarLab.Data.MongoDB.Import
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ImportProvider"/> class.
    /// </summary>
    public class ImportProviderTests
    {
        private const string COLLECTION = "stars";

        private const string DATABASE = "test";

        private readonly IImportDefinition importDefinition;

        private readonly Connection connection;

        /// <summary>
        /// Initialises a new instance of the <see cref="ImportProviderTests"/> class.
        /// </summary>
        public ImportProviderTests()
        {
            connection = new Connection();

            importDefinition = ImportDefinitionBuilder.GetInstance("|")
                .AddField(5, "Apparent Magnitude", DataTypes.Decimal)
                .AddField(8, "RightAscension", DataTypes.Decimal)
                .AddField(9, "Declination", DataTypes.Decimal)
                .AddField(11, "Parallax", DataTypes.Decimal)
                .AddField(37, "B-V", DataTypes.Decimal)
                .AddField(40, "V-I", DataTypes.Decimal)
                .AddField(76, "Spectral Type", DataTypes.Text)
                .AddCompoundField("ID", "{0}-{1}", [0, 1])
                .Build();
        }

        /// <summary>
        /// Deletes the test database after all the tests have been run.
        /// </summary>
        [OneTimeTearDown]
        public void CleanUpFixture()
        {
            connection.DropDatabase(DATABASE);

            connection.Close();
        }

        /// <summary>
        /// Deletes the test collection after each test has been run.
        /// </summary>
        [TearDown]
        public void CleanUpTest()
        {
            connection.GetDatabase(DATABASE).DropCollection(COLLECTION);
        }

        /// <summary>
        /// Creates the database connection prior to running the tests.
        /// </summary>
        [OneTimeSetUp]
        public void InitialiseFixture()
        {
            connection.Open();
        }

        /// <summary>
        /// Test that the <see cref="ImportProvider(Connection)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var provider = new ImportProvider(connection);

            Assert.That(provider, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ImportProvider.Import(IDataset, string, string)"/> method works correctly when provided with a <see cref="FileBackedDataset"/> containing data for 1000 stars.
        /// </summary>
        [Test]
        public void TestImport()
        {
            var provider = new ImportProvider(connection);

            using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
            {
                provider.Import(dataset, DATABASE, COLLECTION);
            }

            var collection = connection.GetDatabase(DATABASE).GetCollection<BsonDocument>(COLLECTION);

            var count = collection.CountDocuments(Builders<BsonDocument>.Filter.Empty);

            Assert.That(count, Is.EqualTo(1000));
        }
    }
}
