using StarLab.Application.Workspace;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// View model representation of a database in the workspace hierarchy.
    /// </summary>
    internal class Database : IDatabase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Database"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="DatabaseDTO"/> representation of the database.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public Database(DatabaseDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Host = dto.Host ?? string.Empty;
            Name = dto.Name ?? string.Empty;
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
