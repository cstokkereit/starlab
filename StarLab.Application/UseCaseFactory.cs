using AutoMapper;
using StarLab.Application.Data;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Application
{
    /// <summary>
    /// A factory for creating use case interactors.
    /// </summary>
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        private readonly ISerialisationProvider serialiser; // Used to serialise and deserialise model objects.

        private readonly IDatabaseManager dataProvider; // Used to retrieve data from the database.

        /// <summary>
        /// Initialises a new instance of the <see cref="UseCaseFactory"/> class.
        /// </summary>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="dataProvider">An <see cref="IDatabaseManager"/> that will be used to retrieve data from the database.</param>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used for serialise and deserialisation of model objects.</param>
        public UseCaseFactory(IMapper mapper, IDatabaseManager dataProvider, ISerialisationProvider serialiser)
        {
            this.dataProvider = dataProvider ?? throw new ArgumentNullException(nameof(dataProvider));
            this.serialiser = serialiser ?? throw new ArgumentNullException(nameof(serialiser));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Creates a use case interactor that adds a document to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddDocumentUseCaseArgs> CreateAddDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddFolderUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddFolderUseCaseArgs> CreateAddFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that adds a project to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddProjectUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<AddProjectUseCaseArgs> CreateAddProjectUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddProjectInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that updates a chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ChartDTO}"/> that implements the use case.</returns>
        public IUseCase<ChartDTO> CreateApplyChartSettingsUseCase(IChartOutputPort outputPort)
        {
            return new ApplyChartSettingsInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ClipboardUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<ClipboardUseCaseArgs> CreateCopyAndPasteUseCase(IWorkspaceOutputPort outputPort)
        {
            return new CopyAndPasteInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ClipboardUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<ClipboardUseCaseArgs> CreateCutAndPasteUseCase(IWorkspaceOutputPort outputPort)
        {
            return new CutAndPasteInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{DeleteDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<DeleteDocumentUseCaseArgs> CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{DeleteFolderUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<DeleteFolderUseCaseArgs> CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that loads a workspace from a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{string}"/> that implements the use case.</returns>
        public IUseCase<string> CreateOpenWorkspaceUseCase(IApplicationOutputPort outputPort)
        {
            return new OpenWorkspaceInteractor(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames a document in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RenameDocumentUseCaseArgs> CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameFolderUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RenameFolderUseCaseArgs> CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameWorkspaceUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<RenameWorkspaceUseCaseArgs> CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameWorkspaceInteractor(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that saves the current workspace to a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO> CreateSaveWorkspaceUseCase(IApplicationOutputPort outputPort)
        {
            return new SaveWorkspaceInteractor(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that TODO
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        public IUseCase<UpdateChartUseCaseArgs> CreateUpdateChartUseCase(IChartOutputPort outputPort)
        {
            return new UpdateChartInteractor(outputPort, mapper, dataProvider);
        }

        /// <summary>
        /// Creates a use case interactor that updates a document in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{UpdateDocumentUseCaseArgs}"/> that implements the use case.</returns>
        public IUseCase<UpdateDocumentUseCaseArgs> CreateUpdateDocumentUseCase(IApplicationOutputPort outputPort)
        {
            return new UpdateDocumentInteractor(outputPort, mapper);
        }
    }
}
