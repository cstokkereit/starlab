using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// Represents part of MongoDB filter.
    /// </summary>
    internal interface IFilterAdapter
    {
        /// <summary>
        /// Converts the internal representation of a query to the equivalent <see cref="FilterDefinition{BsonDocument}"/>.
        /// </summary>
        /// <returns>A <see cref="FilterDefinition{BsonDocument}"/> that can be used to filter the documents in a MongoDB collection.</returns>
        FilterDefinition<BsonDocument> GetFilter();
    }
}
