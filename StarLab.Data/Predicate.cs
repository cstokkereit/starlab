namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that compares one or more expressions in order to determine which values will be returned by the query.
    /// </summary>
    public abstract class Predicate : IPredicate
    {
        /// <summary>
        /// Converts the value of the current <see cref="Predicate"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="Predicate"/> object.</returns>
        public abstract string ToString(bool useFullNames);

        /// <summary>
        /// Converts the value of the current <see cref="Predicate"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Predicate"/> object.</returns>
        public override string ToString()
        {
            return ToString(true);
        }
    }
}
