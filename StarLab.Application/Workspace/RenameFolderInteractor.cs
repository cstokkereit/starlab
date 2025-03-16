using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that renames a folder in the workspace hierarchy.
    /// </summary>
    internal class RenameFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IRenameItemUseCase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RenameFolderInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public RenameFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the folder being renamed.</param>
        /// <param name="name">The new folder name.</param>
        public void Execute(WorkspaceDTO dto, string key, string name)
        {
            var workspace = new Workspace(dto);

            var folder = workspace.GetFolder(key);

            var type = folder is Project ? Resources.Project : Resources.Folder;

            if (WorkspaceInteractionHelper.IsValid(name))
            {
                var folders = folder is Project ? workspace.Projects : folder.Parent.Folders;

                if (IsValid(folders, name))
                {
                    workspace.RenameFolder(folder, name);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
                else
                {
                    throw new Exception(WorkspaceInteractionHelper.CreateTargetExistsMessage(key.Substring(key.LastIndexOf('/') + 1), name, type));
                }
            }
            else
            {
                throw new Exception(WorkspaceInteractionHelper.CreateInvalidNameMessage(name, type));
            }
        }

        /// <summary>
        /// Checks for the existance of an <see cref="IFolder"/> within the <see cref="IEnumerable{IFolder}"/> provided that has a name that matches the new folder name.
        /// </summary>
        /// <param name="folders">An <see cref="IEnumerable{IFolder}"/> containing the folders.</param>
        /// <param name="name">The new folder name.</param>
        /// <returns>true if there are no folders with matching names; false otherwise.</returns>
        private bool IsValid(IEnumerable<IFolder> folders, string name)
        {
            var valid = true;

            foreach (var folder in folders)
            {
                if (folder.Name == name)
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }
    }
}
