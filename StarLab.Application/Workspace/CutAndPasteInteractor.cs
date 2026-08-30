using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that copies a folder at a specified location within the workspace hierarchy.
    /// </summary>
    internal class CutAndPasteInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<ClipboardUseCaseArgs>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="CutAndPasteInteractor"/> class.
        /// </summary>
        /// <param name="OutputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="Mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public CutAndPasteInteractor(IWorkspaceOutputPort OutputPort, IMapper Mapper)
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
                CutAndPaste(workspace, args.Source, workspace.GetFolder(args.Destination));
            }
            else
            {
                throw new InvalidOperationException(string.Format(Resources.InvalidOperation, args.Destination));
            }
            
            OutputPort.ClearClipboard();
        }

        /// <summary>
        /// Mpves the specified source folder or document to the specified destination folder within the workspace hierarchy.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="source">The key that identifies the document or folder being moved.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        /// <exception cref="InvalidOperationException"></exception>
        private void CutAndPaste(Workspace workspace, string source, IFolder destination)
        {
            if (workspace.IsFolder(source) || workspace.IsProject(source))
            {
                UpdateWorkspace(workspace, workspace.GetFolder(source), destination);
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
        }

        /// <summary>
        /// Moves the <see cref="Document"/> provided from its current location in the workspace hierarchy to the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="document">The <see cref="Document"/> that is being moved.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        /// <param name="replace">true to replace the document in the destination folder with the document being moved; false otherwise.</param>
        private void UpdateWorkspace(Workspace workspace, Document document, IFolder destination, bool replace)
        {
            workspace.DeleteDocument(document);

            if (DocumentNameExists(destination, document.Name))
            {
                if (replace)
                {
                    workspace.DeleteDocument(GetDocumentID(destination, document.Name));
                    document.SetFolder(destination);
                    workspace.AddDocument(document);
                }
            }
            else
            {
                document.SetFolder(destination);
                workspace.AddDocument(document);
            }
        }

        /// <summary>
        /// Moves the <see cref="Document"/> provided from its current location in the workspace hierarchy to the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="document">The <see cref="Document"/> that is being moved.</param>
        /// <param name="destination">The destination <see cref="IFolder"/>.</param>
        private void UpdateWorkspace(Workspace workspace, Document document, IFolder destination)
        {
            if (document.Path == destination.Path)
            {
                OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DestinationSameAsSource, document.Name), InteractionType.Error, InteractionResponses.OK);
            }
            else if (DocumentNameExists(destination, document.Name))
            {
                var result = OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DocumentAlreadyExists, document.Name), InteractionType.Error, InteractionResponses.YesNoCancel);

                if (result != InteractionResult.Cancel)
                {
                    UpdateWorkspace(workspace, document, destination, result == InteractionResult.Yes);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
            }
            else
            {
                workspace.DeleteDocument(document);
                document.SetFolder(destination);
                workspace.AddDocument(document);

                OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
            }
        }

        /// <summary>
        /// Moves the <see cref="IFolder"/> provided from its current location in the workspace hierarchy to the specified destination folder.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="folder">The <see cref="IFolder"/> that is being moved.</param>
        /// <param name="destination">The <see cref="IFolder"/> that is the destination for the cut folder.</param>
        /// <param name="replace">true to replace the contents of existing folders with the contents of the cut folder in the event of name collisions; false otherwise.</param>
        private void UpdateWorkspace(Workspace workspace, IFolder folder, IFolder destination, bool replace)
        {
            var documents = new List<Document>(folder.Documents);
            var target = GetFolder(workspace, destination, folder.Name);

            foreach (var document in documents)
            {
                UpdateWorkspace(workspace, document, target, replace);
            }

            var children = new List<IFolder>(folder.Folders);

            foreach (var child in children)
            {
                UpdateWorkspace(workspace, child, target, replace);
            }

            workspace.DeleteFolder(folder);
        }

        /// <summary>
        /// Moves the <see cref="IFolder"/> provided to the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/>.</param>
        /// <param name="folder">The <see cref="IFolder"/> that is being moved.</param>
        /// <param name="destination">The <see cref="IFolder"/> that is the destination for the cut folder.</param>
        private void UpdateWorkspace(Workspace workspace, IFolder folder, IFolder destination)
        {
            if (destination.Path == folder.Path)
            {
                OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DestinationSameAsSource, folder.Name), InteractionType.Error, InteractionResponses.OK);
            }
            else if (FolderNameExists(destination, folder.Name))
            {
                var result = OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.FolderAlreadyExists, folder.Name), InteractionType.Error, InteractionResponses.YesNoCancel);

                if (result != InteractionResult.Cancel)
                {
                    UpdateWorkspace(workspace, folder, destination, result == InteractionResult.Yes);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
            }
            else
            {
                workspace.DeleteFolder(folder);
                workspace.AddFolder(folder, destination.Path);

                OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
            }
        }

        /// <summary>
        /// Determines whether the <see cref="IFolder"> provided contains a document with the specified name.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being searched.</param>
        /// <param name="name">The name of the document.</param>
        /// <returns>true if the folder contains a document with the specified name; false otherwise.</returns>
        private static bool DocumentNameExists(IFolder folder, string name)
        {
            foreach (var document in folder.Documents)
            {
                if (document.Name == name) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the <see cref="IFolder"> provided contains a child folder with the specified name.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> being searched.</param>
        /// <param name="name">The name of the child folder.</param>
        /// <returns>true if the folder contains a child folder with the specified name; false otherwise.</returns>
        private static bool FolderNameExists(IFolder folder, string name)
        {
            foreach (var child in folder.Folders)
            {
                if (child.Name == name) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the ID of the document with the specified name and containing folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that contains the document.</param>
        /// <param name="name">The name of the document.</param>
        /// <returns>The ID of the document with the specified name.</returns>
        private static DocumentID GetDocumentID(IFolder folder, string name)
        {
            foreach (var document in folder.Documents)
            {
                if (document.Name == name) return document.ID;
            }

            throw new ArgumentException(string.Format(Resources.DocumentNotFound, name, folder.Path));
        }

        /// <summary>
        /// Gets the specified <see cref="IFolder"/>. If the folder does not exist it will be created.
        /// </summary>
        /// <param name="parent">The <see cref="IFolder"/> that contains the required folder.</param>
        /// <param name="name">The name of the required folder.</param>
        /// <returns>The specified <see cref="IFolder"/>.</returns>
        private static IFolder GetFolder(Workspace workspace, IFolder parent, string name)
        {
            IFolder folder;

            if (parent.ContainsFolder(name))
            {
                folder = workspace.GetFolder($"{parent.Path}/{name}");
            }
            else
            {
                folder = workspace.AddFolder(name, parent);
            }

            return folder;
        }
    }
}
