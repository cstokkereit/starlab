namespace StarLab.Data
{
    /// <summary>
    /// Represents a value used in a predicate.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IValue<T>
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        T Value { get; }
    }
}
