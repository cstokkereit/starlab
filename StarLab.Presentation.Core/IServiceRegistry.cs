namespace StarLab.Presentation
{
    /// <summary>
    /// Defines the methods required to initialise and access the registered services.
    /// </summary>
    public interface IServiceRegistry
    {
        /// <summary>
        /// Gets the specified service.
        /// </summary>
        /// <typeparam name="TService">The type of the required service.</typeparam>
        /// <returns>The specified service.</returns>
        TService GetService<TService>();

        /// <summary>
        /// Initialises the registered services.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        void Initialise(IApplicationController controller);
    }
}
