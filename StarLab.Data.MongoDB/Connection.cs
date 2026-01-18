using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A wrapper around the <see cref="MongoClient"/> that provides access to the MongoDB server.
    /// </summary>
    public class Connection
    {
        private MongoClient? client; // Provides access to the MongoDB server.

        /// <summary>
        /// Initialises a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        public Connection(string host, int port)
            : this($"mongodb://{host}:{port}") { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="url">A connection string or URL that specifies how to connect to the MongoDB server.</param>
        public Connection(string url)
        {
            ConnectionString = url;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Connection"/> class.
        /// </summary>
        public Connection()
            : this("localhost", 27017) { }

        /// <summary>
        /// Gets the connection string or URL that specifies how to connect to the MongoDB server.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Closes the connection to the MongoDB server.
        /// </summary>
        public void Close()
        {
            if (client != null)
            {
                client.Dispose();
                client = null;
            }
        }

        /// <summary>
        /// Drops the specified database.
        /// </summary>
        /// <param name="database">The name of the database that is being dropped.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DropDatabase(string database)
        {
            if (client == null) throw new InvalidOperationException();

            client.DropDatabase(database);
        }

        /// <summary>
        /// Gets the specified <see cref="IMongoDatabase"/>.
        /// </summary>
        /// <param name="database">The name of the required database.</param>
        /// <returns>The specified <see cref="IMongoDatabase"/>.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IMongoDatabase GetDatabase(string database)
        {
            if (client == null) throw new InvalidOperationException();

            return client.GetDatabase(database);
        }

        /// <summary>
        /// Opens a connection to the MongoDB server.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void Open()
        {
            if (client != null) throw new InvalidOperationException();

            client = new MongoClient(ConnectionString);
        }
    }
}
