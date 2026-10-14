using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A service that executes the use cases that implement the chart settings panel functionality.
    /// </summary>
    public class ChartSettingsUseCaseService : UseCaseService, IChartSettingsUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsUseCaseService"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use cases.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public ChartSettingsUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Executes the UpdateDocument use case.
        /// </summary>
        /// <param name="workspace">A <see cref="IWorkspace"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateChart(IWorkspace workspace, DocumentID id, IChartSettings chart)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentNullException.ThrowIfNull(chart, nameof(chart));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var useCase = Factory.CreateUpdateChartUseCase(ApplicationController.GetOutputPort<IApplicationOutputPort>());

            useCase.Execute(new UpdateChartUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), id.ToString(), Mapper.Map<ChartDTO>(chart)));
        }

        /// <summary>
        /// Executes the ApplyChartSettings use case.
        /// </summary>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UpdateChartPreview(DocumentID id, IChartSettings chart)
        {
            ArgumentNullException.ThrowIfNull(chart, nameof(chart));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var useCase = Factory.CreateUpdateChartPreviewUseCase(ApplicationController.GetOutputPort<IChartOutputPort>(new ControllerID(id)));

            var dto = Mapper.Map<ChartDTO>(chart);

            useCase.Execute(dto);
        }
    }
}
