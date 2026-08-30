using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that renames the workspace.
    /// </summary>
    internal class RenameWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<RenameWorkspaceUseCaseArgs>
    {
        private readonly ISerialisationProvider serialiser; // Used to serialise the workspace to a file.

        /// <summary>
        /// Initialises a new instance of the <see cref="RenameWorkspaceInteractor"/> class.
        /// </summary>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used to serialise the <see cref="WorkspaceDTO"/>.</param>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public RenameWorkspaceInteractor(ISerialisationProvider serialiser, IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialiser = serialiser;
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="RenameWorkspaceUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(RenameWorkspaceUseCaseArgs args)
        {
            var filename = args.Workspace.FileName;

            if (WorkspaceInteractionHelper.IsValid(args.Name) && !string.IsNullOrEmpty(filename))
            {
                args.Workspace.FileName = Path.ChangeExtension(Path.Join(Path.GetDirectoryName(filename), args.Name), Constants.WorkspaceExtension);
               
                if (!File.Exists(args.Workspace.FileName))
                {
                    try
                    {
                        serialiser.SerialiseWorkspace(args.Workspace, args.Workspace.FileName);
                        File.Delete(filename);
                    }
                    catch (Exception e)
                    {
                        OutputPort.ShowMessage(Resources.StarLab, e.Message, InteractionType.Error, InteractionResponses.OK);
                        args.Workspace.FileName = filename;
                    }
                    finally
                    {
                        OutputPort.UpdateWorkspace(args.Workspace);
                    }
                }
                else
                {
                    throw new Exception(WorkspaceInteractionHelper.CreateCannotRenameItemMessage(Path.GetFileName(filename), Path.GetFileName(args.Workspace.FileName), Resources.Workspace));
                }
            }
            else
            {
                throw new Exception(WorkspaceInteractionHelper.CreateInvalidNameMessage(args.Name, Resources.Workspace));
            }
        }
    }
}
