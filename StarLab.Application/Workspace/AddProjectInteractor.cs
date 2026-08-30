using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that adds a project to the workspace.
    /// </summary>
    internal class AddProjectInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<AddProjectUseCaseArgs>
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
        /// <param name="args">The <see cref="AddProjectUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(AddProjectUseCaseArgs args)
        {
            var workspace = new Workspace(args.Workspace);

            if (WorkspaceInteractionHelper.IsValid(args.Project.Name))
            {
                try
                {
                    var project = new Project(args.Project, workspace);

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
                OutputPort.ShowMessage(Resources.StarLab, WorkspaceInteractionHelper.CreateInvalidNameMessage(args.Project.Name, Resources.Project), InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
