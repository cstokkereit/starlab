using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application
{
    /// <summary>
    /// The base class for all use case interactors.
    /// </summary>
    /// <typeparam name="TOutputPort">The <see cref="IOutputPort"/> that updates the UI in response to the outputs of the use case.</typeparam>
    public abstract class UseCaseInteractor<TOutputPort> where TOutputPort : IOutputPort
    {
        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        private readonly TOutputPort outputPort; // Updates the UI in response to the outputs of the use case.

        /// <summary>
        /// Initialises a new instance of the <see cref="UseCaseInteractor{TOutputPort}"/> class.
        /// </summary>
        /// <param name="outputPort">The <see cref="IOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public UseCaseInteractor(TOutputPort outputPort, IMapper mapper)
        {
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Gets the <see cref="IMapper"/> used to copy data from model objects to data transfer objects and vice versa.
        /// </summary>
        protected IMapper Mapper => mapper;

        /// <summary>
        /// Gets the <see cref="IOutputPort"/> that updates the UI in response to the outputs of the use case.
        /// </summary>
        protected TOutputPort OutputPort => outputPort;

        /// <summary>
        /// Displays a confirmation dialog box with the specified message.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <returns>true if the action was confirmed; false otherwise.</returns>
        protected bool ConfirmAction(string message)
        {
            return OutputPort.ShowMessage(Resources.StarLab, message, InteractionType.Warning, InteractionResponses.OKCancel) == InteractionResult.OK;
        }
    }
}
