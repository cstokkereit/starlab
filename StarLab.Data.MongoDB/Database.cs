using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Application.Data;
using StarLab.Domain.Entities;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class Database : IDatabase
    {
        private readonly IMongoDatabase database; // The wrapped Mongo database.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="database"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Database(IMongoDatabase database)
        {
            this.database = database ?? throw new ArgumentNullException(nameof(database));
        }

        /// <summary>
        /// Drops the collection with the specified name.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
        public void DropCollection(string name)
        {
            database.DropCollection(name);
        }

        /// <summary>
        /// Gets the <see cref="IMongoCollection{BsonDocument}"/> with the specified name.
        /// </summary>
        /// <param name="name">The name of the collection.</param>
        /// <returns>The <see cref="IMongoCollection{BsonDocument}"/> with the specified name.</returns>
        public IMongoCollection<BsonDocument> GetCollection(string name)
        {
            return database.GetCollection<BsonDocument>(name);
        }

        public IList<IStar> GetStars(IQuery query, int skip, int rowLimit)
        {
            // Need to check that just one table
            var collection = database.GetCollection<BsonDocument>(query.SelectStatement.Tables[0].Name);

            if (query is Query q)
            {
                collection.Find(q.GetFilter()).Project(q.GetProjection());
            }

            throw new ArgumentException();
        }

        public IForwardOnlyCursor<IStar> GetStars(IQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
