namespace StarLab.Application.Data
{
    /// <summary>
    /// Holds the details of a database connection.
    /// </summary>
    public interface IConnection
    {
        /// <summary>
        /// Gets the connection string.
        /// </summary>
        string ConnectionString { get; }

        /// <summary>
        /// Gets the connection name.
        /// </summary>
        string Name { get; }
    }
}
