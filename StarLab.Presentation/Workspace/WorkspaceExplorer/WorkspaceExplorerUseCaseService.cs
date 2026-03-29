using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// A service that executes the use cases that implement the workspace explorer functionality.
    /// </summary>
    public class WorkspaceExplorerUseCaseService : UseCaseService, IWorkspaceExplorerUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceExplorerUseCaseService"/>.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public WorkspaceExplorerUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Executes the AddFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the parent folder.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddFolder(IWorkspace workspace, string key)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateAddFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key);
        }

        /// <summary>
        /// Executes the Copy use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the document or folder to be copied.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Copy(IWorkspace workspace, string key)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>(), ClipboardOperations.Copy);

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key);
        }

        /// <summary>
        /// Executes the Cut use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the document or folder to be cut.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Cut(IWorkspace workspace, string key)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>(), ClipboardOperations.Cut);

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key);
        }

        /// <summary>
        /// Executes the DeleteDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="id">The ID of the document to be deleted.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteDocument(IWorkspace workspace, string id)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(id, nameof(id));

            var interactor = Factory.CreateDeleteDocumentUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), id);
        }

        /// <summary>
        /// Executes the DeleteFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the folder to be deleted.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteFolder(IWorkspace workspace, string key)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateDeleteFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key);
        }

        /// <summary>
        /// Executes the Paste use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the destination for the document or folder.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void Paste(IWorkspace workspace, string key)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>(), ClipboardOperations.Paste);

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key);
        }

        /// <summary>
        /// Executes the RenameDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameDocument(IWorkspace workspace, string key, string name)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateRenameDocumentUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key, name);
        }

        /// <summary>
        /// Executes the RenameFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The node key.</param>
        /// <param name="name">The new name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameFolder(IWorkspace workspace, string key, string name)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateRenameFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key, name);
        }

        /// <summary>
        /// Executes the RenameWorkspace use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="name">The new name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameWorkspace(IWorkspace workspace, string name)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

            var interactor = Factory.CreateRenameWorkspaceUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), name);
        }
    }
}
