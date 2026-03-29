namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the properties that are common to all controllers.
    /// </summary>
    public interface IController : IDisposable
    {
        /// <summary>
        /// Gets the controller ID.
        /// </summary>
        string ID { get; }
    }
}
