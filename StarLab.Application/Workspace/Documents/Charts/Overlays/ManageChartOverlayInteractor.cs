using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A use case that manages the chart overlays.
    /// </summary>
    internal class ManageChartOverlayInteractor : UseCaseInteractor<IChartOutputPort>, IUseCase<ChartOverlayUseCaseArgs>
    {
        private readonly IOverlayDataProvider provider; // Provides access to the overlay data.

        /// <summary>
        /// Initialises a new instance of the <see cref="ManageChartOverlayInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public ManageChartOverlayInteractor(IChartOutputPort outputPort, IMapper mapper, IOverlayDataProvider provider)
            : base(outputPort, mapper)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="ChartOverlayUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(ChartOverlayUseCaseArgs args)
        {
            
        }
    }
}
