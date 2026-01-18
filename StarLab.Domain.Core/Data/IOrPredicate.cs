namespace StarLab.Data
{
    /// <summary>
    /// Represents a predicate that combines multiple predicates using the OR operator.
    /// </summary>
    public interface IOrPredicate : IPredicate
    {
        /// <summary>
        /// Adds an <see cref="IPredicate"/> to the predicates that are being combined using the OR operator.
        /// </summary>
        /// <param name="predicate">The <see cref="IPredicate"/> being added.</param>
        /// <returns>A reference to this <see cref="IOrPredicate"/> object to allow fluent addition of predicates.</returns>
        IOrPredicate AddPredicate(IPredicate predicate);
    }
}
