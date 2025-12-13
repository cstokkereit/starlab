using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that renames the workspace.
    /// </summary>
    internal class RenameWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<WorkspaceDTO, string>
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
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="name">The new workspace name.</param>
        public void Execute(WorkspaceDTO dto, string name)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            var filename = dto.FileName;

            if (WorkspaceInteractionHelper.IsValid(name) && !string.IsNullOrEmpty(filename))
            {
                dto.FileName = Path.ChangeExtension(Path.Join(Path.GetDirectoryName(filename), name), Constants.WorkspaceExtension);
               
                if (!File.Exists(dto.FileName))
                {
                    try
                    {
                        serialiser.SerialiseWorkspace(dto, dto.FileName);
                        File.Delete(filename);
                    }
                    catch (Exception e)
                    {
                        OutputPort.ShowMessage(Resources.StarLab, e.Message, InteractionType.Error, InteractionResponses.OK);
                        dto.FileName = filename;
                    }
                    finally
                    {
                        OutputPort.UpdateWorkspace(dto);
                    }
                }
                else
                {
                    throw new Exception(WorkspaceInteractionHelper.CreateCannotRenameItemMessage(Path.GetFileName(filename), Path.GetFileName(dto.FileName), Resources.Workspace)); // TODO - Can these be changed to show messages
                }
            }
            else
            {
                throw new Exception(WorkspaceInteractionHelper.CreateInvalidNameMessage(name, Resources.Workspace));
            }
        }
    }
}
