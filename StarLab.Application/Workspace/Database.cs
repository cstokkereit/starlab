namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Application model represention of a database.
    /// </summary>
    internal class Database
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Database"/>.</param>
        public Database(DatabaseDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Host = dto.Host;
            Name = dto.Name;
            Port = dto.Port;
        }

        /// <summary>
        /// Gets the host name.
        /// </summary>
        public string Host { get; }

        /// <summary>
        /// Gets the database name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the port number.
        /// </summary>
        public int Port { get; }
    }
}
