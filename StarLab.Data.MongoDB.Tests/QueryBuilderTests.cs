namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="QueryBuilder"/> class.
    /// </summary>
    public class QueryBuilderTests
    {
        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an empty <see cref="IAndPredicate"/>.
        /// </summary>
        [Test]
        public void TestAddAndPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreateAndPredicate())
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an <see cref="IAndPredicate"/> containing multiple child predicates.
        /// </summary>
        [Test]
        public void TestAddAndPredicateWithMultipleChildPredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            var predicate = builder.CreateAndPredicate()
                                   .AddPredicate(builder.CreatePredicate(field1, -1, ComparisonOperators.GreaterThan))
                                   .AddPredicate(builder.CreatePredicate(field1, 1, ComparisonOperators.LessThan))
                                   .AddPredicate(builder.CreatePredicate(field2, 0, ComparisonOperators.Equals));

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(predicate)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 > -1 AND Field-1 < 1 AND Field-2 = 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddField(IField)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddField()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.Table.Returns("Table-1");
            field.Name.Returns("Field-1");

            // Act
            var query = builder.AddField(field)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a GreaterThan comparison.
        /// </summary>
        [Test]
        public void TestAddGreaterThanPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.GreaterThan)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 > 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a GreaterThanOrEquals comparison.
        /// </summary>
        [Test]
        public void TestAddGreaterThanOrEqualsPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.GreaterThanOrEquals)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 >= 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a LessThan comparison.
        /// </summary>
        [Test]
        public void TestAddLessThanPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.LessThan)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 < 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a LessThanOrEquals comparison.
        /// </summary>
        [Test]
        public void TestAddLessThanOrEqualsPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.LessThanOrEquals)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 <= 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddField(IField)"/> method works correctly when called multiple times with fields from different tables.
        /// </summary>
        [Test]
        public void TestAddMultipleFieldsFromDifferentTables()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Field-1");
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-2.Field-1");
            field2.Table.Returns("Table-2");
            field2.Name.Returns("Field-1");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-3.Field-1");
            field3.Table.Returns("Table-3");
            field3.Name.Returns("Field-1");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Table-1.Field-1, Table-2.Field-1, Table-3.Field-1 FROM Table-1, Table-2, Table-3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddField(IField)"/> method works correctly when called multiple times with fields from the same table.
        /// </summary>
        [Test]
        public void TestAddMultipleFieldsFromSameTable()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(IField, SortOrder)"/> method works correctly when called multiple times with fields from different tables.
        /// </summary>
        [Test]
        public void TestAddMultipleSortFieldsWithMultipleTablesSelected()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Field-1");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-2.Field-1");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-2.Field-2");

            // Act
            var query = builder.AddTable("Table-1")
                               .AddTable("Table-2")
                               .AddSortField(field3, SortOrder.Ascending)
                               .AddSortField(field2, SortOrder.Descending)
                               .AddSortField(field1, SortOrder.Ascending)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Table-1.*, Table-2.* FROM Table-1, Table-2 ORDER BY Table-2.Field-2 ASC, Table-2.Field-1 DESC, Table-1.Field-1 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(IField, SortOrder)"/> method works correctly when called multiple times with fields from the same table.
        /// </summary>
        [Test]
        public void TestAddMultipleSortFieldsWithSingleTableSelected()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var query = builder.AddTable("Table-1")
                               .AddSortField(field1, SortOrder.Ascending)
                               .AddSortField(field3, SortOrder.Descending)
                               .AddSortField(field2, SortOrder.Ascending)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1 ORDER BY Field-1 ASC, Field-3 DESC, Field-2 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when called multiple times with fields from the same table.
        /// </summary>
        [Test]
        public void TestAddMultiplePredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreatePredicate(field1, "Value-1", ComparisonOperators.Equals))
                               .AddPredicate(builder.CreatePredicate(field2, "Value-2", ComparisonOperators.Equals))
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 = 'Value-1' AND Field-2 = 'Value-2'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddTable(ITable)"/> method works correctly when called multiple times.
        /// </summary>
        [Test]
        public void TestAddMultipleTables()
        {
            // Arrange
            var builder = new QueryBuilder();

            var table1 = Substitute.For<ITable>();
            table1.Name.Returns("Table-1");
            table1.SelectAll.Returns(true);

            var table2 = Substitute.For<ITable>();
            table2.Name.Returns("Table-2");
            table2.SelectAll.Returns(true);

            var table3 = Substitute.For<ITable>();
            table3.Name.Returns("Table-3");
            table3.SelectAll.Returns(true);

            // Act
            var query = builder.AddTable(table1)
                               .AddTable(table2)
                               .AddTable(table3)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Table-1.*, Table-2.*, Table-3.* FROM Table-1, Table-2, Table-3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an empty <see cref="IOrPredicate"/>.
        /// </summary>
        [Test]
        public void TestAddOrPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreateOrPredicate())
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an <see cref="IOrPredicate"/> containing multiple child predicates.
        /// </summary>
        [Test]
        public void TestAddOrPredicateWithMultipleChildPredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            var predicate = builder.CreateOrPredicate()
                                   .AddPredicate(builder.CreatePredicate(field1, 1, ComparisonOperators.Equals))
                                   .AddPredicate(builder.CreatePredicate(field1, 2, ComparisonOperators.Equals))
                                   .AddPredicate(builder.CreatePredicate(field1, 3, ComparisonOperators.Equals));

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(predicate)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 = 1 OR Field-1 = 2 OR Field-1 = 3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreatePredicate(field1, "Value-1", ComparisonOperators.Equals))
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 = 'Value-1'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(IField, SortOrder)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddSortField()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.Table.Returns("Table-1");
            field.Name.Returns("Field-1");

            // Act
            var query = builder.AddTable("Table-1")
                               .AddSortField(field, SortOrder.Ascending)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1 ORDER BY Field-1 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(string, string, SortOrder)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddSortFieldByName()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var query = builder.AddTable("Table-1")
                               .AddSortField("Table-1", "Field-1", SortOrder.Ascending)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1 ORDER BY Field-1 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddTable(ITable)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddTable()
        {
            // Arrange
            var builder = new QueryBuilder();

            var table = Substitute.For<ITable>();
            table.Name.Returns("Table-1");
            table.SelectAll.Returns(true);

            // Act
            var query = builder.AddTable(table)
                               .BuildQuery();

            // Assert
            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateAndPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var predicate = builder.CreateAndPredicate();

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate(IEnumerable{IPredicate})"/> method works correctly when a single predicate is provided.
        /// </summary>
        [Test]
        public void TestCreateAndPredicateWithASingleChildPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Height");

            // Act
            var predicate = builder.CreateAndPredicate([builder.CreatePredicate(field, 1.3, ComparisonOperators.Equals)]);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate()"/> method works correctly with fluent addition of predicates.
        /// </summary>
        [Test]
        public void TestCreateAndPredicateWithFluentAdditionOfPredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Height");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-1.Length");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-1.Width");

            // Act
            var predicate = builder.CreateAndPredicate()
                .AddPredicate(builder.CreatePredicate(field1, 1.3, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field2, 2.5, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field3, 4.1, ComparisonOperators.Equals));

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3 AND Table-1.Length = 2.5 AND Table-1.Width = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate(IEnumerable{IPredicate})"/> method works correctly when multiple predicates are provided.
        /// </summary>
        [Test]
        public void TestCreateAndPredicateWithMultipleChildPredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Height");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-1.Length");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-1.Width");

            var predicate1 = builder.CreatePredicate(field1, 1.3, ComparisonOperators.Equals);
            var predicate2 = builder.CreatePredicate(field2, 2.5, ComparisonOperators.Equals);
            var predicate3 = builder.CreatePredicate(field3, 4.1, ComparisonOperators.Equals);

            // Act
            var predicate = builder.CreateAndPredicate([predicate1, predicate2, predicate3]);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3 AND Table-1.Length = 2.5 AND Table-1.Width = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates an equals predicate.
        /// </summary>
        [Test]
        public void TestCreateEqualsPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Name");

            // Act
            var predicate = builder.CreatePredicate(field, "Fred", ComparisonOperators.Equals);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Name = 'Fred'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, double, ComparisonOperators)"/> method correctly creates an equals predicate.
        /// </summary>
        [Test]
        public void TestCreateEqualsPredicateWithNumericArgument()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Length");

            // Act
            var predicate = builder.CreatePredicate(field, 1.6, ComparisonOperators.Equals);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Length = 1.6"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateField(string)"/> method correctly creates an <see cref="IField">.
        /// </summary>
        [Test]
        public void TestCreateField()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var field = builder.CreateField("Field-1");

            // Assert
            Assert.That(field, Is.Not.Null);

            Assert.That(field.FullName, Is.EqualTo(".Field-1"));
            Assert.That(field.Table, Is.EqualTo(string.Empty));
            Assert.That(field.Name, Is.EqualTo("Field-1"));

            Assert.That(field.ToString, Is.EqualTo(".Field-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateField(string, string)"/> method correctly creates an <see cref="IField">.
        /// </summary>
        [Test]
        public void TestCreateFieldWithTable()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var field = builder.CreateField("Table-1", "Field-1");

            // Assert
            Assert.That(field, Is.Not.Null);

            Assert.That(field.FullName, Is.EqualTo("Table-1.Field-1"));
            Assert.That(field.Table, Is.EqualTo("Table-1"));
            Assert.That(field.Name, Is.EqualTo("Field-1"));

            Assert.That(field.ToString, Is.EqualTo("Table-1.Field-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a greater than predicate.
        /// </summary>
        [Test]
        public void TestCreateGreaterThanPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            // Act
            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.GreaterThan);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age > 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a greater than or equals predicate.
        /// </summary>
        [Test]
        public void TestCreateGreaterThanOrEqualsPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            // Act
            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.GreaterThanOrEquals);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age >= 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a less than predicate.
        /// </summary>
        [Test]
        public void TestCreateLessThanPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            // Act
            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.LessThan);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age < 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a less than or equals predicate.
        /// </summary>
        [Test]
        public void TestCreateLessThanOrEqualsPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            // Act
            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.LessThanOrEquals);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age <= 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a not equals predicate.
        /// </summary>
        [Test]
        public void TestCreateNotEqualsPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Name");

            // Act
            var predicate = builder.CreatePredicate(field, "Fred", ComparisonOperators.NotEquals);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Name != 'Fred'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, double, ComparisonOperators)"/> method correctly creates a not equals predicate.
        /// </summary>
        [Test]
        public void TestCreateNotEqualsPredicateWithNumericArgument()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Length");

            // Act
            var predicate = builder.CreatePredicate(field, 1.6, ComparisonOperators.NotEquals);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Length != 1.6"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateOrPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var predicate = builder.CreateOrPredicate();

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate(IEnumerable{IPredicate})"/> method works correctly when a single predicate is provided.
        /// </summary>
        [Test]
        public void TestCreateOrPredicateWithASingleChildPredicate()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Height");

            // Act
            var predicate = builder.CreateOrPredicate([builder.CreatePredicate(field, 1.3, ComparisonOperators.Equals)]);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate()"/> method works correctly with fluent addition of predicates.
        /// </summary>
        [Test]
        public void TestCreateOrPredicateWithFluentAdditionOfPredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Height");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-1.Length");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-1.Width");

            // Act
            var predicate = builder.CreateOrPredicate()
                .AddPredicate(builder.CreatePredicate(field1, 1.3, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field2, 2.5, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field3, 4.1, ComparisonOperators.Equals));

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3 OR Table-1.Length = 2.5 OR Table-1.Width = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate(IEnumerable{IPredicate})"/> method works correctly when multiple predicates are provided.
        /// </summary>
        [Test]
        public void TestCreateOrPredicateWithMultipleChildPredicates()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Length");

            var predicate1 = builder.CreatePredicate(field, 1.3, ComparisonOperators.Equals);
            var predicate2 = builder.CreatePredicate(field, 2.5, ComparisonOperators.Equals);
            var predicate3 = builder.CreatePredicate(field, 4.1, ComparisonOperators.Equals);

            // Act
            var predicate = builder.CreateOrPredicate([predicate1, predicate2, predicate3]);

            // Assert
            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Length = 1.3 OR Table-1.Length = 2.5 OR Table-1.Length = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string)"/> method correctly creates an <see cref="ITable">.
        /// </summary>
        [Test]
        public void TestCreateTable()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var table = builder.CreateTable("Table-1");

            // Assert
            Assert.That(table, Is.Not.Null);
            Assert.That(table.Name, Is.EqualTo("Table-1"));
            Assert.That(table.SelectAll, Is.True);

            Assert.That(table.Fields, Is.Not.Null);
            Assert.That(table.Fields.Count, Is.EqualTo(0));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string)"/> method works correctly with fluent addition of fields.
        /// </summary>
        [Test]
        public void TestCreateTableWithFluentAdditionOfFields()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Act
            var table = builder.CreateTable("Table-1")
                .AddField(builder.CreateField("Field-1"))
                .AddField(builder.CreateField("Field-2"))
                .AddField(builder.CreateField("Field-3"));

            // Assert
            Assert.That(table, Is.Not.Null);
            Assert.That(table.Name, Is.EqualTo("Table-1"));
            Assert.That(table.SelectAll, Is.False);

            Assert.That(table.Fields, Is.Not.Null);
            Assert.That(table.Fields.Count, Is.EqualTo(3));

            var fields = new List<IField>(table.Fields);

            Assert.That(fields[0].Name, Is.EqualTo("Field-1"));
            Assert.That(fields[1].Name, Is.EqualTo("Field-2"));
            Assert.That(fields[2].Name, Is.EqualTo("Field-3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string)"/> method throws an <see cref="ArgumentException"/> when a duplicate field is added.
        /// </summary>
        [Test]
        public void TestCreateTableWithDuplicateFieldsThrowsException()
        {
            // Arrange
            var builder = new QueryBuilder();

            // Arrange
            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-1");

            // Act
            var e = Assert.Throws<ArgumentException>(() => builder.CreateTable("Table-1", [field1, field2]));

            // Assert.That(e.Message, Is.EqualTo()); TODO
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string, IEnumerable{IField})"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateTableWithFields()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-1");
            field3.Name.Returns("Field-3");

            // Act
            var table = builder.CreateTable("Table-1", [field1, field2, field3]);

            // Assert
            Assert.That(table, Is.Not.Null);
            Assert.That(table.Name, Is.EqualTo("Table-1"));
            Assert.That(table.SelectAll, Is.False);

            Assert.That(table.Fields, Is.Not.Null);
            Assert.That(table.Fields.Count, Is.EqualTo(3));

            foreach (var field in table.Fields)
            {
                Assert.That(field.Table, Is.EqualTo("Table-1"));
            }
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string, IEnumerable{IField})"/> method works correctly when provided with fields from another table.
        /// </summary>
        [Test]
        public void TestCreateTableWithFieldsFromAnotherTable()
        {
            // Arrange
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-2");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-2");
            field2.Name.Returns("Field-2");

            var field3 = Substitute.For<IField>();
            field3.Table.Returns("Table-2");
            field3.Name.Returns("Field-3");

            // Act
            var table = builder.CreateTable("Table-1", [field1, field2, field3]);

            // Assert
            Assert.That(table, Is.Not.Null);
            Assert.That(table.Name, Is.EqualTo("Table-1"));
            Assert.That(table.SelectAll, Is.False);

            Assert.That(table.Fields, Is.Not.Null);
            Assert.That(table.Fields.Count, Is.EqualTo(3));

            foreach (var field in table.Fields)
            {
                Assert.That(field.Table, Is.EqualTo("Table-1"));
            }
        }
    }
}
