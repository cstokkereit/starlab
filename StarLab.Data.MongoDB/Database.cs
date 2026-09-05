using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Application.Data;
using StarLab.Domain.Entities;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IDatabase"/> interface.
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

        /// <summary>
        /// Retrieves the data specified in the query. If a large amount of data could be returned by the query use the skip and limit parameters to limit the amount of data returned.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        /// <param name="skip">The number of records to skip before starting to retrieve records.</param>
        /// <param name="limit">The maximum number of records to retrieve.</param>
        /// <returns>An <see cref="IList{IStar}"/> containg the specified values.</returns>
        public IList<IStar> GetStars(IQuery query, int skip, int limit)
        {
            if (query.FromClause.Size > 1) throw new NotImplementedException(); // Currently works with just one table

            var stars = new List<IStar>();

            if (query is Query q)
            {
                var table = query.SelectStatement.Tables[0];

                var collection = database.GetCollection<BsonDocument>(table.Name);

                List<BsonDocument> documents;

                if (!table.SelectAll)
                {
                    documents = collection.Find(q.GetFilter()).Project(q.GetProjection()).Skip(skip).Limit(limit).ToList();
                }
                else
                {
                    documents = collection.Find(q.GetFilter()).Skip(skip).Limit(limit).ToList();
                }

                var data = new EntityData(); 

                foreach (var document in documents)
                {
                    data.SetData(document);

                    stars.Add(new Star(data));
                }
            }

            return stars;
        }

        /// <summary>
        /// Retrieves the data specified in the query. This is the preferred method for returning large amounts of data.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        /// <returns>An <see cref="IForwardOnlyCursor{IStar}"/> containg the specified values.</returns>
        public IForwardOnlyCursor<IStar> GetStars(IQuery query)
        {
            if (query.FromClause.Size > 1) throw new NotImplementedException(); // Currently works with just one table

            if (query is Query q)
            {
                var table = query.SelectStatement.Tables[0];

                var collection = database.GetCollection<BsonDocument>(table.Name);

                IAsyncCursor<BsonDocument> documents;

                if (!table.SelectAll)
                {
                    documents = collection.Find(q.GetFilter()).Project(q.GetProjection()).ToCursor();
                }
                else
                {
                    documents = collection.FindSync(q.GetFilter());
                }

                return new Stars(documents);
            }

            return new Stars();
        }
    }
}
