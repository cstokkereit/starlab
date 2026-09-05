using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Data.Import;
using StarLab.Data.MongoDB.Import;
using StarLab.Domain.Entities;

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

        private readonly IDatabaseManager manager;

        private readonly Database database;

        /// <summary>
        /// Initialises a new instance of the <see cref="StarsTests"/> class.
        /// </summary>
        public StarsTests()
        {
            manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);

            importDefinition = ImportDefinitionBuilder.GetInstance("|")
                .AddField(5, "ApparentMagnitude", DataTypes.Decimal)
                .AddField(8, "RightAscension", DataTypes.Decimal)
                .AddField(9, "Declination", DataTypes.Decimal)
                .AddField(11, "Parallax", DataTypes.Decimal)
                .AddField(37, "B-V", DataTypes.Decimal)
                .AddField(40, "V-I", DataTypes.Decimal)
                .AddField(76, "SpectralType", DataTypes.Text)
                .AddCompoundField("ID", "{0}-{1}", [0, 1])
                .Build();

            database = (Database)manager.GetDatabase(DATABASE);
        }

        /// <summary>
        /// Creates the test database prior to running the tests.
        /// </summary>
        [OneTimeSetUp]
        public void SetUp()
        {
            var provider = new ImportManager(manager);

            using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
            {
                provider.Import(dataset, DATABASE, COLLECTION);
            }
        }

        /// <summary>
        /// Deletes the test database after all the tests have been run.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            manager.GetDatabase(DATABASE).DropCollection(COLLECTION);

            manager.DropDatabase(DATABASE);

            manager.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="Stars(IAsyncCursor{BsonDocument})"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION).BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            Assert.That(stars, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Stars.Current"/> property returns null when the cursor been moved beyond the last record.
        /// </summary>
        [Test]
        public void TestCurrentAfterLast()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION).BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            while (stars.MoveNext())
            {
                // Do Nothing
            }

            stars.MoveNext();

            Assert.That(stars.Current, Is.Null);
        }

        /// <summary>
        /// Test that the <see cref="Stars.Current"/> property returns null when the cursor has not yet been moved to the first record.
        /// </summary>
        [Test]
        public void TestCurrentBeforeFirst()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION).BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            Assert.That(stars.Current, Is.Null);
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns 10 stars.
        /// </summary>
        [Test]
        public void TestMoveNextWith10Stars()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.Equals)
                               .BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            Validate(stars, 10, s => Assert.That(s.ApparentMagnitude, Is.EqualTo(8.55)));
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns 990 stars.
        /// </summary>
        [Test]
        public void TestMoveNextWith990Stars()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.NotEquals)
                               .BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            Validate(stars, 990, s => Assert.That(s.ApparentMagnitude, Is.Not.EqualTo(8.55)));
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns all stars.
        /// </summary>
        [Test]
        public void TestMoveNextWithAllStars()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            Validate(stars, 1000, s => { });
        }

        /// <summary>
        /// Test that the <see cref="Stars.MoveNext()"/> method works correctly when provided with an <see cref="IQuery"/> that returns no stars.
        /// </summary>
        [Test]
        public void TestMoveNextWithNoStars()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 20, ComparisonOperators.Equals)
                               .BuildQuery();

            var collection = database.GetCollection(COLLECTION);

            var stars = new Stars(collection.Find(((Query)query).GetFilter()).ToCursor());

            Validate(stars, 0, s => { });
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
