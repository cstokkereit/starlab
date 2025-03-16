using AutoMapper;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class AddProjectInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IAddProjectUseCase
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
            var workspace = new Workspace(dtoWorkspace);

            if (WorkspaceInteractionHelper.IsValid(dtoProject.Name))
            {
                try
                {
                    var project = new Project(dtoProject, workspace);

                    workspace.AddProject(project);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
                catch (InvalidOperationException)
                {
                    //OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DocumentExistsWarning, dtoDocument.Name), InteractionType.Error, InteractionResponses.OK);
                }
            }
            else
            {
                //OutputPort.ShowMessage(Resources.StarLab, CreateInvalidNameMessage(dtoDocument.Name, Resources.Document), InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
