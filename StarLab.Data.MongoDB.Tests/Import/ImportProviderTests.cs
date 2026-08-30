using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Data.Import;

namespace StarLab.Data.MongoDB.Import
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ImportManager"/> class.
    /// </summary>
    public class ImportProviderTests
    {
        private const string COLLECTION = "stars";

        private const string DATABASE = "test";

        private readonly IImportDefinition importDefinition;

        private readonly IDatabaseManager databases;

        /// <summary>
        /// Initialises a new instance of the <see cref="ImportProviderTests"/> class.
        /// </summary>
        public ImportProviderTests()
        {
            databases = new DatabaseManager();
            
            databases.OpenConnection("localhost", 27017);

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
            databases.Dispose();
        }

        /// <summary>
        /// Deletes the test collection after each test has been run.
        /// </summary>
        [TearDown]
        public void CleanUpTest()
        {
            if (databases.GetDatabase(DATABASE) is Database db)
            {
                db.DropCollection(COLLECTION);
            }
        }

        /// <summary>
        /// Test that the <see cref="ImportManager(Connection)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var provider = new ImportManager(databases);

            Assert.That(provider, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ImportManager.Import(IDataset, string, string)"/> method works correctly when provided with a <see cref="FileBackedDataset"/> containing data for 1000 stars.
        /// </summary>
        [Test]
        public void TestImport()
        {
            var provider = new ImportManager(databases);

            using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
            {
                provider.Import(dataset, DATABASE, COLLECTION);
            }

            if (databases.GetDatabase(DATABASE) is Database db)
            {
                var collection = db.GetCollection(COLLECTION);

                var count = collection.CountDocuments(Builders<BsonDocument>.Filter.Empty);

                Assert.That(count, Is.EqualTo(1000));
            }
            else
            {
                Assert.Fail();
            }
        }
    }
}
