namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that specifies the criteria that determine which values will be returned by the query.
    /// </summary>
    public abstract class WhereFragment : IQueryFragment, IWhere
    {
        /// <summary>
        /// Adds an <see cref="IPredicate"/> that will be used to filter the data.
        /// </summary>
        /// <param name="predicate">An <see cref="IPredicate"/> that will be used to filter the data.</param>
        /// <returns>A reference to this <see cref="IWhere"/> object to allow fluent addition of predicates.</returns>
        public abstract IWhere AddPredicate(IPredicate predicate);

        /// <summary>
        /// Converts the value of the current <see cref="WhereFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="WhereFragment"/> object.</returns>
        public abstract string ToString(bool useFullNames);
    }
}
