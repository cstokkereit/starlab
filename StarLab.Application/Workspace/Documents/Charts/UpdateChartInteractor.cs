using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that applies preview changes to a chart.
    /// </summary>
    internal class UpdateChartInteractor : UseCaseInteractor<IChartOutputPort>, IUseCase<ChartDTO>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public UpdateChartInteractor(IChartOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the current state of the chart.</param>
        public void Execute(ChartDTO dto)
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

            OutputPort.UpdateChart(dto);
        }
    }
}
