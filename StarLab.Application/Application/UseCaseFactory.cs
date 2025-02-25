using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

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
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IAddDocumentUseCase"/> that implements the use case.</returns>
        public IAddDocumentUseCase CreateAddDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IAddFolderUseCase"/> that implements the use case.</returns>
        public IAddFolderUseCase CreateAddFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IDeleteItemUseCase"/> that implements the use case.</returns>
        public IDeleteItemUseCase CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IDeleteItemUseCase"/> that implements the use case.</returns>
        public IDeleteItemUseCase CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that deletes a project from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IDeleteItemUseCase"/> that implements the use case.</returns>
        public IDeleteItemUseCase CreateDeleteProjectUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteProjectInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that loads a workspace from a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IOpenWorkspaceUseCase"/> that implements the use case.</returns>
        public IOpenWorkspaceUseCase CreateOpenWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new OpenWorkspaceInteractor(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames a document in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IRenameItemUseCase"/> that implements the use case.</returns>
        public IRenameItemUseCase CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameDocumentInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IRenameItemUseCase"/> that implements the use case.</returns>
        public IRenameItemUseCase CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameFolderInteractor(outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that renames the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IRenameWorkspaceUseCase"/> that implements the use case.</returns>
        public IRenameWorkspaceUseCase CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameWorkspaceInteractor(serialiser, outputPort, mapper);
        }

        /// <summary>
        /// Creates a use case interactor that saves the current workspace to a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="ISaveWorkspaceUseCase"/> that implements the use case.</returns>
        public ISaveWorkspaceUseCase CreateSaveWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new SaveWorkspaceInteractor(serialiser, outputPort, mapper);
        }
    }
}
