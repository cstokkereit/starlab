namespace StarLab.Presentation.Configuration
{
    public class OverlayConfiguration : IOverlayConfiguration
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayConfiguration"/> class.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="typeName">The overlay type name.</param>
        /// <param name="dataSource"></param>
        /// <exception cref="ArgumentException"></exception>
        public OverlayConfiguration(string name, string typeName, string dataSource)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(typeName, nameof(typeName));

            DataSource = dataSource;
            TypeName = typeName;
            Name = name;
        }

        /// <summary>
        /// TTODO
        /// </summary>
        public string DataSource { get; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type name of the overlay.
        /// </summary>
        public string TypeName { get; }
    }
}
