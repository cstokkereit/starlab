using StarLab.Application.DataTransfer;
using System.Collections.Generic;
using System.IO;

namespace StarLab.Application.Model
{
    public class Workspace : IWorkspace
    {
        private readonly Dictionary<string, IDocument> documentsByID = new Dictionary<string, IDocument>();

        private readonly Dictionary<string, IFolder> foldersByPath = new Dictionary<string, IFolder>();

        public Workspace(WorkspaceDTO dto)
        {
            if (dto.Folders != null)
            {
                CreateFolders(dto.Folders);
            }

            if (dto.Documents != null)
            {
                CreateDocuments(dto.Documents);
            }
        }

        public Workspace() { }
        public IEnumerable<IDocument> Documents => documentsByID.Values;

        public IEnumerable<IFolder> Folders => foldersByPath.Values;

        public void AddFolder(IFolder folder)
        {
            if (!foldersByPath.ContainsKey(folder.Path)) foldersByPath.Add(folder.Path, folder);
        }

        public void DeleteDocument(string id)
        {
            throw new NotImplementedException();
        }

        public void DeleteFolder(string path)
        {
            DeleteFolders(GetFolder(path));
        }

        public IDocument GetDocument(string id)
        {
            return documentsByID[id];
        }

        public IFolder GetFolder(string path)
        {
            return foldersByPath[path];
        }

        public void RenameFolder(IFolder folder, string name)
        {
            var folders = new List<IFolder>(foldersByPath.Values);
            if (folder is Folder f) f.Name = name;
            UpdatePaths(folders);
        } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateDocuments(IEnumerable<DocumentDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Path) && foldersByPath.ContainsKey(dto.Path))
                {
                    var document = new Document(dto);
                    foldersByPath[document.Path].AddDocument(document);
                    documentsByID.Add(document.ID, document);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtos"></param>
        private void CreateFolders(IEnumerable<FolderDTO> dtos)
        {
            Folder folder;

            foreach (var dto in dtos)
            {
                if (!string.IsNullOrEmpty(dto.Path))
                {
                    var parentPath = dto.Path.Substring(0, dto.Path.LastIndexOf('/'));

                    if (foldersByPath.ContainsKey(parentPath))
                        folder = new Folder(dto, foldersByPath[parentPath]);
                    else
                        folder = new Folder(dto);

                    foldersByPath.Add(folder.Path, folder);
                }
            }
        }

        private void DeleteFolders(IFolder folder)
        {
            foreach (var document in folder.Documents)
            {
                documentsByID.Remove(document.ID);
            }

            foreach (var child in folder.Folders)
            {
                foldersByPath.Remove(child.Path);

                DeleteFolders(child);
            }

            foldersByPath.Remove(folder.Path);

            folder.DeleteContents();
        }

        private void UpdatePaths(IEnumerable<IFolder> folders)
        {
            foldersByPath.Clear();

            foreach (var folder in folders)
            {
                foreach (var document in folder.Documents)
                {
                    document.Path = folder.Path;
                }

                foldersByPath.Add(folder.Path, folder);
            }
        }
    }
}
