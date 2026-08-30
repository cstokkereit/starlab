using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents;

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
        /// <param name="path">The path to the parent folder.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void AddFolder(IWorkspace workspace, string path)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));

            var interactor = Factory.CreateAddFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new AddFolderUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), path));
        }

        /// <summary>
        /// Executes the CopyAndPaste use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="source">The key that identifies the source document or folder.</param>
        /// <param name="destination">The key that identifies the destination document or folder.</param>
        public void CopyAndPaste(IWorkspace workspace, string source, string destination)
        {
            ArgumentException.ThrowIfNullOrEmpty(destination, nameof(destination));
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(source, nameof(source));
            
            // TODO UseCases should not show messages directly - this could be tricky to change

            var interactor = Factory.CreateCopyAndPasteUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new ClipboardUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), source, destination));
        }

        /// <summary>
        /// Executes the CutAndPaste use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="source">The key that identifies the source document or folder.</param>
        /// <param name="destination">The key that identifies the destination document or folder.</param>
        public void CutAndPaste(IWorkspace workspace, string source, string destination)
        {
            ArgumentException.ThrowIfNullOrEmpty(destination, nameof(destination));
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(source, nameof(source));

            // TODO UseCases should not show messages directly - this could be tricky to change

            var interactor = Factory.CreateCutAndPasteUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new ClipboardUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), source, destination));
        }

        /// <summary>
        /// Executes the DeleteDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="id">The ID of the document to be deleted.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteDocument(IWorkspace workspace, DocumentID id)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var interactor = Factory.CreateDeleteDocumentUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new DeleteDocumentUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), id.ToString()));
        }

        /// <summary>
        /// Executes the DeleteFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="path">The path to the folder to be deleted.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void DeleteFolder(IWorkspace workspace, string path)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));

            var interactor = Factory.CreateDeleteFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new DeleteFolderUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), path));
        }

        /// <summary>
        /// Executes the RenameDocument use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="id">The document ID.</param>
        /// <param name="name">The new name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameDocument(IWorkspace workspace, DocumentID id, string name)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentNullException.ThrowIfNull(id, nameof(id));

            var interactor = Factory.CreateRenameDocumentUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new RenameDocumentUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), id.ToString(), name));
        }

        /// <summary>
        /// Executes the RenameFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="path">The folder path.</param>
        /// <param name="name">The new name.</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void RenameFolder(IWorkspace workspace, string path, string name)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentException.ThrowIfNullOrEmpty(path, nameof(path));

            var interactor = Factory.CreateRenameFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(new RenameFolderUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), path, name));
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

            interactor.Execute(new RenameWorkspaceUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), name));
        }
    }
}
