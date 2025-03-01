using AutoMapper;
using StarLab.Shared.Properties;
using System.Windows.Forms;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that removes a folder from the workspace hierarchy.
    /// </summary>
    internal class DeleteFolderInteractor : WorkspaceInteractor, IDeleteItemUseCase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DeleteFolderInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public DeleteFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the folder being deleted.</param>
        public virtual void Execute(WorkspaceDTO dto, string key)
        {
            dto.ActiveDocument = string.Empty;

            var workspace = new Workspace(dto);
            var folder = workspace.GetFolder(key);

            if (folder.IsEmpty || ConfirmAction(string.Format(Resources.FolderDeletionWarning, folder.Name)))
            {
                var ids = GetDocumentIds(folder);

                workspace.DeleteFolder(key);

                foreach (var id in ids)
                {
                    OutputPort.RemoveDocument(id);
                }

                OutputPort.UpdateWorkspace(Mapper.Map(workspace, dto));
            }
        }

        /// <summary>
        /// Recursively populates the <see cref="List{string}"/> provided with the IDs of all documents owned by the parent <see cref="IFolder"/> provided and its child folders.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that contains the documents.</param>
        /// <param name="documents">An <see cref="List{string}"/> that will be populated with the IDs of the documents.</param>
        private static void GetDocumentIds(IFolder folder, List<string> documents)
        {
            foreach (var document in folder.Documents)
            {
                documents.Add(document.ID);
            }

            foreach (var child in folder.Folders)
            {
                GetDocumentIds(child, documents);
            }
        }

        /// <summary>
        /// Gets the IDs of all documents owned by the parent <see cref="IFolder"/> provided and its child folders.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that contains the documents.</param>
        /// <returns>An <see cref="IEnumerable{string}"/> containing the required document IDs.</returns>
        private static IEnumerable<string> GetDocumentIds(IFolder folder)
        {
            var ids = new List<string>();
            GetDocumentIds(folder, ids);
            return ids;
        }
    }
}
