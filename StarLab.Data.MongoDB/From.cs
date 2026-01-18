using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IFrom"/> interface.
    /// </summary>
    internal class From : FromFragment
    {
        /// <summary>
        /// Gets the <see cref="IMongoCollection{BsonDocument}"/> that contains the data.
        /// </summary>
        /// <param name="database">The <see cref="IMongoDatabase"/> that contains the collection.</param>
        /// <returns>The <see cref="IMongoCollection{TDocument}"/> that contains the data.</returns>
        public IMongoCollection<BsonDocument> GetCollection(IMongoDatabase database)
        {
            Debug.Assert(tables.Count == 1);

            return database.GetCollection<BsonDocument>(tables.First());
        }
    }
}
