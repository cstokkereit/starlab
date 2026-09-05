namespace StarLab.Application.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IDatabaseManager : IDisposable
    {
        /// <summary>
        /// Drops the specified database.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        void DropDatabase(string database);

        /// <summary>
        /// Gets the specified database. If a database with the specified name does not exist it will be created.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        /// <returns>The requested <see cref="IDatabase"/> instance.</returns>
        IDatabase GetDatabase(string database);

        /// <summary>
        /// Gets a list containing the names of the available databases.
        /// </summary>
        /// <returns>A <see cref="List{string}"/> containing the database names.</returns>
        List<string> GetDatabaseNames();

        /// <summary>
        /// Opens a connection to the server with the specified host name and port number.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        void OpenConnection(string host, int port);
    }
}