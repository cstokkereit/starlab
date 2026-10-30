using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that updates the chart preview.
    /// </summary>
    internal class UpdateChartPreviewUseCase : UseCase<IChartOutputPort>, IUseCase<ChartDTO>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UpdateChartPreviewUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        public UpdateChartPreviewUseCase(IChartOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the current state of the chart.</param>
        public void Execute(ChartDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            OutputPort.ApplyChanges(dto);
        }
    }
}
