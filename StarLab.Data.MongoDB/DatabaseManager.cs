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

        private MongoClient? client; //

        /// <summary>
        /// Drops the database with the specified name.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void DropDatabase(string database)
        {
            if (client == null) throw new InvalidOperationException(); // TODO Connection not open

            client.DropDatabase(database);
        }

        /// <summary>
        /// Gets the specified database. If a database with the specified name does not exist it will be created.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        /// <returns>The requested <see cref="IDatabase"/> instance.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IDatabase GetDatabase(string database)
        {
            if (client == null) throw new InvalidOperationException(); // TODO Connection not open

            return new Database(client.GetDatabase(database));
        }

        /// <summary>
        /// Gets a list containing the names of the available databases.
        /// </summary>
        /// <returns>A <see cref="List{string}"/> containing the database names.</returns>
        public List<string> GetDatabaseNames()
        {
            if (client == null) throw new InvalidOperationException(); // TODO Connection not open

            return client.ListDatabaseNames().ToList();
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
        ///// Gets a list containing the names of the available databases.
        ///// </summary>
        ///// <returns>A list containing the names of the available databases.</returns>
        //public List<string> GetDatabaseNames()
        //{
        //    if (client == null) throw new InvalidOperationException(); // TODO Connection not open
        //    return client.ListDatabaseNames().ToList() ?? [];
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
    }
}
