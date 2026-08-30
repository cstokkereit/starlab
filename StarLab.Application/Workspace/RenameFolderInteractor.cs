using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that renames a folder in the workspace hierarchy.
    /// </summary>
    internal class RenameFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<RenameFolderUseCaseArgs>
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
        /// <param name="args">The <see cref="RenameFolderUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(RenameFolderUseCaseArgs args)
        {
            var workspace = new Workspace(args.Workspace);

            var folder = workspace.GetFolder(args.Path);

            var type = folder is Project ? Resources.Project : Resources.Folder;

            if (WorkspaceInteractionHelper.IsValid(args.Name))
            {
                var folders = folder is Project ? workspace.Projects : folder.Parent.Folders;

                if (IsValid(folders, args.Name))
                {
                    workspace.RenameFolder(folder, args.Name);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
                else
                {
                    throw new Exception(WorkspaceInteractionHelper.CreateCannotRenameItemMessage(args.Path.Substring(args.Path.LastIndexOf('/') + 1), args.Name, type));
                }
            }
            else
            {
                throw new Exception(WorkspaceInteractionHelper.CreateInvalidNameMessage(args.Name, type));
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
