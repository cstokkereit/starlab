using AutoMapper;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application.Workspace
{
    internal class AddDocumentInteractor : WorkspaceInteractor, IAddDocumentUseCase
    {
        public AddDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string path)
        {
            var workspace = new Workspace(dto);
            
            //var document = new Document();


            //workspace.AddDocument(document);
            
        }
    }
}
