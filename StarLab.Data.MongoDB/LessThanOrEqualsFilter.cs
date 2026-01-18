using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IPredicate"/> interface that only retrieves those documents for which the contents of the specified field are less than or equal to the comparison value.
    /// </summary>
    /// <typeparam name="T">The type of the comparison value.</typeparam>
    internal class LessThanOrEqualsFilter<T> : BinaryPredicate<T>, IFilterAdapter
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LessThanOrEqualsFilter{T}"/> class.
        /// </summary>
        /// <param name="field"></param>
        /// <param name="value">The comparison value.</param>
        public LessThanOrEqualsFilter(IField field, T value)
            : base(field, value) { }

        /// <summary>
        /// Gets the <see cref="FilterDefinition{BsonDocument}"/> specified by this predicate.
        /// </summary>
        /// <returns>A <see cref="FilterDefinition{BsonDocument}"/> that specifies which documents to retrieve.</returns>
        public FilterDefinition<BsonDocument> GetFilter()
        {
            if (LHS is IField lhs && RHS is IValue<T> rhs)
            {
                return Builders<BsonDocument>.Filter.Lte(lhs.Name, rhs.Value);
            }

            throw new NotSupportedException();
        }

        /// <summary>
        /// Converts the value of the current <see cref="LessThanOrEqualsFilter{T}"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="LessThanOrEqualsFilter{T}"/> object.</returns>
        public override string ToString(bool useFullNames)
        {
            if (LHS is IField lhs && RHS is IValue<T> rhs)
            {
                return $"{(useFullNames ? lhs.FullName : lhs.Name)} <= {rhs.Value}";
            }

            throw new NotSupportedException();
        }
    }
}
