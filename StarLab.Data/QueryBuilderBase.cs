using StarLab.Application.Data;
using StarLab.Shared;
using System.Xml.Linq;

namespace StarLab.Data
{
    /// <summary>
    /// A fluent builder for constructing instances of objects that implement the <see cref="IQuery"/> interface.
    /// </summary>
    public abstract class QueryBuilderBase : IQueryBuilder
    {
        private readonly Dictionary<string, TableFragment> tablesByName = new Dictionary<string, TableFragment>(); // A dictionary that contains the tables that have been created indexed by name.

        private readonly List<string> tables = new List<string>(); // A list that contains the names of the tables that have been added to the query.

        private IQuery query; // The query being constructed.

        /// <summary>
        /// Initialises a new instance of the <see cref="QueryBuilderBase"/> class.
        /// </summary>
        public QueryBuilderBase()
        {
            query = CreateQuery();
        }

        /// <summary>
        /// Adds a field to the select statement.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddField(IField field)
        {
            if (!tablesByName.ContainsKey(field.Table))
            {
                var table = (TableFragment)CreateTable(field.Table);

                table.AddField(field);

                AddTable(table);
            }

            if (!tablesByName[field.Table].HasField(field.Name))
            {
                tablesByName[field.Table].AddField(field);
            }

            //query.SelectStatement.AddField(field.Table, field);

            if (!tables.Contains(field.Table))
            {
                tables.Add(field.Table);
            }

            return this;
        }

        /// <summary>
        /// Adds the <see cref="IPredicate"/> provided to the where clause.
        /// </summary>
        /// <param name="predicate">The <see cref="IPredicate"/> to add.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddPredicate(IPredicate predicate)
        {
            query.WhereClause.AddPredicate(predicate);
            
            return this;
        }

        /// <summary>
        /// Adds the specified predicate to the where clause.
        /// </summary>
        /// <typeparam name="T">The type of the comparison value.</typeparam>
        /// <param name="field">The <see cref="IField"/> containg the values being compared.</param>
        /// <param name="value">The comparison value.</param>
        /// <param name="type">A <see cref="ComparisonOperators"/> that specifies how the value of the field is to be compared to the comparison value.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddPredicate<T>(IField field, T value, ComparisonOperators type)
        {
            return AddPredicate(CreatePredicate(field, value, type));
        }

        /// <summary>
        /// Adds the field provided to the order by clause.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that is to be added to the order by clause.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort order for the field.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddSortField(IField field, SortOrder sortOrder)
        {
            query.OrderByClause.AddSortField(field, sortOrder);

            return this;
        }

        /// <summary>
        /// Adds the specified field to the order by clause.
        /// </summary>
        /// <param name="table">The name of the table containing the field.</param>
        /// <param name="field">The name of the field.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort order for the field.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddSortField(string table, string field, SortOrder sortOrder)
        {
            return AddSortField(new FieldFragment(table, field), sortOrder);
        }

        /// <summary>
        /// Adds the <see cref="ITable"/> provided to the select statement.
        /// </summary>
        /// <param name="table">An <see cref="ITable"/> that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddTable(ITable table)
        {
            query.SelectStatement.AddTable(table);

            if (!tables.Contains(table.Name)) tables.Add(table.Name);

            return this;
        }

        /// <summary>
        /// Adds the specified table to the select statement.
        /// </summary>
        /// <param name="name">The name of the table that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public IQueryBuilder AddTable(string name)
        {
            if (!tablesByName.ContainsKey(name)) CreateTable(name);

            return AddTable(tablesByName[name]);
        }

