using AutoMapper;
using System.Diagnostics;

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

        private readonly Stopwatch stopwatch = new Stopwatch(); // A stopwatch that can be used for benchmarking code execution.

        /// <summary>
        /// Initialises a new instance of the <see cref="UseCaseInteractor{TOutputPort}"/> class.
        /// </summary>
        /// <param name="outputPort">The <see cref="IOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected UseCaseInteractor(TOutputPort outputPort, IMapper mapper)
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
        /// Stops the performance benchmarking stopwatch and returns the elapsed time in milliseconds.
        /// </summary>
        /// <returns>The ellapsed time in milliseconds.</returns>
        protected long GetElapsedTime()
        {
            stopwatch.Stop();

            return stopwatch.ElapsedMilliseconds;
        }

        /// <summary>
        /// Starts the performance benchmarking stopwatch.
        /// </summary>
        protected void StartStopWatch()
        {
            stopwatch.Restart();
        }
    }
}
