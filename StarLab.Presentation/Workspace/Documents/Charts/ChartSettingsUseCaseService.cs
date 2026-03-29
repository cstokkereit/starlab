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
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public ChartSettingsUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Executes the UpdateChart use case.
        /// </summary>
        /// <param name="id">The ID of the document that contains the chart.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UpdateChart(string id, IChartSettings chart)
        {
            ArgumentNullException.ThrowIfNull(chart, nameof(chart));

            var interactor = Factory.CreateUpdateChartUseCase(ApplicationController.GetOutputPort<IChartOutputPort>(id));

            interactor.Execute(Mapper.Map<ChartDTO>(chart));
        }

        /// <summary>
        /// Executes the UpdateDocument use case.
        /// </summary>
        /// <param name="workspace">A <see cref="IWorkspace"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The ID of the document that contains the chart.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void UpdateDocument(IWorkspace workspace, string id, IChartSettings chart)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentNullException.ThrowIfNull(chart, nameof(chart));
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            var interactor = Factory.CreateUpdateDocumentUseCase(ApplicationController.GetOutputPort<IApplicationOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), id, Mapper.Map<ChartDTO>(chart));
        }
    }
}
