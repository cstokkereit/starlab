using StarLab.Application;

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
        ViewID ID { get; }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        string Name { get; }
    }
}
