using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A helper class that exposes functions that can be used by any of the workspace interactors.
    /// </summary>
    internal static class WorkspaceInteractionHelper
    {
        /// <summary>
        /// Creates an error message in response to a duplicate name being provided when renaming an item.
        /// </summary>
        /// <param name="oldName">The current name of the item being renamed.</param>
        /// <param name="newName">A name that is not valid because an item of the same type with the same name already exists.</param>
        /// <param name="target">The item being renamed.</param>
        /// <returns>An error message identifying the issue with the name provided.</returns>
        public static string CreateCannotRenameItemMessage(string oldName, string newName, string target)
        {
            if (target == Resources.Workspace)
            {
                return string.Format(Resources.CannotRenameWorkspace, Path.GetFileNameWithoutExtension(oldName), Path.GetFileNameWithoutExtension(newName));
            }

            return string.Format(Resources.CannotRenameItem, oldName, newName, target.ToLower());
        }

        /// <summary>
        /// Creates an error message in response to an invalid name being provided.
        /// </summary>
        /// <param name="name">A name that is not valid for the item being named.</param>
        /// <param name="target">The item being named.</param>
        /// <returns>An error message identifying the issue with the name provided.</returns>
        public static string CreateInvalidNameMessage(string? name, string target)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return string.Format(Resources.NameCannotInclude, target, string.Join(' ', Constants.IllegalCharacters));
            }

            return string.Format(Resources.NameNullOrEmpty, target.ToLower());
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{DocumentDTO}"/> containing the specified <see cref="DocumentDTO"/>s from the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that contains the required <see cref="DocumentDTO"/>s.</param>
        /// <param name="ids">An <see cref="IEnumerable{string}"/> containing the IDs of the required <see cref="DocumentDTO"/>s.</param>
        /// <returns>An <see cref="IEnumerable{DocumentDTO}"/> containing the required <see cref="DocumentDTO"/>s.</returns>
        public static IEnumerable<DocumentDTO> GetDocumentDTOs(WorkspaceDTO dto, IEnumerable<string> ids)
        {
            List<DocumentDTO> dtos = new List<DocumentDTO>();

            foreach (var project in dto.Projects)
            {
                dtos.AddRange(project.Documents.Where(d => ids.Contains(d.ID)));
            }

            return dtos;
        }

        /// <summary>
        /// A recursive function that gets all folders that are descendents of the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="folder">The top most <see cref="IFolder"/> in the required folder tree.</param>
        /// <returns>An <see cref="IEnumerable{IFolder}"/> containing the required folders.</returns>
        public static IEnumerable<IFolder> GetChildFolders(IFolder folder)
        {
            var folders = new List<IFolder>();

            foreach (var child in folder.Folders)
            {
                folders.Add(child);
                folders.AddRange(GetChildFolders(child));
            }

            return folders;
        }

        /// <summary>
        /// Gets all of the documents contained within the specified folders.
        /// </summary>
        /// <param name="folders">An <see cref="IEnumerable{IFolder}"/> containing the folders.</param>
        /// <returns>An <see cref="IEnumerable{Document}"/> containing the required documents.</returns>
        public static IEnumerable<Document> GetDocuments(IEnumerable<IFolder> folders)
        {
            var documents = new List<Document>();

            foreach (var folder in folders)
            {
                documents.AddRange(folder.Documents);
            }

            return documents;
        }

        /// <summary>
        /// Checks the name provided to make sure that it does not contain any illegal characters.
        /// </summary>
        /// <param name="name">A name that may contain illegal characters.</param>
        /// <returns><see cref="true"/> if the name does not contain illegal characters; <see cref="false"/> otherwise.</returns>
        public static bool IsValid(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            foreach (var character in Constants.IllegalCharacters)
            {
                if (name.Contains(character)) return false;
            }

            return true;
        }
    }
}