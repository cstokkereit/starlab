using AutoMapper;
using Stratosoft.Commands;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that copies a folder at a specified location within the workspace hierarchy.
    /// </summary>
    public class ClipboardInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IClipboardInteractionUseCase
    {
        private static ParameterisedCommand<Workspace, string>? command; // A command that captures the current cut or copy operation. It will be executed in response to a paste operation.

        private readonly ClipboardOperations operation; // An enum that stores the current clipboard operation.

        /// <summary>
        /// Initialises a new instance of the <see cref="ClipboardInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="operation">A <see cref="ClipboardOperations"/> enum that specifies the clipboard operation.</param>
        public ClipboardInteractor(IWorkspaceOutputPort outputPort, IMapper mapper, ClipboardOperations operation)
            : base(outputPort, mapper)
        {
            this.operation = operation;
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the target of the current cut, copy or paste operation.</param>
        public void Execute(WorkspaceDTO dto, string key)
        {
            var workspace = new Workspace(dto);

            switch (operation)
            {
                case ClipboardOperations.Copy:
                    command = new CopyAndPasteCommand(workspace, key, Mapper, OutputPort);
                    OutputPort.UpdateClipboard(key);
                    break;

                case ClipboardOperations.Cut:
                    command = new CutAndPasteCommand(workspace, key, Mapper, OutputPort);
                    OutputPort.UpdateClipboard(key);
                    break;

                case ClipboardOperations.Paste:
                    if (command != null)
                    {
                        command.Execute(workspace, key);
                        OutputPort.ClearClipboard();
                        command = null;
                    }
                    break;
            }
        }
    }
}
