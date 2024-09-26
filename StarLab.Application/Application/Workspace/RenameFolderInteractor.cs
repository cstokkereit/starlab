using AutoMapper;
using StarLab.Application.DataTransfer;
using StarLab.Application.Model;

namespace StarLab.Application.Workspace
{
    internal class RenameFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IRenameDocumentUseCase
    {
        private readonly char[] invalidCharacters = { '/', '?', ':', '&',  '\\', '*', '\'', '\"', '<', '>', '|', '#', '%' };

        public RenameFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }
    
        public void Execute(WorkspaceDTO dto, string id, string name)
        {
            throw new NotImplementedException();

            //var workspace = new Model.Workspace(dto);

            //var document = workspace.GetDocument(id);

            //var folder = workspace.GetFolder(document.Path);

            //if (IsValid(folder, name))
            //{
            //    document.Name = name;
            //}
            //else
            //{
            //    throw new NotImplementedException(); // Show a message
            //}

            //OutputPort.UpdateDocument(Mapper.Map<IDocument, DocumentDTO>(document));
        }

        private bool IsValid(IFolder folder, string name)
        {
            throw new NotImplementedException();

            //var valid = true;

            //foreach (var character in invalidCharacters)
            //{
            //    if (name.Contains(character))
            //    {
            //        valid = false;
            //        break;
            //    }
            //}

            //foreach (var document in folder.Documents)
            //{
            //    if (document.Name == name)
            //    {
            //        valid = false;
            //        break;
            //    }
            //}

            //return valid;
        }
    }
}
