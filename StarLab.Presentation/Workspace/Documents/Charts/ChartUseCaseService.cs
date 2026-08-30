using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A service that executes the use cases that implement chart document functionality.
    /// </summary>
    public class ChartUseCaseService : UseCaseService, IChartUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartUseCaseService"/>.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public ChartUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Executes the UpdateChart use case.
        /// </summary>
        /// <param name="workspace">The current <see cref="IWorkspace"/>.</param>
        /// <param name="id">The <see cref="DocumentID"> that identifies the document.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void UpdateChart(IWorkspace workspace, DocumentID id)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            // TODO
            // 1. Sort out initial population of chart with data when a document is first displayed
            // 2. This will need to change or other methods created to handle searching and filtering etc.

            var interactor = Factory.CreateUpdateChartUseCase(ApplicationController.GetOutputPort<IChartOutputPort>(new ControllerID(id)));

            var database = workspace.GetProject(workspace.GetDocument(id).Project).Database;

            interactor.Execute(new UpdateChartUseCaseArgs(id.ToString(), database.Host, database.Port, database.Name));
        }
    }
}
