using AutoMapper;
using StarLab.Application.Data;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Application
{
    /// <summary>
    /// A factory for creating use cases.
    /// </summary>
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        private readonly ISerialisationService serialiser; // Used to serialise and deserialise model objects.

        private readonly IOverlayDataProvider overlayData; // Used to provide access to the overlay data.

        private readonly IDatabaseManager databases; // Used to retrieve data from the database.

        private readonly IQueryBuilder queries; // Used to build database queries.

        /// <summary>
        /// Initialises a new instance of the <see cref="UseCaseFactory"/> class.
        /// </summary>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="databases">An <see cref="IDatabaseManager"/> that will be used to retrieve data from the database.</param>
        /// <param name="queries">An <see cref="IQueryBuilder"/> that will be used to build database queries.</param>
        /// <param name="serialiser">An <see cref="ISerialisationService"/> that will be used to serialise and deserialise model objects.</param>
        public UseCaseFactory(IMapper mapper, IDatabaseManager databases, IQueryBuilder queries, IOverlayDataProvider overlays, ISerialisationService serialiser)
        {
            this.databases = databases ?? throw new ArgumentNullException(nameof(databases));
            this.serialiser = serialiser ?? throw new ArgumentNullException(nameof(serialiser));
            this.overlayData = overlays ?? throw new ArgumentNullException(nameof(overlays));
            this.queries = queries ?? throw new ArgumentNullException(nameof(queries));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a use case that adds an overlay to a chart.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddChartOverlayUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddChartOverlayUseCaseArgs> CreateAddChartOverlayUseCase(IChartOutputPort outputPort)
        {
            return new AddChartOverlayUseCase(outputPort, mapper, overlayData, this);
        }

        /// <summary>
        /// Creates a use case that adds a document to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddDocumentUseCaseArgs> CreateAddDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddDocumentUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddFolderUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddFolderUseCaseArgs> CreateAddFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddFolderUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that adds a project to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddProjectUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddProjectUseCaseArgs> CreateAddProjectUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddProjectUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ClipboardUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<ClipboardUseCaseArgs> CreateCopyAndPasteUseCase(IWorkspaceOutputPort outputPort)
        {
            return new CopyAndPasteUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ClipboardUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<ClipboardUseCaseArgs> CreateCutAndPasteUseCase(IWorkspaceOutputPort outputPort)
        {
            return new CutAndPasteUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{DeleteDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<DeleteDocumentUseCaseArgs> CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteDocumentUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{DeleteFolderUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<DeleteFolderUseCaseArgs> CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteFolderUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that loads a workspace from a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{string}"/> that implements the use case.</returns>
        public IUseCase<string> CreateOpenWorkspaceUseCase(IApplicationOutputPort outputPort)
        {
            return new OpenWorkspaceUseCase(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that TODO
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        public IUseCaseAsync<PlotChartUseCaseArgs> CreatePlotChartUseCase(IChartOutputPort outputPort)
        {
            return new PlotChartUseCase(outputPort, mapper, databases, queries, overlayData, this);
        }

        /// <summary>
        /// Creates a use case that removes an overlay from a chart.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RemoveChartOverlayUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RemoveChartOverlayUseCaseArgs> CreateRemoveChartOverlayUseCase(IChartOutputPort outputPort)
        {
            return new RemoveChartOverlayUseCase(outputPort, mapper, this);
        }

        /// <summary>
        /// Creates a use case that renames a document in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RenameDocumentUseCaseArgs> CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameDocumentUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameFolderUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RenameFolderUseCaseArgs> CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameFolderUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that renames the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameWorkspaceUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RenameWorkspaceUseCaseArgs> CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameWorkspaceUseCase(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that saves the current workspace to a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO> CreateSaveWorkspaceUseCase(IApplicationOutputPort outputPort)
        {
            return new SaveWorkspaceUseCase(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that updates a preview chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ChartDTO}"/> that implements the use case.</returns>
        public IUseCase<ChartDTO> CreateUpdateChartPreviewUseCase(IChartOutputPort outputPort)
        {
            return new UpdateChartPreviewUseCase(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case that updates a chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{UpdateDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<UpdateChartUseCaseArgs> CreateUpdateChartUseCase(IApplicationOutputPort outputPort)
        {
            return new UpdateChartUseCase(outputPort, mapper);
        }
    }
}
