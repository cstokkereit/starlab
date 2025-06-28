using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A parameterised command that performs a cut and paste operation within the workspace hierarchy.
    /// </summary>
    internal class CutAndPasteCommand : ParameterisedCommand<Workspace, string>
    {
        private readonly IWorkspaceOutputPort outputPort; // Updates the UI with the results of the cut and paste operation.

        private readonly IMapper mapper; // Copies data from model objects to data transfer objects and vice versa.

        private readonly string key; // The key that identifies the document or folder being moved.

        /// <summary>
        /// Initialises a new instance of the <see cref="CutAndPasteCommand"/> class.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/> state.</param>
        /// <param name="key">The key that identifies the document or folder being moved.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the command.</param>
        public CutAndPasteCommand(Workspace workspace, string key, IMapper mapper, IWorkspaceOutputPort outputPort)
            : base(workspace)
        {
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.key = key ?? throw new ArgumentNullException(nameof(key));

            // Move output port calls inside the command, do away with the exceptions, create additional functionality and tests for copyandpaste.
            // TODO - Reduce transparency of cut folders prior to paste, handle documents ie create new instances when copied etc.
        }

        /// <summary>
        /// Executes the command with the specified destination path.
        /// </summary>
        /// <param name="destination">The path to the folder that is the destination for the cut document or folder.</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Execute(string destination)
        {
            if (Receiver.IsFolder(key) || Receiver.IsProject(key))
            {
                CutAndPasteFolder(destination);
            }
            else if (Receiver.IsDocument(key))
            {
                CutAndPasteDocument(destination);
            }
            else
            {
                throw new InvalidOperationException(string.Format(Constants.InvalidOperationMessage, key));
            }
        }

        /// <summary>
        /// Moves the <see cref="Document"/> provided from its current location in the workspace hierarchy to the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> that is being moved.</param>
        /// <param name="folder">The <see cref="IFolder"/> that is the destination for the cut document.</param>
        /// <param name="replace"><see cref="true"/> to replace the document in the destination folder with the cut document; <see cref="false"/> otherwise.</param>
        private void CutAndPasteDocument(Document document, IFolder folder, bool replace)
        {
            Receiver.DeleteDocument(document);

            var id = GetDocumentID(folder, document.Name);

            if (!string.IsNullOrEmpty(id))
            {
                if (replace)
                {
                    Receiver.DeleteDocument(id);
                    document.SetFolder(folder);
                    Receiver.AddDocument(document);
                }
            }
            else
            {
                document.SetFolder(folder);
                Receiver.AddDocument(document);
            }
        }

        /// <summary>
        /// Moves the <see cref="Document"/> provided from its current location in the workspace hierarchy to the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="document">The <see cref="Document"/> that is being moved.</param>
        /// <param name="folder">The <see cref="IFolder"/> that is the destination for the cut document.</param>
        private void CutAndPasteDocument(Document document, IFolder folder)
        {
            Receiver.DeleteDocument(document);
            document.SetFolder(folder);
            Receiver.AddDocument(document);

            outputPort.UpdateWorkspace(mapper.Map<WorkspaceDTO>(Receiver));
        }

        /// <summary>
        /// Moves the cut document from its current location in the workspace hierarchy to the specified destination folder.
        /// </summary>
        /// <param name="destination">The path to the folder that is the destination for the cut document.</param>
        private void CutAndPasteDocument(string destination)
        {
            var folder = Receiver.GetFolder(destination);
            var document = Receiver.GetDocument(key);
            
            if (document.Path == folder.Path)
            {
                outputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DestinationSameAsSource, document.Name), InteractionType.Error, InteractionResponses.OK);
            }
            else if (DocumentNameExists(folder, document.Name))
            {
                var result = outputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DocumentAlreadyExists, document.Name), InteractionType.Error, InteractionResponses.YesNoCancel);

                if (result != InteractionResult.Cancel)
                {
                    CutAndPasteDocument(document, folder, result == InteractionResult.Yes);
                    outputPort.UpdateWorkspace(mapper.Map<WorkspaceDTO>(Receiver));
                }
            }
            else
            {
                CutAndPasteDocument(document, folder);
            }
        }

        /// <summary>
        /// Moves the <see cref="IFolder"/> provided from its current location in the workspace hierarchy to the specified destination folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that is being moved.</param>
        /// <param name="destination">The <see cref="IFolder"/> that is the destination for the cut folder.</param>
        /// <param name="replace"><see cref="true"/> to replace the contents of existing folders with the contents of the cut folder in the event of name collisions; <see cref="false"/> otherwise.</param>
        private void CutAndPasteFolder(IFolder folder, IFolder destination, bool replace)
        {
            var documents = new List<Document>(folder.Documents);
            var target = GetFolder(destination, folder.Name);

            foreach (var document in documents)
            {
                CutAndPasteDocument(document, target, replace);
            }

            var children = new List<IFolder>(folder.Folders);

            foreach (var child in children)
            {
                CutAndPasteFolder(child, target, replace);
            }

            Receiver.DeleteFolder(folder);
        }

        /// <summary>
        /// Moves the <see cref="IFolder"/> provided from its current location in the workspace hierarchy to the specified destination folder.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that is being moved.</param>
        /// <param name="destination">The path to the folder that is the destination for the cut folder.</param>
        private void CutAndPasteFolder(IFolder folder, string destination)
        {
            Receiver.DeleteFolder(key);

            Receiver.AddFolder(folder, destination);

            outputPort.UpdateWorkspace(mapper.Map<WorkspaceDTO>(Receiver));
        }

        /// <summary>
        /// Moves the cut folder to the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="destination">The path to the folder that is the destination for the cut folder.</param>
        private void CutAndPasteFolder(string destination)
        {
            var folder = Receiver.GetFolder(key);

            if (destination == folder.Path)
            {
                outputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DestinationSameAsSource, folder.Name), InteractionType.Error, InteractionResponses.OK);
            }
            else if (FolderNameExists(destination, folder.Name))
            {
                var result = outputPort.ShowMessage(Resources.StarLab, string.Format(Resources.FolderAlreadyExists, folder.Name), InteractionType.Error, InteractionResponses.YesNoCancel);

                if (result != InteractionResult.Cancel)
                {
                    CutAndPasteFolder(folder, Receiver.GetFolder(destination), result == InteractionResult.Yes);
                    outputPort.UpdateWorkspace(mapper.Map<WorkspaceDTO>(Receiver));
                }
            }
            else
            {
                CutAndPasteFolder(folder, destination);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="IFolder"> provided contains a document with the specified name.
        /// </summary>
        /// <param name="folder">The folder being searched.</param>
        /// <param name="name">The document name.</param>
        /// <returns><see cref="true"/> if the <see cref="IFolder"> provided contains a document with the specified name; <see cref="false"/> otherwise.</returns>
        private bool DocumentNameExists(IFolder folder, string name)
        {
            foreach (var document in folder.Documents)
            {
                if (document.Name == name) return true;
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified folder contains a child folder with the specified name.
        /// </summary>
        /// <param name="destination">The path to the folder being searched.</param>
        /// <param name="name">The name of the child folder.</param>
        /// <returns><see cref="true"/> if the specified folder contains a child folder with the specified name; <see cref="false"/> otherwise.</returns>
        private bool FolderNameExists(string destination, string name)
        {
            var parent = Receiver.GetFolder(destination);

            foreach (var child in parent.Folders)
            {
                if (child.Name == name) return true;
            }

            return false;
        }

        /// <summary>
        /// Gets the ID of the specified <see cref="Document"/>.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that contains the document.</param>
        /// <param name="name">The name of the document.</param>
        /// <returns>The ID of the specified document.</returns>
        private string GetDocumentID(IFolder folder, string name)
        {
            foreach (var document in folder.Documents)
            {
                if (document.Name == name) return document.ID;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the specified <see cref="IFolder"/>. If the folder does not exist it will be created.
        /// </summary>
        /// <param name="parent">The <see cref="IFolder"/> that contains the required folder.</param>
        /// <param name="name">The name of the required folder.</param>
        /// <returns>The required <see cref="IFolder"/>.</returns>
        private IFolder GetFolder(IFolder parent, string name)
        {
            IFolder folder;

            if (parent.ContainsFolder(name))
            {
                folder = Receiver.GetFolder($"{parent.Path}/{name}");
            }
            else
            {
                folder = Receiver.AddFolder(name, parent);
            }

            return folder;
        }
    }
}
