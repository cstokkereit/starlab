using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that adds a project to the workspace.
    /// </summary>
    internal class AddProjectInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<WorkspaceDTO, ProjectDTO>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddProjectInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="INewProjectOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public AddProjectInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="dtoProject">A <see cref="ProjectDTO"/> that defines the project being added.</param>
        public void Execute(WorkspaceDTO dtoWorkspace, ProjectDTO dtoProject)
        {
            ArgumentNullException.ThrowIfNull(nameof(dtoWorkspace));
            ArgumentNullException.ThrowIfNull(nameof(dtoProject));

            var workspace = new Workspace(dtoWorkspace);

            if (WorkspaceInteractionHelper.IsValid(dtoProject.Name))
            {
                try
                {
                    var project = new Project(dtoProject, workspace);

                    workspace.AddProject(project);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
                catch (NameExistsException e)
                {
                    OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.NameAlreadyExists, e.Target, e.Name), InteractionType.Error, InteractionResponses.OK);
                }
            }
            else
            {
                OutputPort.ShowMessage(Resources.StarLab, WorkspaceInteractionHelper.CreateInvalidNameMessage(dtoProject.Name, Resources.Project), InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
