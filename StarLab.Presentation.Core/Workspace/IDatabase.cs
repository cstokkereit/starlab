namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Represents a database in the workspace hierarchy.
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// Gets the host name.
        /// </summary>
        string Host { get; }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the port number.
        /// </summary>
        int Port { get; }
    }
}
