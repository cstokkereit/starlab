using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A use case that removes an overlay from a chart.
    /// </summary>
    internal class RemoveChartOverlayUseCase : UseCase<IChartOutputPort>, IUseCase<RemoveChartOverlayUseCaseArgs>
    {
        private readonly IUseCase<ChartDTO> updatePreview; // A use case that updates the chart preview.

        /// <summary>
        /// Initialises a new instance of the <see cref="RemoveChartOverlayUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use cases.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public RemoveChartOverlayUseCase(IChartOutputPort outputPort, IMapper mapper, IUseCaseFactory factory)
            : base(outputPort, mapper)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            updatePreview = factory.CreateUpdateChartPreviewUseCase(outputPort);
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="RemoveChartOverlayUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(RemoveChartOverlayUseCaseArgs args)
        {
            OutputPort.RemoveOverlay(args.Overlay);

            var chart = new Chart(args.Chart);

            chart.RemoveOverlay(args.Overlay);

            var dto = Mapper.Map<ChartDTO>(chart);

            updatePreview.Execute(dto);
        }
    }
}
