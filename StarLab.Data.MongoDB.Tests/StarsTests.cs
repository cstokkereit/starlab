using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Application.Data.Import;
using StarLab.Data.Import;
using StarLab.Data.MongoDB.Import;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Stars"/> class.
    /// </summary>
    public class StarsTests
    {
        private const string COLLECTION = "stars";

        private const string DATABASE = "test";

        private readonly IImportDefinition importDefinition;

        private readonly Connection connection;

        private IMongoDatabase database;

        /// <summary>
        /// Initialises a new instance of the <see cref="StarsTests"/> class.
        /// </summary>
        public StarsTests()
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
            connection.GetDatabase(DATABASE).DropCollection(COLLECTION);

            connection.DropDatabase(DATABASE);

            connection.Close();
        }

        /// <summary>
        /// Creates the test database prior to running the tests.
        /// </summary>
        [OneTimeSetUp]
        public void InitialiseFixture()
        {
            connection.Open();

            var provider = new ImportManager(connection);

            using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
            {
                provider.Import(dataset, DATABASE, COLLECTION);
            }

            database = connection.GetDatabase(DATABASE);
        }

        /// <summary>
        /// Test that the <see cref="Stars(IAsyncCursor{BsonDocument})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            // Arrange
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .BuildQuery();

            var collection = database.GetCollection<BsonDocument>(COLLECTION);

            // Act
            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            // Assert
            Assert.That(stars, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Stars.Current"/> property returns null when the cursor been moved beyond the last record.
        /// </summary>
        [Test]
        public void TestCurrentAfterLast()
        {
            // Arrange
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .BuildQuery();

            var collection = database.GetCollection<BsonDocument>(COLLECTION);

            // Act
            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            while (stars.MoveNext())
            {
                // Do Nothing
            }

            stars.MoveNext();

            // Assert
            Assert.That(stars.Current, Is.Null);
        }

        /// <summary>
        /// Test that the <see cref="Stars.Current"/> property returns null when the cursor has not yet been moved to the first record.
        /// </summary>
        [Test]
        public void TestCurrentBeforeFirst()
        {
            // Arrange
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .BuildQuery();

            var collection = database.GetCollection<BsonDocument>(COLLECTION);

            // Act
            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            // Assert
            Assert.That(stars.Current, Is.Null);
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns 10 stars.
        /// </summary>
        [Test]
        public void TestMoveNextWith10Stars()
        {
            // Arrange
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.Equals)
                               .BuildQuery();

            var collection = database.GetCollection<BsonDocument>(COLLECTION);

            // Act
            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            // Assert
            Validate(stars, 10, s => Assert.That(s.ApparentMagnitude, Is.EqualTo(8.55)));
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns 990 stars.
        /// </summary>
        [Test]
        public void TestMoveNextWith990Stars()
        {
            // Arrange
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.NotEquals)
                               .BuildQuery();

            // Act
            var stars = provider.GetStars(query);

            // Assert
            Validate(stars, 990, s => Assert.That(s.ApparentMagnitude, Is.Not.EqualTo(8.55)));
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns all stars.
        /// </summary>
        [Test]
        public void TestMoveNextWithAllStars()
        {
            // Arrange
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .BuildQuery();

            // Act
            var stars = provider.GetStars(query);

            // Assert
            Validate(stars, 1000, s => {});
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns no stars.
        /// </summary>
        [Test]
        public void TestMoveNextWithNoStars()
        {
            // Arrange
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 20, ComparisonOperators.Equals)
                               .BuildQuery();

            var collection = database.GetCollection<BsonDocument>(COLLECTION);

            // Act
            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            // Assert
            Validate(stars, 0, s => {});
        }

        /// <summary>
        /// Validates the <see cref="IForwardOnlyCursor{IStar}"/> provided.
        /// </summary>
        /// <param name="stars">The <see cref="IForwardOnlyCursor{IStar}"/> being validated.</param>
        /// <param name="count">The expected number of items returned by the cursor.</param>
        /// <param name="validate">An <see cref="Action{IStar}"/> that validates the items returned by the cursor.</param>
        private void Validate(IForwardOnlyCursor<IStar> stars, int count, Action<IStar> validate)
        {
            Assert.That(stars, Is.Not.Null);

            var n = 0;

            while (stars.MoveNext())
            {
                Assert.That(stars.Current, Is.Not.Null);
                validate(stars.Current);
                n++;
            }

            Assert.That(n, Is.EqualTo(count));
        }
    }
}
