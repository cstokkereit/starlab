namespace StarLab.Application.Data
{
    public interface IQueryBuilder
    {
        /// <summary>
        /// Adds a field to the select statement.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddField(IField field);

        /// <summary>
        /// Adds the <see cref="IPredicate"/> provided to the where clause.
        /// </summary>
        /// <param name="predicate">The <see cref="IPredicate"/> to add.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddPredicate(IPredicate predicate);

        /// <summary>
        /// Adds the specified predicate to the where clause.
        /// </summary>
        /// <typeparam name="T">The type of the comparison value.</typeparam>
        /// <param name="field">The <see cref="IField"/> containg the values being compared.</param>
        /// <param name="value">The comparison value.</param>
        /// <param name="type">A <see cref="ComparisonOperators"/> that specifies how the value of the field is to be compared to the comparison value.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddPredicate<T>(IField field, T value, ComparisonOperators type);

        /// <summary>
        /// Adds the field provided to the order by clause.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that is to be added to the order by clause.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort order for the field.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddSortField(IField field, SortOrder sortOrder);

        /// <summary>
        /// Adds the specified field to the order by clause.
        /// </summary>
        /// <param name="table">The name of the table containing the field.</param>
        /// <param name="field">The name of the field.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort order for the field.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddSortField(string table, string field, SortOrder sortOrder);

        /// <summary>
        /// Adds the <see cref="ITable"/> provided to the select statement.
        /// </summary>
        /// <param name="table">An <see cref="ITable"/> that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddTable(ITable table);

        /// <summary>
        /// Adds the specified table to the select statement.
        /// </summary>
        /// <param name="table">The name of the table that is to be added to the select statement.</param>
        /// <returns>A reference to this <see cref="IQueryBuilder"/> object to allow fluent modification of the query.</returns>
        IQueryBuilder AddTable(string table);

        /// <summary>
        /// Builds an instance of <see cref="IQuery"/> that specifies the data that will be returned from a database.
        /// </summary>
        /// <returns>An instance of <see cref="IQuery"/> that specifies the data that will be returned from a database.</returns>
        IQuery BuildQuery();

        /// <summary>
        /// Creates an empty instance of the <see cref="IAndPredicate"/> interface.
        /// </summary>
        /// <returns>An instance of the <see cref="IAndPredicate"/> interface containing no child predicates.</returns>
        IAndPredicate CreateAndPredicate();

        /// <summary>
        /// Creates an instance of the <see cref="IAndPredicate"/> interface and initialises it with the predicates contained in the <see cref="IEnumerable{IPredicate}"/> provided.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be combined using the AND operator.</param>
        /// <returns>An instance of the <see cref="IAndPredicate"/> interface containing the child predicates provided.</returns>
        IAndPredicate CreateAndPredicate(IEnumerable<IPredicate> predicates);

        /// <summary>
        /// Creates an instance of <see cref="IField"/> with the specified parent table and name.
        /// </summary>
        /// <param name="table">The name of the table that contains the field.</param>
        /// <param name="name">The name of the field.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface.</returns>
        IField CreateField(string table, string name);

        /// <summary>
        /// Creates an instance of <see cref="IField"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface.</returns>
        IField CreateField(string name);

        /// <summary>
        /// Creates an empty instance of the <see cref="IOrPredicate"/> interface.
        /// </summary>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface containing no child predicates.</returns>
        IOrPredicate CreateOrPredicate();

        /// <summary>
        /// Creates an instance of the <see cref="IOrPredicate"/> interface and initialises it with the predicates contained in the <see cref="IEnumerable{IPredicate}"/> provided.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be combined using the OR operator.</param>
        /// <returns>An instance of the <see cref="IOrPredicate"/> interface containing the child predicates provided.</returns>
        IOrPredicate CreateOrPredicate(IEnumerable<IPredicate> predicates);

        /// <summary>
        /// Creates an <see cref="IPredicate"/> of the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the comparison value.</typeparam>
        /// <param name="field">The <see cref="IField"/> containg the values being compared.</param>
        /// <param name="value">The comparison value.</param>
        /// <param name="type">A <see cref="ComparisonOperators"/> that specifies how the value of the field is to be compared to the comparison value.</param>
        /// <returns>An instance of the required <see cref="IPredicate"/>.</returns>
        IPredicate CreatePredicate<T>(IField field, T value, ComparisonOperators type);
    }
}
