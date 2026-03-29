using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    /// <summary>
    /// Provides access to the registered services.
    /// </summary>
    public class ServiceRegistry : IServiceRegistry
    {
        private readonly IEnumerable<IUseCaseService> services; // A collection containing the registered services.

        /// <summary>
        /// Initialises a new instance of the <see cref="ServiceRegistry"/> class.
        /// </summary>
        /// <param name="services">An <see cref="IEnumerable{IUseCaseService}"/> that contains the registered services.</param>
        public ServiceRegistry(IEnumerable<IUseCaseService> services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Gets the specified service.
        /// </summary>
        /// <typeparam name="TService">The type of the required service.</typeparam>
        /// <returns>The specified service.</returns>
        /// <exception cref="Exception"></exception>
        public TService GetService<TService>()
        {
            foreach (var service in services)
            {
                if (service is TService required) return required;
            }

            throw new Exception(string.Format(Resources.UnknownType, typeof(TService)));
        }

        /// <summary>
        /// Initialises the registered services.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        public void Initialise(IApplicationController controller)
        {
            foreach (var service in services)
            {
                service.Initialise(controller);
            }
        }
    }
}
