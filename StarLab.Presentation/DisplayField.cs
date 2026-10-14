namespace StarLab.Presentation
{
    /// <summary>
    /// TODO : Finish
    /// </summary>
    internal class DisplayField : IDisplayField
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DisplayField"> class.
        /// </summary>
        /// <param name="name">The field name.</param>
        /// <param name="caption">The field caption.</param>
        public DisplayField(string name, string caption)
        {
            ArgumentException.ThrowIfNullOrEmpty(caption, nameof(caption));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            Caption = caption;
            Name = name;
        }

        /// <summary>
        /// Gets the field caption.
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// Gets the field name.
        /// </summary>
        public string Name { get; }
    }
}
