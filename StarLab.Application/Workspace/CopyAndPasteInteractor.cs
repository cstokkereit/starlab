using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that copies a folder at a specified location within the workspace hierarchy.
    /// </summary>
    internal class CopyAndPasteInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<ClipboardUseCaseArgs>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CutAndPasteInteractor"/> class.
        /// </summary>
        /// <param name="OutputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="Mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public CopyAndPasteInteractor(IWorkspaceOutputPort OutputPort, IMapper Mapper)
            : base(OutputPort, Mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="ClipboardUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        /// <exception cref="InvalidOperationException"></exception>
        public void Execute(ClipboardUseCaseArgs args)
        {
            var workspace = new Workspace(args.Workspace);

            if (workspace.IsFolder(args.Destination) || workspace.IsProject(args.Destination))
            {
                CopyAndPaste(workspace, args.Source, workspace.GetFolder(args.Destination));
            }
            else
            {
                throw new InvalidOperationException(string.Format(Resources.InvalidOperation, args.Destination));
            }

            OutputPort.ClearClipboard();
        }

        /// <summary>
        /// Copies the specified source folder or document to the specified destination folder within the workspace hierarchy.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="source">The key that identifies the document or folder being copied.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void CopyAndPaste(Workspace workspace, string source, IFolder destination)
        {
            if (workspace.IsFolder(source) || workspace.IsProject(source))
            {
                var folder = workspace.GetFolder(source);

                var name = GetFolderName(folder.Name, destination);

                UpdateWorkspace(workspace, folder, destination, name);
            }
            else
            {
                var id = new DocumentID(source);

                if (workspace.IsDocument(id))
                {
                    UpdateWorkspace(workspace, workspace.GetDocument(id), destination);
                }
                else
                {
                    throw new InvalidOperationException(string.Format(Resources.InvalidOperation, source));
                }
            }

            OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
        }

        /// <summary>
        /// Copies the specified document to the specified folder within the workspace hierarchy.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="source">The source <see cref="Document"/>.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        private void UpdateWorkspace(Workspace workspace, Document source, IFolder destination)
        {
            if (source.Path == destination.Path)
            {
                workspace.AddDocument(new Document(source, GetDocumentName(source.Name, destination), destination));
            }
            else
            {
                workspace.AddDocument(new Document(source, destination));
            }
        }

        /// <summary>
        /// Copies the contents of the specified source folder to the specified destination folder within the workspace hierarchy.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="source">The source <see cref="IFolder"/>.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        /// <param name="name">The new folder name.</param>
        private void UpdateWorkspace(Workspace workspace, IFolder source, IFolder destination, string name)
        {
            var copy = workspace.AddFolder(name, destination);

            foreach (var document in source.Documents)
            {
                workspace.AddDocument(new Document(document, copy));
            }

            foreach (var folder in source.Folders)
            {
                UpdateWorkspace(workspace, folder, copy, folder.Name);
            }
        }

        /// <summary>
        /// Generates a document name that is guaranteed to be unique within the destination folder. The original name will be returned if there are no naming collisions.
        /// </summary>
        /// <param name="name">The current name of the document.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        /// <returns>A document name that is guaranteed to be unique within the destination folder.</returns>
        private string GetDocumentName(string name, IFolder destination)
        {
            foreach (var doc in destination.Documents)
            {
                if (doc.Name == name)
                {
                    name = $"{name} - Copy";
                    break;
                }
            }

            var seed = name;
            int index = 2;

            while (destination.ContainsDocument(name))
            {
                name = $"{seed} ({index++})";
            }
            ;

            return name;
        }

        /// <summary>
        /// Generates a folder name that is guaranteed to be unique within the destination folder. The original name will be returned if there are no naming collisions.
        /// </summary>
        /// <param name="name">The current name of the folder.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        /// <returns>A document name that is guaranteed to be unique within the destination folder.</returns>
        private string GetFolderName(string name, IFolder destination)
        {
            foreach (var folder in destination.Folders)
            {
                if (folder.Name == name)
                {
                    name = $"{name} - Copy";
                    break;
                }
            }

            var seed = name;
            int index = 2;

            while (destination.ContainsFolder(name))
            {
                name = $"{seed} ({index++})";
            }
            ;

            return name;
        }
    }
}
