namespace StarLab.Data
{
    /// <summary>
    /// Representa a predicate that combines multiple predicates using the AND operator.
    /// </summary>
    public interface IAndPredicate : IPredicate
    {
        /// <summary>
        /// Adds an <see cref="IPredicate"/> to the predicates that are being combined using the AND operator.
        /// </summary>
        /// <param name="predicate">The <see cref="IPredicate"/> being added.</param>
        /// <returns>A reference to this <see cref="IAndPredicate"/> object to allow fluent addition of predicates.</returns>
        IAndPredicate AddPredicate(IPredicate predicate);
    }
}