        /// <summary>
        /// Builds an instance of <see cref="IQuery"/> that specifies the data that will be returned from a database.
        /// </summary>
        /// <returns>An instance of <see cref="IQuery"/> that specifies the data that will be returned from a database.</returns>
        public IQuery BuildQuery()
        {
            tablesByName.Clear();
            tables.Clear();

            var temp = query;

            query = CreateQuery();

            return temp;
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="IAndPredicate"/> interface.
        /// </summary>
        /// <returns>An instance of the <see cref="IAndPredicate"/> interface containing no child predicates.</returns>
        public abstract IAndPredicate CreateAndPredicate();

        /// <summary>
        /// Creates an instance of the <see cref="IAndPredicate"/> interface and initialises it with the predicates contained in the <see cref="IEnumerable{IPredicate}"/> provided.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be combined using the AND operator.</param>
        /// <returns>An instance of the <see cref="IAndPredicate"/> interface containing the child predicates provided.</returns>
        public abstract IAndPredicate CreateAndPredicate(IEnumerable<IPredicate> predicates);

        /// <summary>
        /// Creates an <see cref="IField"/> with the specified parent table and name.
        /// </summary>
        /// <param name="table">The name of the table that contains the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <returns>An instance of the <see cref="IField"/> interface.</returns>
        public virtual IField CreateField(string table, string name)
        {
            var field = new FieldFragment(table, name);

            if (!tablesByName.ContainsKey(table)) CreateTable(table);

            var fragment = tablesByName[table];

            fragment.AddField(field);

            return field;
        }

        /// <summary>
        /// Creates an instance of <see cref="IField"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface.</returns>
        public virtual IField CreateField(string name)
        {
            if (Tables.Count > 1) throw new InvalidOperationException(ExceptionMessages.CannotCreateField(name, Tables.Count));
             
            var table = Tables.Count == 1 ? Tables[0] : GetTableName();

            if (string.IsNullOrEmpty(table)) throw new InvalidOperationException(ExceptionMessages.CannotCreateField(name, tablesByName.Count));

            return new FieldFragment(table, name);
        }

        /// <summary>
        /// Creates an empty instance of the <see cref="IOrPredicate"/> interface.
        /// </summary>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface containing no child predicates.</returns>
        public abstract IOrPredicate CreateOrPredicate();

        /// <summary>
        /// Creates an instance of the <see cref="IOrPredicate"/> interface and initialises it with the predicates contained in the <see cref="IEnumerable{IPredicate}"/> provided.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be combined using the OR operator.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface containing the child predicates provided.</returns>
        public abstract IOrPredicate CreateOrPredicate(IEnumerable<IPredicate> predicates);

        /// <summary>
        /// Creates an <see cref="IPredicate"/> of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the comparison value.</typeparam>
        /// <param name="field">The <see cref="IField"/> containg the values being compared.</param>
        /// <param name="value">The comparison value.</param>
        /// <param name="type">A <see cref="ComparisonOperators"/> that specifies how the value of the field is to be compared to the comparison value.</param>
        /// <returns>An instance of the required <see cref="IPredicate"/>.</returns>
        public abstract IPredicate CreatePredicate<T>(IField field, T value, ComparisonOperators type);

        /// <summary>
        /// Creates an <see cref="ITable"/> with the specified name and fields.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <param name="fields">An <see cref="IEnumerable{string}"/> containing the names of the fields.</param>
        /// <returns>An instance of the <see cref="ITable"/> interface.</returns>
        public virtual ITable CreateTable(string name, IEnumerable<string> fields)
        {
            if (tablesByName.ContainsKey(name)) throw new InvalidOperationException(ExceptionMessages.TableAlreadyCreated(name));

            var table = new TableFragment(name);

            tablesByName.Add(table.Name, table);

            foreach (var field in fields)
            {
                table.AddField(field);
            }

            return table;
        }

        /// <summary>
        /// Creates an <see cref="ITable"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <returns>An instance of the <see cref="ITable"/> interface.</returns>
        public virtual ITable CreateTable(string name)
        {
            if (tablesByName.ContainsKey(name)) throw new InvalidOperationException(ExceptionMessages.TableAlreadyCreated(name));

            var table = new TableFragment(name);

            tablesByName.Add(table.Name, table);

            return table;
        }

        /// <summary>
        /// Gets the names of the tables.
        /// </summary>
        protected List<string> Tables => tables;

        /// <summary>
        /// A function for creating an instance of <see cref="IQuery"/> that will be implemented in derived classes.
        /// </summary>
        /// <returns>An instance of <see cref="IQuery"/> that contains no fields, filter criteria or sort ordering.</returns>
        protected abstract IQuery CreateQuery();

        /// <summary>
        /// Gets the name of the table currently being constructed assuming that there is only one such table.
        /// </summary>
        /// <returns>The name of the table if only one table is being constructed; an empty string otherwise.</returns>
        private string GetTableName()
        {
            return tablesByName.Count == 1 ? tablesByName.Keys.First() : string.Empty;
        }
    }
}
