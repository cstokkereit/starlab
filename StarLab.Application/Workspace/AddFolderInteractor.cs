using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that adds a folder at a specified location within the workspace hierarchy.
    /// </summary>
    internal class AddFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<AddFolderUseCaseArgs>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddFolderInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public AddFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="AddFolderUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(AddFolderUseCaseArgs args)
        {
            var workspace = new Workspace(args.Workspace);
            var parent = workspace.GetFolder(args.Path);
            var name = GetName(parent);

            var folder = workspace.AddFolder(name, parent);

            OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));

            OutputPort.RenameFolder(folder.Path);
        }

        /// <summary>
        /// Gets the default name for a new child of the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="parent">The parent <see cref="IFolder"/>.</param>
        /// <returns>The default name for the new <see cref="IFolder"/>.</returns>
        private string GetName(IFolder parent)
        {
            var name = Resources.DefaultFolderName;
            var names = GetNames(parent.Folders);
            var n = 1;

            while (names.Contains(name))
            {
                name = Resources.DefaultFolderName + n++;
            }

            return name;
        }

        /// <summary>
        /// Gets the names of all of the folders in the <see cref="IEnumerable{IFolder}"/> provided.
        /// </summary>
        /// <param name="folders">An <see cref="IEnumerable{IFolder}"/> containing the folders whose names we require.</param>
        /// <returns>A <see cref="List{string}"/> containg the names of the folders.</returns>
        private List<string> GetNames(IEnumerable<IFolder> folders)
        {
            var names = new List<string>();

            foreach (var folder in folders)
            {
                names.Add(folder.Name);
            }

            return names;
        }
    }
}
