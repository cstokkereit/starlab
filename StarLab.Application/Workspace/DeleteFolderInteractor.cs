using AutoMapper;
using log4net;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that removes a folder from the workspace hierarchy.
    /// </summary>
    public class DeleteFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<DeleteFolderUseCaseArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DeleteFolderInteractor)); // The logger that will be used for writing log messages.

        // <summary>
        /// Initialises a new instance of the <see cref="DeleteFolderInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public DeleteFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="DeleteFolderUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(DeleteFolderUseCaseArgs args)
        {
            args.Workspace.ActiveDocument = string.Empty;

            var workspace = new Workspace(args.Workspace);

            var folder = workspace.GetFolder(args.Path);

            workspace.DeleteFolder(args.Path);

            OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
        }
    }
}
