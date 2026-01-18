using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A predicate adapter that allows all data to be retrieved.
    /// </summary>
    internal class EmptyFilter : Predicate, IFilterAdapter
    {
        /// <summary>
        /// Gets the <see cref="FilterDefinition{BsonDocument}"/> specified by this predicate.
        /// </summary>
        /// <returns>A <see cref="FilterDefinition{BsonDocument}"/> that specifies which documents to retrieve.</returns>
        public FilterDefinition<BsonDocument> GetFilter()
        {
            return Builders<BsonDocument>.Filter.Empty;
        }

        /// <summary>
        /// Converts the value of the current <see cref="EmptyFilter"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="EmptyFilter"/> object.</returns>
        public override string ToString(bool useFullNames)
        {
            return string.Empty;
        }
    }
}
