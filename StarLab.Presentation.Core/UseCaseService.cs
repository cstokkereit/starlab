using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    /// <summary>
    /// The base class for all use case services.
    /// </summary>
    public class UseCaseService : IUseCaseService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UseCaseService)); // The logger that will be used for writing log messages.

        private IApplicationController? controller; // Tha application controller.

        /// <summary>
        /// Initialises a new instance of the <see cref="UseCaseService"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UseCaseService(IUseCaseFactory factory, IMapper mapper) 
        {
            Factory = factory ?? throw new ArgumentNullException(nameof(factory));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the application controller.
        /// </summary>
        protected IApplicationController ApplicationController
        {
            get
            {
                if (controller == null) throw new InvalidOperationException(Resources.NotInitialised);

                return controller;
            }
        }

        /// <summary>
        /// Gets the <see cref="IUseCaseFactory"/> used to create use case interactors.
        /// </summary>
        protected IUseCaseFactory Factory { get; }

        /// <summary>
        /// Gets the <see cref="IMapper"/> used to copy data from model objects to data transfer objects and vice versa.
        /// </summary>
        protected IMapper Mapper { get; }

        /// <summary>
        /// Initialises the <see cref="UseCaseService"/>.
        /// </summary>
        /// <param name="controller">The application controller.</param>
        public void Initialise(IApplicationController controller)
        {
            log.Debug(string.Format(LogEntries.Initialised, this));

            this.controller = controller;
        }
    }
}
