using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A service that executes the use cases that implement the add document functionality.
    /// </summary>
    public class AddDocumentUseCaseService : UseCaseService, IAddDocumentUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentUseCaseService"/>.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use cases.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        public AddDocumentUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Adds a document to the workspace.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="document">A <see cref="DocumentDTO"/> that holds the details of the document to be added.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public void AddDocument(IWorkspace workspace, DocumentDTO document)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentNullException.ThrowIfNull(document, nameof(document));
            
            var useCase = Factory.CreateAddDocumentUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            useCase.Execute(new AddDocumentUseCaseArgs(Mapper.Map<WorkspaceDTO>(workspace), document));
        }
    }
}
