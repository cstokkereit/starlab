namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that compares two expressions in order to determine which values will be returned by the query.
    /// </summary>
    /// <typeparam name="T">The type of the comparison value.</typeparam>
    public abstract class BinaryPredicate<T> : BinaryFragment, IPredicate
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="BinaryPredicate{T}"/> class.
        /// </summary>
        /// <param name="field">The field that contains the value being compared.</param>
        /// <param name="value">The comparison value.</param>
        public BinaryPredicate(IField field, T value)
             : base(field, new ValueFragment<T>(value)) { }

        /// <summary>
        /// Converts the value of the current <see cref="BinaryPredicate{T}"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="BinaryPredicate{T}"/> object.</returns>
        public abstract string ToString(bool useFullNames);

        /// <summary>
        /// Converts the value of the current <see cref="BinaryPredicate{T}"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="BinaryPredicate{T}"/> object.</returns>
        public override string ToString()
        {
            return ToString(true);
        }
    }
}
