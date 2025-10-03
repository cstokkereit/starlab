using AutoMapper;
using log4net;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that removes a folder from the workspace hierarchy.
    /// </summary>
    public class DeleteFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<WorkspaceDTO, string>
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
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the folder being deleted.</param>
        public void Execute(WorkspaceDTO dto, string key)
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

            dto.ActiveDocument = string.Empty;

            try
            {
                var workspace = new Workspace(dto);

                var folder = workspace.GetFolder(key);

                if (folder.IsEmpty || ConfirmAction(GetConfirmationMessage(folder)))
                {
                    workspace.DeleteFolder(key);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Returns a message requesting confirmation of the deletion of the specified project or folder.
        /// </summary>
        /// <param name="target">The <see cref="IFolder"/> being deleted.</param>
        /// <returns>The required confirmation message.</returns>
        private static string GetConfirmationMessage(IFolder target)
        {
            return string.Format(Resources.FolderDeletionWarning, (target is Project ? Resources.Project : Resources.Folder).ToLower(), target.Name);
        }
    }
}
