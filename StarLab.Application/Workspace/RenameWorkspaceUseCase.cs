using AutoMapper;
using StarLab.Shared;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that renames the workspace.
    /// </summary>
    internal class RenameWorkspaceUseCase : WorkspaceUseCase, IUseCase<RenameWorkspaceUseCaseArgs>
    {
        private readonly ISerialisationService serialiser; // Used to serialise the workspace to a file.

        /// <summary>
        /// Initialises a new instance of the <see cref="RenameWorkspaceUseCase"/> class.
        /// </summary>
        /// <param name="serialiser">An <see cref="ISerialisationService"/> that will be used to serialise the <see cref="WorkspaceDTO"/>.</param>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        public RenameWorkspaceUseCase(ISerialisationService serialiser, IWorkspaceOutputPort outputPort, IMapper mapper)
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

            if (IsValid(args.Name) && !string.IsNullOrEmpty(filename))
            {
                args.Workspace.FileName = Path.ChangeExtension(Path.Join(Path.GetDirectoryName(filename), args.Name), Constants.WorkspaceExtension);
               
                if (!File.Exists(args.Workspace.FileName))
                {
                    serialiser.SerialiseWorkspace(args.Workspace, args.Workspace.FileName);

                    File.Delete(filename);

                    OutputPort.UpdateWorkspace(args.Workspace);
                }
                else
                {
                    throw new IOException(ExceptionMessages.FileExists(args.Workspace.FileName));
                }
            }
            else
            {
                throw new InvalidNameException(args.Name);
            }
        }
    }
}
