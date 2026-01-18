namespace StarLab.Data
{
    /// <summary>
    /// A fluent builder for constructing instances of objects that implement the <see cref="IQuery"/> interface.
    /// </summary>
    public abstract class QueryBuilderBase
    {
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
        public QueryBuilderBase AddField(IField field)
        {
            query.SelectStatement.AddField(field.Table, field);

            return this;
        }

        /// <summary>
        /// Adds the <see cref="IPredicate"/> provided to the where clause.
        /// </summary>
        /// <param name="predicate">The <see cref="IPredicate"/> to add.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public QueryBuilderBase AddPredicate(IPredicate predicate)
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
        public QueryBuilderBase AddPredicate<T>(IField field, T value, ComparisonOperators type)
        {
            return AddPredicate(CreatePredicate(field, value, type));
        }

        /// <summary>
        /// Adds the field provided to the order by clause.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that is to be added to the order by clause.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort order for the field.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public QueryBuilderBase AddSortField(IField field, SortOrder sortOrder)
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
        public QueryBuilderBase AddSortField(string table, string field, SortOrder sortOrder)
        {
            return AddSortField(new FieldFragment(table, field), sortOrder);
        }

        /// <summary>
        /// Adds the <see cref="ITable"/> provided to the select statement.
        /// </summary>
        /// <param name="table">An <see cref="ITable"/> that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public QueryBuilderBase AddTable(ITable table)
        {
            query.SelectStatement.AddTable(table);

            return this;
        }

        /// <summary>
        /// Adds the specified table to the select statement.
        /// </summary>
        /// <param name="table">The name of the table that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="QueryBuilderBase"/> object to allow fluent modification of the query.</returns>
        public QueryBuilderBase AddTable(string table)
        {
            return AddTable(new TableFragment(table));
        }

        /// <summary>
        /// Builds an instance of <see cref="IQuery"/> that specifies the data that will be returned from a database.
        /// </summary>
        /// <returns>An instance of <see cref="IQuery"/> that specifies the data that will be returned from a database.</returns>
        public IQuery BuildQuery()
        {
            return query;
        }

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
        /// A function for creating an instance of <see cref="IQuery"/> that will be implemented in derived classes.
        /// </summary>
        /// <returns>An instance of <see cref="IQuery"/> that contains no fields, filter criteria or sort ordering.</returns>
        protected abstract IQuery CreateQuery();
    }
}
