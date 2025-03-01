using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that removes a project from the workspace hierarchy.
    /// </summary>
    internal class DeleteProjectInteractor : DeleteFolderInteractor, IDeleteItemUseCase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DeleteProjectInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public DeleteProjectInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the folder being deleted.</param>
        public override void Execute(WorkspaceDTO dto, string key)
        {
            dto.ActiveDocument = string.Empty;

            var workspace = new Workspace(dto);

            var project = workspace.GetProject(key);

            if (project.IsEmpty || ConfirmAction(string.Format(Resources.ProjectDeletionWarning, project.Name)))
            {
                workspace.DeleteProject(key);

                //OutputPort.DeleteDocuments(GetDocumentDTOs(dto, project));
                Mapper.Map(workspace, dto);
                OutputPort.UpdateWorkspace(dto);
            }
        }
    }
}
