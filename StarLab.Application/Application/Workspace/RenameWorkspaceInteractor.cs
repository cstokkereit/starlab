using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that renames the workspace.
    /// </summary>
    internal class RenameWorkspaceInteractor : WorkspaceInteractor, IRenameWorkspaceUseCase
    {
        private readonly ISerialisationProvider serialiser; // Used to serialise the workspace to a file.

        /// <summary>
        /// Initialises a new instance of the <see cref="RenameWorkspaceInteractor"/> class.
        /// </summary>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used to serialise the <see cref="WorkspaceDTO"/>.</param>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
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
            var filename = dto.FileName;

            if (IsValid(name) && !string.IsNullOrEmpty(filename))
            {
                dto.FileName = Path.ChangeExtension(Path.Join(Path.GetDirectoryName(filename), name), Constants.WORKSPACE_EXTENSION);
               
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
                    throw CreateTargetExistsException(Path.GetFileName(filename), Path.GetFileName(dto.FileName), Resources.Workspace);
                }
            }
            else
            {
                throw CreateInvalidNameException(name, Resources.Workspace);
            }
        }
    }
}
