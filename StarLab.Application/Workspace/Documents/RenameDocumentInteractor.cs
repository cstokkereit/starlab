using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that renames a document in the workspace hierarchy.
    /// </summary>
    internal class RenameDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<RenameDocumentUseCaseArgs>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public RenameDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="RenameDocumentUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(RenameDocumentUseCaseArgs args)
        {
            if (WorkspaceInteractionHelper.IsValid(args.Name))
            {
                var workspace = new Workspace(args.Workspace);

                var document = workspace.GetDocument(new DocumentID(args.DocumentID));

                var folder = workspace.GetFolder(document.Path);

                if (IsValid(folder, args.Name))
                {
                    workspace.RenameDocument(document, args.Name);

                    OutputPort.UpdateDocument(Mapper.Map<WorkspaceDTO>(workspace), args.DocumentID);
                }
                else
                {
                    throw new Exception(WorkspaceInteractionHelper.CreateCannotRenameItemMessage(document.Name, args.Name, Resources.Document));
                }
            }
            else
            {
                throw new Exception(WorkspaceInteractionHelper.CreateInvalidNameMessage(args.Name, Resources.Document));
            }
        }

        /// <summary>
        /// Checks for the existance of a <see cref="Document"/> within the <see cref="IFolder"/> provided that has a name that matches the new document name.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> containing the documents.</param>
        /// <param name="name">The new document name.</param>
        /// <returns>true if there are no documents with matching names; false otherwise.</returns>
        private bool IsValid(IFolder folder, string name)
        {
            var valid = true;

            foreach (var document in folder.Documents)
            {
                if (document.Name == name)
                {
                    valid = false;
                    break;
                }
            }

            return valid;
        }
    }
}
