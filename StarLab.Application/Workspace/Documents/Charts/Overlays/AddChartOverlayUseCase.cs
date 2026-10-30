using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A use case that adds an overlay to a chart.
    /// </summary>
    internal class AddChartOverlayUseCase : UseCase<IChartOutputPort>, IUseCase<AddChartOverlayUseCaseArgs>
    {
        private readonly IOverlayDataProvider provider; // A data provider that imports and caches the overlay data.

        private readonly IUseCase<ChartDTO> update; // A use case that updates the chart preview.

        /// <summary>
        /// Initialises a new instance of the <see cref="AddChartOverlayUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        /// <param name="provider">An <see cref="IOverlayDataProvider"/> that imports and caches the overlay data.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use cases.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public AddChartOverlayUseCase(IChartOutputPort outputPort, IMapper mapper, IOverlayDataProvider provider, IUseCaseFactory factory)
            : base(outputPort, mapper)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));

            update = factory.CreateUpdateChartPreviewUseCase(outputPort);
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="AddChartOverlayUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(AddChartOverlayUseCaseArgs args)
        {
            var data = provider.GetPointOverlayData();

            var point = new LabelledPointDTO
            {
                X = data.GetCoordinate(args.Chart.X1.Data),
                y = data.GetCoordinate(args.Chart.Y1.Data),
                Label = data.Label
            };

            OutputPort.AddOverlay(args.Overlay, point);

            var chart = new Chart(args.Chart);

            chart.AddOverlay(args.Overlay, new Font(args.Font, args.FontSize), args.Colour);

            var dto = Mapper.Map<ChartDTO>(chart);

            update.Execute(dto);
        }
    }
}
