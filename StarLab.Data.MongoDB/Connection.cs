using StarLab.Application.Data;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// Holds the information required to connect to a MongoDB server.
    /// </summary>
    public class Connection : IConnection
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Connection"/> class.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        public Connection(string host, int port)
        {
            ConnectionString = $"mongodb://{host}:{port}";

            Name = $"{host}:{port}";
        }

        /// <summary>
        /// Gets the connection string that specifies how to connect to the MongoDB server.
        /// </summary>
        public string ConnectionString { get; }

        /// <summary>
        /// Gets the connection name;
        /// </summary>
        public string Name { get; }
    }
}
