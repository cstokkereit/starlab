namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DatabaseManager"/> class.
    /// </summary>
    public class DatabaseManagerTests
    {
        /// <summary>
        /// Test that the <see cref="DatabaseManager()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var manager = new DatabaseManager();

            Assert.That(manager, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager.GetDatabase(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetDatabase()
        {
            var manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);

            var database = manager.GetDatabase("local");

            Assert.That (database, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="DatabaseManager.OpenConnection(string, int)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenConnection()
        {
            var manager = new DatabaseManager();

            manager.OpenConnection("localhost", 27017);

            Assert.Pass();
        }




        //private const string COLLECTION = "stars";

        //private const string DATABASE = "test";

        //private readonly IImportDefinition importDefinition;

        //private readonly Connection connection;

        ///// <summary>
        ///// Initialises a new instance of the <see cref="DataProviderTests"/> class.
        ///// </summary>
        //public DataProviderTests()
        //{
        //    connection = Connection.OpenConnection("mongodb://localhost:27017");

        //    importDefinition = ImportDefinitionBuilder.GetInstance("|")
        //        .AddField(5, "Apparent Magnitude", DataTypes.Decimal)
        //        .AddField(8, "RightAscension", DataTypes.Decimal)
        //        .AddField(9, "Declination", DataTypes.Decimal)
        //        .AddField(11, "Parallax", DataTypes.Decimal)
        //        .AddField(37, "B-V", DataTypes.Decimal)
        //        .AddField(40, "V-I", DataTypes.Decimal)
        //        .AddField(76, "Spectral Type", DataTypes.Text)
        //        .AddCompoundField("ID", "{0}-{1}", [0, 1])
        //        .Build();
        //}

        

        ///// <summary>
        ///// Creates the test database prior to running the tests.
        ///// </summary>
        //[OneTimeSetUp]
        //public void InitialiseFixture()
        //{
        //    var provider = new ImportManager(connection);

        //    using (var dataset = new FileBackedDataset(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Stars.dat"), importDefinition))
        //    {
        //        provider.Import(dataset, DATABASE, COLLECTION);
        //    }
        //}

        ///// <summary>
        ///// Test that the <see cref="ImportManager(Connection)"/> constructor works correctly.
        ///// </summary>
        //[Test]
        //public void TestConstructor()
        //{
        //    var provider = new ImportManager(connection);

        //    Assert.That(provider, Is.Not.Null);
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.CloseDatabase()"/> method works correctly.
        ///// </summary>
        //[Test]
        //public void TestCloseDatabase()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    provider.CloseDatabase();

        //    provider.OpenDatabase(DATABASE);

        //    Assert.Pass();
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method correctly returns the first twenty stars.
        ///// </summary>
        //[Test]
        //public void TestGetFirstTwentyStars()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 20);

        //    Assert.That(stars, Is.Not.Null);
        //    Assert.That(stars, Has.Count.EqualTo(20));

        //    // TODO - Check that these are the first twenty stars
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery)"/> method works correctly when all stars are included in the query.
        ///// </summary>
        //[Test]
        //public void TestGetStarsReturnsAllStarsAsCursor()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query);

        //    Assert.That(stars, Is.Not.Null);

        //    Validate(stars, 1000);
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when all stars are included in the query.
        ///// </summary>
        //[Test]
        //public void TestGetStarsReturnsAllStarsAsList()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 2000);

        //    Assert.That(stars, Is.Not.Null);

        //    Validate(stars, 1000, s => {});
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method throws an <see cref="InvalidOperationException"/> if the database has not been opened.
        ///// </summary>
        //[Test]
        //public void TestGetStarsThrowsExceptionIfDatabaseNotOpened()
        //{
        //    var provider = new DatabaseManager(connection);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .BuildQuery();

        //    Assert.Throws<InvalidOperationException>(() => provider.GetStars(query, 0, 20));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing an equals predicate.
        ///// </summary>
        //[Test]
        //public void TestGetStarsWithEqualsQuery()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.Equals)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 1000);

        //    Validate(stars, 10, s => Assert.That(s.ApparentMagnitude, Is.EqualTo(8.55)));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a greater than predicate.
        ///// </summary>
        //[Test]
        //public void TestGetStarsWithGreaterThanQuery()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .AddPredicate(builder.CreateField("B-V"), 1.67, ComparisonOperators.GreaterThan)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 1000);

        //    Validate(stars, 13, s => Assert.That(s.BVColourIndex, Is.GreaterThan(1.67)));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a greater than or equals predicate.
        ///// </summary>
        //[Test]
        //public void TestGetStarsWithGreaterThanOrEqualsQuery()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .AddPredicate(builder.CreateField("B-V"), 1.67, ComparisonOperators.GreaterThanOrEquals)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 1000);

        //    Validate(stars, 14, s => Assert.That(s.BVColourIndex, Is.GreaterThanOrEqualTo(1.67)));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a less than predicate.
        ///// </summary>
        //[Test]
        //public void TestGetStarsWithLessThanQuery()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .AddPredicate(builder.CreateField("B-V"), -0.089, ComparisonOperators.LessThan)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 1000);

        //    Validate(stars, 8, s => Assert.That(s.BVColourIndex, Is.LessThan(-0.089)));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a less than or equals predicate.
        ///// </summary>
        //[Test]
        //public void TestGetStarsWithLessThanOrEqualsQuery()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .AddPredicate(builder.CreateField("B-V"), -0.089, ComparisonOperators.LessThanOrEquals)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 1000);

        //    Validate(stars, 9, s => Assert.That(s.BVColourIndex, Is.LessThanOrEqualTo(-0.089)));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.GetStars(IQuery, int, int)"/> method works correctly when provided with an <see cref="IQuery"/> containing a not equals predicate.
        ///// </summary>
        //[Test]
        //public void TestGetStarsWithNotEqualsQuery()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    var builder = new QueryBuilder();

        //    var query = builder.AddTable(COLLECTION)
        //                       .AddPredicate(builder.CreateField("Apparent Magnitude"), 8.55, ComparisonOperators.NotEquals)
        //                       .BuildQuery();

        //    var stars = provider.GetStars(query, 0, 1000);

        //    Validate(stars, 990, s => Assert.That(s.ApparentMagnitude, Is.Not.EqualTo(8.55)));
        //}

        ///// <summary>
        ///// Test that the <see cref="DatabaseManager.OpenDatabase(string)"/> method throws an <see cref="InvalidOperationException"/> if the database has already been opened.
        ///// </summary>
        //[Test]
        //public void TestOpenDatabaseThrowsExceptionIfDatabaseAlreadyOpened()
        //{
        //    var provider = new DatabaseManager(connection);

        //    provider.OpenDatabase(DATABASE);

        //    Assert.Throws<InvalidOperationException>(() => provider.OpenDatabase(DATABASE));
        //}

        //// TODO - And and Or queries

        ///// <summary>
        ///// Validates the <see cref="IForwardOnlyCursor{IStar}"/> provided.
        ///// </summary>
        ///// <param name="stars">The <see cref="IForwardOnlyCursor{IStar}"/> being validated.</param>
        ///// <param name="count">The expected number of items returned by the cursor.</param>
        //private void Validate(IForwardOnlyCursor<IStar> stars, int count)
        //{
        //    Assert.That(stars, Is.Not.Null);

        //    var n = 0;

        //    while (stars.MoveNext())
        //    {
        //        Assert. That(stars.Current, Is.Not.Null);
        //        n++;
        //    }

        //    Assert.That(n, Is.EqualTo(count));
        //}

        ///// <summary>
        ///// Validates the <see cref="IList{IStar}"/> provided.
        ///// </summary>
        ///// <param name="stars">The <see cref="IList{IStar}"/> being validated.</param>
        ///// <param name="count">The expected number of items in the list.</param>
        ///// <param name="validate">An <see cref="Action{IStar}"/> that validates the items in the list.</param>
        //private void Validate(IList<IStar> stars, int count, Action<IStar> validate)
        //{
        //    Assert.That(stars, Is.Not.Null);

        //    Assert.That(stars, Has.Count.EqualTo(count));

        //    for (int n = 0; n < count; n++)
        //    {
        //        validate(stars[n]);
        //    }
        //}
    }
}
