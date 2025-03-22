namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties that are common to all controllers.
    /// </summary>
    public interface IController
    {
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        string Name { get; }
    }
}
