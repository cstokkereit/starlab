using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that adds a document to a folder in the workspace hierarchy.
    /// </summary>
    internal class AddDocumentInteractor : UseCaseInteractor<IApplicationOutputPort>, IAddDocumentUseCase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IApplicationOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public AddDocumentInteractor(IApplicationOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="dtoDocument">A <see cref="DocumentDTO"/> that defines the document being added.</param>
        public void Execute(WorkspaceDTO dtoWorkspace, DocumentDTO dtoDocument)
        {
            var workspace = new Workspace(dtoWorkspace);
            
            if (WorkspaceInteractionHelper.IsValid(dtoDocument.Name))
            {
                try
                {
                    var folder = workspace.GetFolder(dtoDocument.Path);
                    var document = new Document(dtoDocument, folder);
                    workspace.AddDocument(document);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));

                    OutputPort.OpenDocument(document.ID);
                }
                catch(InvalidOperationException)
                {
                    OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.DocumentExistsWarning, dtoDocument.Name), InteractionType.Error, InteractionResponses.OK);
                }
            }
            else
            {
                OutputPort.ShowMessage(Resources.StarLab, WorkspaceInteractionHelper.CreateInvalidNameMessage(dtoDocument.Name, Resources.Document), InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
