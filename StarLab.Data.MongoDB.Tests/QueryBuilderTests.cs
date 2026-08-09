using StarLab.Application.Data;

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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreateAndPredicate())
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an <see cref="IAndPredicate"/> containing multiple child predicates.
        /// </summary>
        [Test]
        public void TestAddAndPredicateWithMultipleChildPredicates()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(predicate)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 > -1 AND Field-1 < 1 AND Field-2 = 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddField(IField)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddField()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.Table.Returns("Table-1");
            field.Name.Returns("Field-1");

            var query = builder.AddField(field)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a GreaterThan comparison.
        /// </summary>
        [Test]
        public void TestAddGreaterThanPredicate()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.GreaterThan)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 > 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a GreaterThanOrEquals comparison.
        /// </summary>
        [Test]
        public void TestAddGreaterThanOrEqualsPredicate()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.GreaterThanOrEquals)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 >= 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a LessThan comparison.
        /// </summary>
        [Test]
        public void TestAddLessThanPredicate()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.LessThan)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 < 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IField, int, ComparisonOperators)"/> method works correctly for a LessThanOrEquals comparison.
        /// </summary>
        [Test]
        public void TestAddLessThanOrEqualsPredicate()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-2");

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddPredicate(field1, 0, ComparisonOperators.LessThanOrEquals)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2 FROM Table-1 WHERE Field-1 <= 0"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddField(IField)"/> method works correctly when called multiple times with fields from different tables.
        /// </summary>
        [Test]
        public void TestAddMultipleFieldsFromDifferentTables()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Table-1.Field-1, Table-2.Field-1, Table-3.Field-1 FROM Table-1, Table-2, Table-3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddField(IField)"/> method works correctly when called multiple times with fields from the same table.
        /// </summary>
        [Test]
        public void TestAddMultipleFieldsFromSameTable()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(IField, SortOrder)"/> method works correctly when called multiple times with fields from different tables.
        /// </summary>
        [Test]
        public void TestAddMultipleSortFieldsWithMultipleTablesSelected()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Field-1");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-2.Field-1");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-2.Field-2");

            var query = builder.AddTable("Table-1")
                               .AddTable("Table-2")
                               .AddSortField(field3, SortOrder.Ascending)
                               .AddSortField(field2, SortOrder.Descending)
                               .AddSortField(field1, SortOrder.Ascending)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Table-1.*, Table-2.* FROM Table-1, Table-2 ORDER BY Table-2.Field-2 ASC, Table-2.Field-1 DESC, Table-1.Field-1 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(IField, SortOrder)"/> method works correctly when called multiple times with fields from the same table.
        /// </summary>
        [Test]
        public void TestAddMultipleSortFieldsWithSingleTableSelected()
        {
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

            var query = builder.AddTable("Table-1")
                               .AddSortField(field1, SortOrder.Ascending)
                               .AddSortField(field3, SortOrder.Descending)
                               .AddSortField(field2, SortOrder.Ascending)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1 ORDER BY Field-1 ASC, Field-3 DESC, Field-2 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when called multiple times with fields from the same table.
        /// </summary>
        [Test]
        public void TestAddMultiplePredicates()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreatePredicate(field1, "Value-1", ComparisonOperators.Equals))
                               .AddPredicate(builder.CreatePredicate(field2, "Value-2", ComparisonOperators.Equals))
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 = 'Value-1' AND Field-2 = 'Value-2'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddTable(ITable)"/> method works correctly when called multiple times.
        /// </summary>
        [Test]
        public void TestAddMultipleTables()
        {
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

            var query = builder.AddTable(table1)
                               .AddTable(table2)
                               .AddTable(table3)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Table-1.*, Table-2.*, Table-3.* FROM Table-1, Table-2, Table-3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an empty <see cref="IOrPredicate"/>.
        /// </summary>
        [Test]
        public void TestAddOrPredicate()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreateOrPredicate())
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly when provided with an <see cref="IOrPredicate"/> containing multiple child predicates.
        /// </summary>
        [Test]
        public void TestAddOrPredicateWithMultipleChildPredicates()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(predicate)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 = 1 OR Field-1 = 2 OR Field-1 = 3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddPredicate(IPredicate)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddPredicate()
        {
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

            var query = builder.AddField(field1)
                               .AddField(field2)
                               .AddField(field3)
                               .AddPredicate(builder.CreatePredicate(field1, "Value-1", ComparisonOperators.Equals))
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT Field-1, Field-2, Field-3 FROM Table-1 WHERE Field-1 = 'Value-1'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(IField, SortOrder)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddSortField()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.Table.Returns("Table-1");
            field.Name.Returns("Field-1");

            var query = builder.AddTable("Table-1")
                               .AddSortField(field, SortOrder.Ascending)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1 ORDER BY Field-1 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddSortField(string, string, SortOrder)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddSortFieldByName()
        {
            var builder = new QueryBuilder();

            var query = builder.AddTable("Table-1")
                               .AddSortField("Table-1", "Field-1", SortOrder.Ascending)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1 ORDER BY Field-1 ASC"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilderBase.AddTable(ITable)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddTable()
        {
            var builder = new QueryBuilder();

            var table = Substitute.For<ITable>();
            table.Name.Returns("Table-1");
            table.SelectAll.Returns(true);

            var query = builder.AddTable(table)
                               .BuildQuery();

            Assert.That(query, Is.Not.Null);
            Assert.That(query.ToString(), Is.EqualTo("SELECT * FROM Table-1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateAndPredicate()
        {
            var builder = new QueryBuilder();

            var predicate = builder.CreateAndPredicate();

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate(IEnumerable{IPredicate})"/> method works correctly when a single predicate is provided.
        /// </summary>
        [Test]
        public void TestCreateAndPredicateWithASingleChildPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Height");

            var predicate = builder.CreateAndPredicate([builder.CreatePredicate(field, 1.3, ComparisonOperators.Equals)]);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate()"/> method works correctly with fluent addition of predicates.
        /// </summary>
        [Test]
        public void TestCreateAndPredicateWithFluentAdditionOfPredicates()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Height");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-1.Length");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-1.Width");

            var predicate = builder.CreateAndPredicate()
                .AddPredicate(builder.CreatePredicate(field1, 1.3, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field2, 2.5, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field3, 4.1, ComparisonOperators.Equals));

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3 AND Table-1.Length = 2.5 AND Table-1.Width = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateAndPredicate(IEnumerable{IPredicate})"/> method works correctly when multiple predicates are provided.
        /// </summary>
        [Test]
        public void TestCreateAndPredicateWithMultipleChildPredicates()
        {
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

            var predicate = builder.CreateAndPredicate([predicate1, predicate2, predicate3]);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3 AND Table-1.Length = 2.5 AND Table-1.Width = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates an equals predicate.
        /// </summary>
        [Test]
        public void TestCreateEqualsPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Name");

            var predicate = builder.CreatePredicate(field, "Fred", ComparisonOperators.Equals);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Name = 'Fred'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, double, ComparisonOperators)"/> method correctly creates an equals predicate.
        /// </summary>
        [Test]
        public void TestCreateEqualsPredicateWithNumericArgument()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Length");

            var predicate = builder.CreatePredicate(field, 1.6, ComparisonOperators.Equals);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Length = 1.6"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateField(string)"/> method correctly creates an <see cref="IField">.
        /// </summary>
        [Test]
        public void TestCreateField()
        {
            var builder = new QueryBuilder();

            var field = builder.CreateField("Field-1");

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
            var builder = new QueryBuilder();

            var field = builder.CreateField("Table-1", "Field-1");

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
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.GreaterThan);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age > 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a greater than or equals predicate.
        /// </summary>
        [Test]
        public void TestCreateGreaterThanOrEqualsPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.GreaterThanOrEquals);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age >= 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a less than predicate.
        /// </summary>
        [Test]
        public void TestCreateLessThanPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.LessThan);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age < 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a less than or equals predicate.
        /// </summary>
        [Test]
        public void TestCreateLessThanOrEqualsPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Age");

            var predicate = builder.CreatePredicate(field, 18, ComparisonOperators.LessThanOrEquals);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Age <= 18"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, string, ComparisonOperators)"/> method correctly creates a not equals predicate.
        /// </summary>
        [Test]
        public void TestCreateNotEqualsPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Name");

            var predicate = builder.CreatePredicate(field, "Fred", ComparisonOperators.NotEquals);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Name != 'Fred'"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreatePredicate(string, double, ComparisonOperators)"/> method correctly creates a not equals predicate.
        /// </summary>
        [Test]
        public void TestCreateNotEqualsPredicateWithNumericArgument()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Length");

            var predicate = builder.CreatePredicate(field, 1.6, ComparisonOperators.NotEquals);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Length != 1.6"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateOrPredicate()
        {
            var builder = new QueryBuilder();

            var predicate = builder.CreateOrPredicate();

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate(IEnumerable{IPredicate})"/> method works correctly when a single predicate is provided.
        /// </summary>
        [Test]
        public void TestCreateOrPredicateWithASingleChildPredicate()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Height");

            var predicate = builder.CreateOrPredicate([builder.CreatePredicate(field, 1.3, ComparisonOperators.Equals)]);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate()"/> method works correctly with fluent addition of predicates.
        /// </summary>
        [Test]
        public void TestCreateOrPredicateWithFluentAdditionOfPredicates()
        {
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.FullName.Returns("Table-1.Height");

            var field2 = Substitute.For<IField>();
            field2.FullName.Returns("Table-1.Length");

            var field3 = Substitute.For<IField>();
            field3.FullName.Returns("Table-1.Width");

            var predicate = builder.CreateOrPredicate()
                .AddPredicate(builder.CreatePredicate(field1, 1.3, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field2, 2.5, ComparisonOperators.Equals))
                .AddPredicate(builder.CreatePredicate(field3, 4.1, ComparisonOperators.Equals));

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Height = 1.3 OR Table-1.Length = 2.5 OR Table-1.Width = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateOrPredicate(IEnumerable{IPredicate})"/> method works correctly when multiple predicates are provided.
        /// </summary>
        [Test]
        public void TestCreateOrPredicateWithMultipleChildPredicates()
        {
            var builder = new QueryBuilder();

            var field = Substitute.For<IField>();
            field.FullName.Returns("Table-1.Length");

            var predicate1 = builder.CreatePredicate(field, 1.3, ComparisonOperators.Equals);
            var predicate2 = builder.CreatePredicate(field, 2.5, ComparisonOperators.Equals);
            var predicate3 = builder.CreatePredicate(field, 4.1, ComparisonOperators.Equals);

            var predicate = builder.CreateOrPredicate([predicate1, predicate2, predicate3]);

            Assert.That(predicate, Is.Not.Null);
            Assert.That(predicate.ToString(), Is.EqualTo("Table-1.Length = 1.3 OR Table-1.Length = 2.5 OR Table-1.Length = 4.1"));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string)"/> method correctly creates an <see cref="ITable">.
        /// </summary>
        [Test]
        public void TestCreateTable()
        {
            var builder = new QueryBuilder();

            var table = builder.CreateTable("Table-1");

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
            var builder = new QueryBuilder();

            var table = builder.CreateTable("Table-1")
                .AddField(builder.CreateField("Field-1"))
                .AddField(builder.CreateField("Field-2"))
                .AddField(builder.CreateField("Field-3"));

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
            var builder = new QueryBuilder();

            var field1 = Substitute.For<IField>();
            field1.Table.Returns("Table-1");
            field1.Name.Returns("Field-1");

            var field2 = Substitute.For<IField>();
            field2.Table.Returns("Table-1");
            field2.Name.Returns("Field-1");

            Assert.Throws<ArgumentException>(() => builder.CreateTable("Table-1", [field1, field2]));
        }

        /// <summary>
        /// Test that the <see cref="QueryBuilder.CreateTable(string, IEnumerable{IField})"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateTableWithFields()
        {
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

            var table = builder.CreateTable("Table-1", [field1, field2, field3]);

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

            var table = builder.CreateTable("Table-1", [field1, field2, field3]);

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
