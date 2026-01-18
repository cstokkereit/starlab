namespace StarLab.Data
{
    /// <summary>
    /// Represents part of a database query that compares one or more expressions in order to determine which values will be returned by the query.
    /// </summary>
    public interface IPredicate : IQueryFragment
    {
        /// <summary>
        /// Converts the value of the current <see cref="IPredicate"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="IPredicate"/> object.</returns>
        string ToString(bool useFullNames);
    }
}
