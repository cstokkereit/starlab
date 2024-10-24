using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    internal class RenameDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IRenameItemUseCase
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
                    throw CreateException(document.Name, name);
                }
            }
            else
            {
                throw CreateException();
            }
        }

        private Exception CreateException(string oldName, string newName)
        {
            return new Exception(string.Format(Resources.CannotRenameBecauseNameAlreadyExists, oldName, newName, Resources.Document.ToLower()));
        }

        private Exception CreateException()
        {
            return new Exception(string.Format(Resources.NameContainsIllegalCharacters, Resources.Document, string.Join(' ', Constants.IllegalCharacters)));
        }

        private bool IsValid(Folder folder, string name)
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

        private bool IsValid(string name)
        {
            var valid = !string.IsNullOrEmpty(name);

            if (valid)
            {
                foreach (var character in Constants.IllegalCharacters)
                {
                    if (name.Contains(character))
                    {
                        valid = false;
                        break;
                    }
                }
            }

            return valid;
        }
    }
}
