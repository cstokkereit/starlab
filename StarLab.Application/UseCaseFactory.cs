using AutoMapper;
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
        private readonly ISerialisationProvider serialiser; // Used to serialise and deserialise model objects.

        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        /// <summary>
        /// Initialises a new instance of the <see cref="UseCaseFactory"/> class.
        /// </summary>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used for serialise and deserialisation of model objects.</param>
        public UseCaseFactory(IMapper mapper, ISerialisationProvider serialiser)
        {
            this.serialiser = serialiser;
            this.mapper = mapper;
        }

        /// <summary>
        /// Creates a use case interactor that adds a document to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, DocumentDTO}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, DocumentDTO> CreateAddDocumentUseCase(IAddDocumentOutputPort outputPort)
        {
            return new AddDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string> CreateAddFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that adds a project to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, ProjectDTO}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, ProjectDTO> CreateAddProjectUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddProjectInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string> CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string> CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort)
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
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string, string> CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string, string> CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string> CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort)
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
        /// Creates a use case interactor that copies a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <param name="operation">A <see cref="ClipboardOperations"/> enum that specifies the clipboard operation.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string> CreateUseCase(IWorkspaceOutputPort outputPort, ClipboardOperations operation)
        {
            return new ClipboardInteractor(outputPort, mapper, operation);
        }

        /// <summary>
        /// Creates a use case interactor that updates a chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ChartDTO}"/> that implements the use case.</returns>
        public IUseCase<ChartDTO> CreateUpdateChartUseCase(IChartOutputPort outputPort)
        {
            return new UpdateChartInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that updates a document in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string, ChartDTO}"/> that implements the use case.</returns>
        public IUseCase<WorkspaceDTO, string, ChartDTO> CreateUpdateDocumentUseCase(IApplicationOutputPort outputPort)
        {
            return new UpdateDocumentInteractor(outputPort, mapper);
        }
    }
}
