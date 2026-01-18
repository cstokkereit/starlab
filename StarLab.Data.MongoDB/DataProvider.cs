using MongoDB.Bson;
using MongoDB.Driver;
using System.Diagnostics;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IDataProvider"/> interface that provides methods for accessing the data contained within a MongoDB database.
    /// </summary>
    public class DataProvider : IDataProvider
    {
        private readonly Connection connection; // A wrapped connection to the MongoDB server.

        private IMongoDatabase? database; // The MongoDB database that contains the data.

        /// <summary>
        /// Initialises a new instance of the <see cref="DataProvider"/> class.
        /// </summary>
        /// <param name="connection">A <see cref="Connection"/> that can be used to access the MongoDB server.</param>
        public DataProvider(Connection connection)
        {
            this.connection = connection;
        }

        /// <summary>
        /// Retrieves the data specified in the query. If a large amount of data could be returned by the query use the skip and rowLimit parameters to limit the amount of data returned.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        /// <param name="skip">The number of records to skip before starting to retrieve records.</param>
        /// <param name="rowLimit">The maximum number of records to retrieve.</param>
        /// <returns>An <see cref="IList{IStar}"/> containg the specified values.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IList<IStar> GetStars(IQuery query, int skip, int rowLimit)
        {
            if (database == null) throw new InvalidOperationException(); // TODO

            if (query.FromClause.Size == 0) throw new InvalidOperationException(); // TODO

            var stars = new List<IStar>();

            var documents = GetDocuments((Query)query, skip, rowLimit);

            foreach (var document in documents)
            {
                stars.Add(new StarData(document));
            }

            return stars;
        }

        /// <summary>
        /// Retrieves the data specified in the query. This is the preferred method for returning large amounts of data.
        /// </summary>
        /// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        /// <returns>An <see cref="ICursor{IStar}"/> containg the specified values.</returns>
        public ICursor<IStar> GetStars(IQuery query)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Closes the database that contains the data.
        /// </summary>
        public void CloseDatabase()
        {
            if (database != null) database = null;
        }

        /// <summary>
        /// Opens the specified database.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        public void OpenDatabase(string database)
        {
            if (this.database != null) throw new InvalidOperationException();

            this.database = connection.GetDatabase(database);
        }

        /// <summary>
        /// Gets the documents specified by the <see cref="Query">.
        /// </summary>
        /// <param name="query">The <see cref="Query"/> that determines which values will be returned.</param>
        /// <param name="skip">The number of records to skip before starting to retrieve records.</param>
        /// <param name="rowLimit">The maximum number of records to retrieve.</param>
        /// <returns>An <see cref="IEnumerable{BsonDocument}"/> containing the specified records.</returns>
        private IEnumerable<BsonDocument> GetDocuments(Query query, int skip, int limit)
        {
            Debug.Assert(database != null);

            if (query.FromClause.Size > 1) throw new NotImplementedException();

            var table = query.SelectStatement.Tables[0];

            var collection = database.GetCollection<BsonDocument>(table.Name);

            List<BsonDocument> documents;

            if (!table.SelectAll)
            {
                documents = collection.Find(query.GetFilter()).Project(query.GetProjection()).Skip(skip).Limit(limit).ToList();
            }
            else
            {
                documents = collection.Find(query.GetFilter()).Skip(skip).Limit(limit).ToList();
            }

            return documents;
        }
    }
}
