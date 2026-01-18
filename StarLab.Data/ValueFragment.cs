namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that represents a value used in a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class ValueFragment<T> : IQueryFragment, IValue<T>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ValueFragment{T}"/> class.
        /// </summary>
        /// <param name="value"></param>
        public ValueFragment(T value)
        {
            Value = value;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        public T Value { get; }
    }
}
