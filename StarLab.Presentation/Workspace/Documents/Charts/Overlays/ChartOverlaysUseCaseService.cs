using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Charts.Overlays;
using StarLab.Presentation.Options;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A service that executes the use cases that implement the chart overlays panel functionality.
    /// </summary>
    public class ChartOverlaysUseCaseService : UseCaseService, IChartOverlaysUseCaseService
    {
        private readonly IUserSettings settings; // Provides access t othe user settings.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartOverlaysUseCaseService"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use cases.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        public ChartOverlaysUseCaseService(IUseCaseFactory factory, IMapper mapper, ISessionContext context)
            : base(factory, mapper) 
        {
            ArgumentNullException.ThrowIfNull(context, nameof(context));

            settings = context.Settings;
        }

        /// <summary>
        /// Executes the AddChartOverlay use case.
        /// </summary>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <param name="name">The name of the overlay.</param>
        public void AddChartOverlay(DocumentID id, IChartSettings chart, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(chart, nameof(chart));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var overlays = settings.Charts.Overlays;

            var useCase = Factory.CreateAddChartOverlayUseCase(ApplicationController.GetOutputPort<IChartOutputPort>(new ControllerID(id)));

            useCase.Execute(new AddChartOverlayUseCaseArgs(Mapper.Map<ChartDTO>(chart), name, overlays.DefaultFont, overlays.DefaultFontSize, overlays.DefaultColour));
        }

        /// <summary>
        /// Executes the RemoveChartOverlay use case.
        /// </summary>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <param name="chart">A <see cref="IChartSettings"/> that specifies the current state of the chart.</param>
        /// <param name="name">The name of the overlay.</param>
        public void RemoveChartOverlay(DocumentID id, IChartSettings chart, string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(chart, nameof(chart));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var useCase = Factory.CreateRemoveChartOverlayUseCase(ApplicationController.GetOutputPort<IChartOutputPort>(new ControllerID(id)));

            useCase.Execute(new RemoveChartOverlayUseCaseArgs(Mapper.Map<ChartDTO>(chart), name));
        }

        /// <summary>
        /// Executes the UpdateChart use case.
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
    }
}
