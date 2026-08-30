using log4net;
using MongoDB.Driver;
using StarLab.Application.Data;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IDatabaseManager"/> interface that provides methods for accessing the data contained within a MongoDB database.
    /// </summary>
    public class DatabaseManager : IDatabaseManager
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DatabaseManager)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, MongoClient> clients = new Dictionary<string, MongoClient>(); // A dictionary containing the available MongoDB clients.

        private MongoClient? client;

        /// <summary>
        /// Gets the specified database. If a database with the specified name does not exist it will be created.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        /// <returns>The requested <see cref="IDatabase"/> instance.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IDatabase GetDatabase(string database)
        {
            if (client != null)
            {
                return new Database(client.GetDatabase(database));
            }

            throw new InvalidOperationException(); // TODO Connection not open
        }

        /// <summary>
        /// Opens a connection to the server with the specified host name and port number.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        public void OpenConnection(string host, int port)
        {
            var connection = new Connection(host, port);

            if (!clients.ContainsKey(connection.Name))
            {
                clients.Add(connection.Name, new MongoClient(connection.ConnectionString));
            }

            client = clients[connection.Name];
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~DatabaseManager()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="DatabaseManager"/> object.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        ///// <summary>
        ///// Drops the specified database.
        ///// </summary>
        ///// <param name="database">The name of the database that is being dropped.</param>
        ///// <exception cref="InvalidOperationException"></exception>
        //public void DropDatabase(string database)
        //{
        //    if (client == null) throw new InvalidOperationException();

        //    client.DropDatabase(database);
        //}

        ///// <summary>
        ///// Gets the specified <see cref="IMongoDatabase"/>.
        ///// </summary>
        ///// <param name="database">The name of the required database.</param>
        ///// <returns>The specified <see cref="IMongoDatabase"/>.</returns>
        ///// <exception cref="InvalidOperationException"></exception>
        //public IMongoDatabase GetDatabase(string database)
        //{
        //    if (client == null) throw new InvalidOperationException();

        //    return client.GetDatabase(database);
        //}

        ///// <summary>
        ///// Gets a list containing the names of the available databases.
        ///// </summary>
        ///// <returns>A list containing the names of the available databases.</returns>
        //public List<string> GetDatabaseNames()
        //{
        //    return client?.ListDatabaseNames().ToList() ?? [];
        //}

        /// <summary>
        /// Releases all resources used by the <see cref="DatabaseManager"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (var client in clients.Values)
                {
                    client.Dispose();
                }
            }
        }







        ///// <summary>
        ///// Initialises a new instance of the <see cref="DatabaseManager"/> class.
        ///// </summary>
        //public DatabaseManager()
        //{
        //    // Do Nothing
        //}

        ///// <summary>
        ///// Closes the database that contains the data.
        ///// </summary>
        //public void CloseDatabase()
        //{
        //    if (database != null) database = null;
        //}

        ///// <summary>
        ///// Retrieves the data specified in the query. If a large amount of data could be returned by the query use the skip and rowLimit parameters to limit the amount of data returned.
        ///// </summary>
        ///// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        ///// <param name="skip">The number of records to skip before starting to retrieve records.</param>
        ///// <param name="rowLimit">The maximum number of records to retrieve.</param>
        ///// <returns>An <see cref="IList{IStar}"/> containg the specified values.</returns>
        ///// <exception cref="InvalidOperationException"></exception>
        //public IList<IStar> GetStars(IQuery query, int skip, int rowLimit)
        //{
        //    if (database == null) throw new InvalidOperationException(); // TODO

        //    if (query.FromClause.Size == 0) throw new InvalidOperationException(); // TODO

        //    var stars = new List<IStar>();

        //    var documents = GetDocuments((Query)query, skip, rowLimit);

        //    foreach (var document in documents)
        //    {
        //        stars.Add(new Star(document));
        //    }

        //    return stars;
        //}

        ///// <summary>
        ///// Retrieves the data specified in the query. This is the preferred method for returning large amounts of data.
        ///// </summary>
        ///// <param name="query">The <see cref="IQuery"/> that determines which values will be returned.</param>
        ///// <returns>An <see cref="IForwardOnlyCursor{IStar}"/> containg the specified values.</returns>
        //public IForwardOnlyCursor<IStar> GetStars(IQuery query)
        //{
        //    if (database == null) throw new InvalidOperationException(); // TODO

        //    if (query.FromClause.Size == 0) throw new InvalidOperationException(); // TODO

        //    return new Stars(GetDocuments((Query)query));
        //}

        ///// <summary>
        ///// Opens a connection to the data provider.
        ///// </summary>
        ///// <param name="connection">A connection string that specifies how to connect to the data provider.</param>
        ///// <returns>An open connection.</returns>
        //public IConnection OpenConnection(string connection)
        //{
        //    this.connection = Connection.OpenConnection(connection);

        //    return this.connection;
        //}

        ///// <summary>
        ///// Opens the specified database.
        ///// </summary>
        ///// <param name="database">The name of the database.</param>
        //public void OpenDatabase(string database)
        //{
        //    if (this.database != null) throw new InvalidOperationException();

        //    this.database = connection.GetDatabase(database);
        //}

        ///// <summary>
        ///// Gets the documents specified by the <see cref="Query">.
        ///// </summary>
        ///// <param name="query">The <see cref="Query"/> that determines which values will be returned.</param>
        ///// <param name="skip">The number of records to skip before starting to retrieve records.</param>
        ///// <param name="rowLimit">The maximum number of records to retrieve.</param>
        ///// <returns>An <see cref="IEnumerable{BsonDocument}"/> containing the specified records.</returns>
        //private IEnumerable<BsonDocument> GetDocuments(Query query, int skip, int limit)
        //{
        //    Debug.Assert(database != null);

        //    if (query.FromClause.Size > 1) throw new NotImplementedException();

        //    var table = query.SelectStatement.Tables[0];

        //    var collection = database.GetCollection<BsonDocument>(table.Name);

        //    List<BsonDocument> documents;

        //    if (!table.SelectAll)
        //    {
        //        documents = collection.Find(query.GetFilter()).Project(query.GetProjection()).Skip(skip).Limit(limit).ToList();
        //    }
        //    else
        //    {
        //        documents = collection.Find(query.GetFilter()).Skip(skip).Limit(limit).ToList();
        //    }

        //    return documents;
        //}

        ///// <summary>
        ///// Gets the documents specified by the <see cref="Query">.
        ///// </summary>
        ///// <param name="query">The <see cref="Query"/> that determines which values will be returned.</param>
        ///// <returns>An <see cref="IAsyncCursor{BsonDocument}"/> containing the specified records.</returns>
        //private IAsyncCursor<BsonDocument> GetDocuments(Query query)
        //{
        //    Debug.Assert(database != null);

        //    if (query.FromClause.Size > 1) throw new NotImplementedException();

        //    var table = query.SelectStatement.Tables[0];

        //    var collection = database.GetCollection<BsonDocument>(table.Name);

        //    IAsyncCursor<BsonDocument> documents;

        //    if (!table.SelectAll)
        //    {
        //        documents = collection.Find(query.GetFilter()).Project(query.GetProjection()).ToCursor();
        //    }
        //    else
        //    {
        //        documents = collection.Find(query.GetFilter()).ToCursor();
        //    }

        //    return documents;
        //}
    }
}
