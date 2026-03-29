namespace StarLab.Presentation
{
    /// <summary>
    /// Defines a service that implements the use cases that are initiated by a particular controller or group of controllers.
    /// </summary>
    public interface IUseCaseService
    {
        /// <summary>
        /// Initialises the use cases.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        void Initialise(IApplicationController controller);
    }
}
