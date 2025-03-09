using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that adds a document to a folder in the workspace hierarchy.
    /// </summary>
    internal class AddDocumentInteractor : WorkspaceInteractor, IAddDocumentUseCase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public AddDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="dtoDocument">A <see cref="DocumentDTO"/> that defines the document being added.</param>
        public void Execute(WorkspaceDTO dtoWorkspace, DocumentDTO dtoDocument)
        {
            var workspace = new Workspace(dtoWorkspace);
            
            if (IsValid(dtoDocument.Name))
            {
                if (!workspace.HasDocument(dtoDocument.Name, dtoDocument.Path))
                {
                    var folder = workspace.GetFolder(dtoDocument.Path);
                    var document = new Document(dtoDocument, folder);
                    workspace.AddDocument(document);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));

                    OutputPort.OpenDocument(document.ID);
                }
                else
                {
                    OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DocumentExistsWarning, dtoDocument.Name), InteractionType.Error, InteractionResponses.OK);
                }
            }
            else
            {
                OutputPort.ShowMessage(Resources.StarLab, CreateInvalidNameMessage(dtoDocument.Name, Resources.Document), InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
