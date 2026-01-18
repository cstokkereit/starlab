namespace StarLab.Data
{
    /// <summary>
    /// Represents part of a database query that specifies the criteria that will be used to filter the data.
    /// </summary>
    public interface IWhere : IQueryFragment
    {
        /// <summary>
        /// Adds an <see cref="IPredicate"/> that will be used to filter the data.
        /// </summary>
        /// <param name="predicate">An <see cref="IPredicate"/> that will be used to filter the data.</param>
        /// <returns>A reference to this <see cref="IWhere"/> object to allow fluent addition of predicates.</returns>
        IWhere AddPredicate(IPredicate predicate);
    }
}
