namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties and methods that are common to all views.
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// Gets the view ID.
        /// </summary>
        string ID { get; }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        string Name { get; }
    }
}
