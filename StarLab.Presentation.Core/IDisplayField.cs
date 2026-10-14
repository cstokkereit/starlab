namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a field that can be displayed in a view or child view.
    /// </summary>
    public interface IDisplayField
    {
        /// <summary>
        /// Gets the field caption.
        /// </summary>
        string Caption { get; }

        /// <summary>
        /// Gets the field name.
        /// </summary>
        string Name { get; }
    }
}
