using AutoMapper;
using StarLab.Application.Workspace.Documents;
using Stratosoft.Commands;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A parameterised command that performs a copy and paste operation within the workspace hierarchy.
    /// </summary>
    internal class CopyAndPasteCommand : ParameterisedCommand<Workspace, string>
    {
        private readonly IWorkspaceOutputPort outputPort;

        private readonly IMapper mapper;

        private readonly string key; // The key that identifies the document or folder being copied.

        /// <summary>
        /// Initialises a new instance of the <see cref="CopyAndPasteCommand"/> class.
        /// </summary>
        /// <param name="workspace">The current <see cref="Workspace"/> state.</param>
        /// <param name="key">The key that identifies the document or folder being copied.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the command.</param>
        public CopyAndPasteCommand(Workspace workspace, string key, IMapper mapper, IWorkspaceOutputPort outputPort)
            : base(workspace)
        {
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.key = key ?? throw new ArgumentNullException(nameof(key));
        }

        /// <summary>
        /// Executes the command with the specified destination path.
        /// </summary>
        /// <param name="destination">The path to the folder that is the destination for the copied document or folder.</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Execute(string destination)
        {
            if (Receiver.IsFolder(key) || Receiver.IsProject(key))
            {
                CopyAndPasteFolder(destination);
            }
            else if (Receiver.IsDocument(key))
            {
                CopyAndPasteDocument(destination);
            }
            else
            {
                throw new InvalidOperationException(string.Format(Constants.InvalidOperationMessage, key));
            }
        }

        /// <summary>
        /// Copies the specified document to the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="path">The path to the folder that is the destination for the copied document.</param>
        private void CopyAndPasteDocument(string path)
        {
            var folder = Receiver.GetFolder(path);

            var document = Receiver.GetDocument(key);

            if (document.Path == folder.Path)
            {
                Receiver.AddDocument(new Document(document, GetDocumentName(document.Name, folder), folder));
            }
            else
            {
                Receiver.AddDocument(new Document(document, folder));
            }

            outputPort.UpdateWorkspace(mapper.Map<WorkspaceDTO>(Receiver));
        }

        /// <summary>
        /// Copies the specified folder to the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="path">The path to the folder that is the destination for the copied folder.</param>
        private void CopyAndPasteFolder(string path)
        {
            var folder = Receiver.GetFolder(key);
            var destination = Receiver.GetFolder(path);

            var name = GetFolderName(folder.Name, destination);

            CopyFolder(folder, destination, name);

            outputPort.UpdateWorkspace(mapper.Map<WorkspaceDTO>(Receiver));
        }

        /// <summary>
        /// Copies the contents of the specified folder to the specified location within the workspace hierarchy.
        /// </summary>
        /// <param name="source">The <see cref="IFolder"/> being copied.</param>
        /// <param name="destination">The new parent <see cref="IFolder"/>.</param>
        /// <param name="name">The new folder name.</param>
        private void CopyFolder(IFolder source, IFolder destination, string name)
        {
            var copy = Receiver.AddFolder(name, destination);

            foreach (var document in source.Documents)
            {
                Receiver.AddDocument(new Document(document, copy));
            }

            foreach (var folder in source.Folders)
            {
                CopyFolder(folder, copy, folder.Name);
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
            bool found = false;

            foreach (var doc in destination.Documents)
            {
                if (doc.Name == name)
                {
                    name = $"{name} - Copy";
                    found = true;
                    break;
                }
            }

            if (found) name = GetDocumentName(name, destination);

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
            bool found = false;

            foreach (var folder in destination.Folders)
            {
                if (folder.Name == name)
                {
                    name =  $"{name} - Copy";
                    found = true;
                    break;
                }
            }

            if (found) name = GetFolderName(name, destination);

            return name;
        }
    }
}
