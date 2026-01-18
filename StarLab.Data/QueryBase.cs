namespace StarLab.Data
{
    /// <summary>
    /// Abstract base implementation of the <see cref="IQuery"/> interface.
    /// </summary>
    public abstract class QueryBase : IQuery
    {
        /// <summary>
        /// Iniitialises a new instance of the <see cref="QueryBase"/> class.
        /// </summary>
        protected QueryBase()
        {
            OrderByClause = CreateOrderByClause();
            SelectStatement = CreateSelectClause();
            WhereClause = CreateWhereClause();
            FromClause = CreateFromClause();

            SelectStatement.TableAdded += OnTableAdded;
        }

        /// <summary>
        /// Gets the <see cref="IFrom"/> that specifies the table(s) containing the data to be retrieved.
        /// </summary>
        public IFrom FromClause { get; }

        /// <summary>
        /// Gets the <see cref="IOrderBy"/> that specifies the sort order for the retrieved records.
        /// </summary>
        public IOrderBy OrderByClause { get; }

        /// <summary>
        /// Gets the <see cref="ISelect"/> that specifies which fields to retrieve.
        /// </summary>
        public ISelect SelectStatement { get; }

        /// <summary>
        /// Gets the <see cref="IWhere"/> that specifies which documents to retrieve.
        /// </summary>
        public IWhere WhereClause { get; }

        /// <summary>
        /// Converts the value of the current <see cref="QueryBase"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="QueryBase"/> object.</returns>
        public override string ToString()
        {
            var useFullNames = FromClause.Size > 1;

            var whereClause = ((WhereFragment)WhereClause).ToString(useFullNames);

            var orderByClause = ((OrderByFragment)OrderByClause).ToString(useFullNames);

            return $"{SelectStatement} {FromClause}{whereClause}{orderByClause}";
        }

        /// <summary>
        /// Creates the <see cref="IFrom"/> that specifies the table(s) containing the data to be retrieved.
        /// </summary>
        /// <returns></returns>
        protected abstract IFrom CreateFromClause();

        /// <summary>
        /// Creates the <see cref="IOrderBy"/> that specifies the sort order for the retrieved records.
        /// </summary>
        protected abstract IOrderBy CreateOrderByClause();

        /// <summary>
        /// Creates the <see cref="ISelect"/> that specifies which fields to retrieve.
        /// </summary>
        protected abstract ISelect CreateSelectClause();

        /// <summary>
        /// Creates the <see cref="IWhere"/> that specifies which documents to retrieve.
        /// </summary>
        protected abstract IWhere CreateWhereClause();

        /// <summary>
        /// Event handler for the <see cref="SelectFragment.TableAdded"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="table">The name of the table that was added.</param>
        protected virtual void OnTableAdded(object? sender, string table)
        {
            FromClause.AddTable(table);
        }
    }
}
