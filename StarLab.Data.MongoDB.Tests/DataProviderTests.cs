using StarLab.Application.Data.Import;
using StarLab.Data.Import;
using StarLab.Data.MongoDB.Import;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DataProvider"/> class.
    /// </summary>
    public class DataProviderTests
    {
        private const string COLLECTION = "stars";

        private const string DATABASE = "test";

        private readonly IImportDefinition importDefinition;

        private readonly Connection connection;

        /// <summary>
        /// Initialises a new instance of the <see cref="DataProviderTests"/> class. 
        /// </summary>
        public DataProviderTests()
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

            var provider = new ImportProvider(connection);

            using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
            {
                provider.Import(dataset, DATABASE, COLLECTION);
            }
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
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method correctly returns the first twenty stars.
        /// </summary>
        [Test]
        public void TestGetFirstTwentyStars()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 20);

            Assert.That(stars, Is.Not.Null);
            Assert.That(stars, Has.Count.EqualTo(20));

            // TODO - Check that these are the first twenty stars
        }

        /// <summary>
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing an equals predicate.
        /// </summary>
        [Test]
        public void TestGetStarsWithEqualsQuery()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.Equals)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 1000);

            Assert.That(stars, Is.Not.Null);

            Assert.That(stars, Has.Count.EqualTo(10));

            for (int n = 0; n < stars.Count; n++)
            {
                Assert.That(stars[n].ApparentMagnitude, Is.EqualTo(8.55));
            }
        }

        /// <summary>
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a greater than predicate.
        /// </summary>
        [Test]
        public void TestGetStarsWithGreaterThanQuery()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("B-V"), 1.67, ComparisonOperators.GreaterThan)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 1000);

            Assert.That(stars, Is.Not.Null);

            Assert.That(stars, Has.Count.EqualTo(13));

            for (int n = 0; n < stars.Count; n++)
            {
                Assert.That(stars[n].BVColourIndex, Is.GreaterThan(1.67));
            }
        }

        /// <summary>
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a greater than or equals predicate.
        /// </summary>
        [Test]
        public void TestGetStarsWithGreaterThanOrEqualsQuery()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("B-V"), 1.67, ComparisonOperators.GreaterThanOrEquals)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 1000);

            Assert.That(stars, Is.Not.Null);

            Assert.That(stars, Has.Count.EqualTo(14));

            for (int n = 0; n < stars.Count; n++)
            {
                Assert.That(stars[n].BVColourIndex, Is.GreaterThanOrEqualTo(1.67));
            }
        }

        /// <summary>
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a less than predicate.
        /// </summary>
        [Test]
        public void TestGetStarsWithLessThanQuery()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("B-V"), -0.089, ComparisonOperators.LessThan)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 1000);

            Assert.That(stars, Is.Not.Null);

            Assert.That(stars, Has.Count.EqualTo(8));

            for (int n = 0; n < stars.Count; n++)
            {
                Assert.That(stars[n].BVColourIndex, Is.LessThan(-0.089));
            }
        }

        /// <summary>
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a less than or equals predicate.
        /// </summary>
        [Test]
        public void TestGetStarsWithLessThanOrEqualsQuery()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("B-V"), -0.089, ComparisonOperators.LessThanOrEquals)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 1000);

            Assert.That(stars, Is.Not.Null);

            Assert.That(stars, Has.Count.EqualTo(9));

            for (int n = 0; n < stars.Count; n++)
            {
                Assert.That(stars[n].BVColourIndex, Is.LessThanOrEqualTo(-0.089));
            }
        }

        /// <summary>
        /// Test that the <see cref="DataProvider.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a not equals predicate.
        /// </summary>
        [Test]
        public void TestGetStarsWithNotEqualsQuery()
        {
            var provider = new DataProvider(connection);

            provider.OpenDatabase(DATABASE);

            var builder = new QueryBuilder();

            var query = builder.AddTable(COLLECTION)
                               .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.NotEquals)
                               .BuildQuery();

            var stars = provider.GetStars(query, 0, 1000);

            Assert.That(stars, Is.Not.Null);

            Assert.That(stars, Has.Count.EqualTo(990));

            for (int n = 0; n < stars.Count; n++)
            {
                Assert.That(stars[n].ApparentMagnitude, Is.Not.EqualTo(8.55));
            }
        }

        // TODO - And and Or queries
    }
}
