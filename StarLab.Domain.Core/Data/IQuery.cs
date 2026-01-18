namespace StarLab.Data
{
    /// <summary>
    /// Represents the filter criteria, sort order and fields to be returned from a database.
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// Gets the <see cref="IFrom"/> that specifies the table(s) containing the data to be retrieved.
        /// </summary>
        IFrom FromClause { get; }

        /// <summary>
        /// Gets the <see cref="IOrderBy"/> that specifies the sort order for the retrieved records.
        /// </summary>
        IOrderBy OrderByClause { get; }

        /// <summary>
        /// Gets the <see cref="ISelect"/> that specifies which fields to retrieve.
        /// </summary>
        ISelect SelectStatement { get; }

        /// <summary>
        /// Gets the <see cref="IWhere"/> that specifies which documents to retrieve.
        /// </summary>
        IWhere WhereClause { get; }
    }
}
