namespace StarLab.Application.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IDatabaseManager : IDisposable
    {
        /// <summary>
        /// Gets the specified database. If a database with the specified name does not exist it will be created.
        /// </summary>
        /// <param name="database">The name of the database.</param>
        /// <returns>The requested <see cref="IDatabase"/> instance.</returns>
        IDatabase GetDatabase(string database);

        /// <summary>
        /// Opens a connection to the server with the specified host name and port number.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        void OpenConnection(string host, int port);
    }
}