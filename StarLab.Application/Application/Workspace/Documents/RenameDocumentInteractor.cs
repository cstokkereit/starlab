using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    internal class RenameDocumentInteractor : WorkspaceInteractor, IRenameItemUseCase
    {
        public RenameDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string id, string name)
        {
            if (IsValid(name))
            {
                var workspace = new Workspace(dto);
                var document = workspace.GetDocument(id);

                var folder = workspace.GetFolder(document.Path);

                if (IsValid(folder, name))
                {
                    document.Name = name;

                    OutputPort.UpdateDocument(Mapper.Map<Document, DocumentDTO>(document));
                }
                else
                {
                    throw CreateTargetExistsException(document.Name, name, Resources.Document);
                }
            }
            else
            {
                throw CreateInvalidNameException(name, Resources.Document);
            }
        }

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
