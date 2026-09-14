using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    /// <summary>
    /// A static class for constructing user friendly information, warning and error messages.
    /// </summary>
    public static class MessageBuilder
    {
        public static string DocumentCouldNotBeCreated => Resources.DocumentCouldNotBeCreated;

        public static string WorkspaceClosing => Resources.WorkspaceClosing;

        public static string WorkspaceCouldNotBeSaved => Resources.WorkspaceCouldNotBeSaved;

        /// <summary>
        /// Creates a user friendly ClipboardContentsCouldNotBePasted message.
        /// </summary>
        /// <param name="folder">The destination folder.</param>
        /// <returns>A user friendly error message.</returns>
        public static string ClipboardContentsCouldNotBePasted(string folder)
        {
            return string.Format(Resources.ClipboardContentsCouldNotBePasted, folder);
        }

        /// <summary>
        /// Creates a user friendly DestinationSameAsSource message.
        /// </summary>
        /// <param name="source">The name of the document or folder being moved.</param>
        /// <returns>A user friendly error message.</returns>
        public static string DestinationSameAsSource(string source)
        {
            return string.Format(Resources.DestinationSameAsSource, source);
        }

        /// <summary>
        /// Creates a user friendly DocumentAlreadyExistsWithReplaceOption message.
        /// </summary>
        /// <param name="document">The document name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string DocumentAlreadyExistsWithReplaceOption(string document)
        {
            return $"{string.Format(Resources.DocumentAlreadyExists, document)} {Resources.DoYouWantToReplaceIt}";
        }

        /// <summary>
        /// Creates a user friendly DocumentAlreadyExists message.
        /// </summary>
        /// <param name="document">The document name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string DocumentAlreadyExists(string document)
        {
            return string.Format(Resources.DocumentAlreadyExists, document);
        }

        /// <summary>
        /// Creates a user friendly DocumentCouldNotBeDeleted message.
        /// </summary>
        /// <param name="document">The document name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string DocumentCouldNotBeDeleted(string document)
        {
            return string.Format(Resources.DocumentCouldNotBeDeleted, document);
        }

        /// <summary>
        /// Creates a user friendly DocumentCouldNotBeRenamed message.
        /// </summary>
        /// <param name="oldName">The document name.</param>
        /// <param name="newName">The new document name.</param>
        /// <returns>A user friendly error message.</returns>
        public static string DocumentCouldNotBeRenamed(string oldName, string newName)
        {
            return string.Format(Resources.DocumentCouldNotBeRenamed, oldName, newName);
        }

        /// <summary>
        /// Creates a user friendly DocumentDeletionWarning message.
        /// </summary>
        /// <param name="document">The document name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string DocumentDeletionWarning(string document)
        {
            return string.Format(Resources.DocumentDeletionWarning, document);
        }

        /// <summary>
        /// Creates a user friendly DocumentNameInvalid message.
        /// </summary>
        /// <param name="document">The document name.</param>
        /// <returns>A user friendly error message.</returns>
        public static string DocumentNameInvalid(string document)
        {
            if (!string.IsNullOrEmpty(document))
            {
                return string.Format(Resources.NameContainsIllegalCharacters, Resources.Document, "\\ / : * ? ' \" < > |");
            }

            return string.Format(Resources.NameCannotBeNullOrEmpty, Resources.Document.ToLower());
        }

        /// <summary>
        /// Creates a user friendly FileCouldNotBeOpened message.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string FileCouldNotBeOpened(string filename)
        {
            return string.Format(Resources.FileCouldNotBeOpened, filename);
        }

        /// <summary>
        /// Creates a user friendly FileNotFound message.
        /// </summary>
        /// <param name="filename">The file name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string FileNotFound(string filename)
        {
            return string.Format(Resources.FileNotFound, filename);
        }

        /// <summary>
        /// Creates a user friendly FolderAlreadyExists message.
        /// </summary>
        /// <param name="folder">The folder name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string FolderAlreadyExists(string folder)
        {
            return string.Format(Resources.FolderAlreadyExists, folder);
        }

        /// <summary>
        /// Creates a user friendly FolderCouldNotBeAddedMessage message.
        /// </summary>
        /// <param name="parent">The path to the parent folder.</param>
        /// <returns>The required user friendly message.</returns>
        public static string FolderCouldNotBeAdded(string parent)
        {
            return string.Format(Resources.FolderCouldNotBeAdded, parent);
        }

        /// <summary>
        /// Creates a user friendly FolderCouldNotBeDeleted message.
        /// </summary>
        /// <param name="folder">The path to the folder.</param>
        /// <returns>The required user friendly message.</returns>
        public static string FolderCouldNotBeDeleted(string folder)
        {
            return string.Format(Resources.FolderCouldNotBeDeleted, folder);
        }

        /// <summary>
        /// Creates a user friendly FolderCouldNotBeRenamed message.
        /// </summary>
        /// <param name="oldName">The folder name.</param>
        /// <param name="newName">The new folder name.</param>
        /// <returns>A user friendly error message.</returns>
        public static string FolderCouldNotBeRenamed(string oldName, string newName)
        {
            return string.Format(Resources.FolderCouldNotBeRenamed, oldName, newName);
        }

        /// <summary>
        /// Creates a user friendly FolderDeletionWarning message.
        /// </summary>
        /// <param name="folder">The path to the folder.</param>
        /// <returns>The required user friendly message.</returns>
        public static string FolderDeletionWarning(string folder)
        {
            return string.Format(Resources.FolderDeletionWarning, folder);
        }

        /// <summary>
        /// Creates a user friendly FolderNameInvalid message.
        /// </summary>
        /// <param name="folder">The folder name.</param>
        /// <returns>A user friendly error message.</returns>
        public static string FolderNameInvalid(string folder)
        {
            if (!string.IsNullOrEmpty(folder))
            {
                return string.Format(Resources.NameContainsIllegalCharacters, Resources.Folder, "\\ / : * ? ' \" < > |");
            }

            return string.Format(Resources.NameCannotBeNullOrEmpty, Resources.Folder.ToLower());
        }

        /// <summary>
        /// Creates a user friendly ProjectCouldNotBeDeleted message.
        /// </summary>
        /// <param name="project">The project name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string ProjectCouldNotBeDeleted(string project)
        {
            return string.Format(Resources.ProjectCouldNotBeDeleted, project);
        }

        /// <summary>
        /// Creates a user friendly ProjectDeletionWarning message.
        /// </summary>
        /// <param name="folder">The project name.</param>
        /// <returns>The required user friendly message.</returns>
        public static string ProjectDeletionWarning(string project)
        {
            return string.Format(Resources.ProjectDeletionWarning, project);
        }

        /// <summary>
        /// Creates a user friendly WorkspaceCouldNotBeRenamed message.
        /// </summary>
        /// <param name="oldName">The document name.</param>
        /// <param name="newName">The new document name.</param>
        /// <returns>A user friendly error message.</returns>
        public static string WorkspaceCouldNotBeRenamed(string oldName, string newName)
        {
            return string.Format(Resources.WorkspaceCouldNotBeRenamedAsAlreadyExists, oldName, newName);
        }

        /// <summary>
        /// Creates a user friendly WorkspaceCouldNotBeRenamed message.
        /// </summary>
        /// <returns>A user friendly error message.</returns>
        public static string WorkspaceCouldNotBeRenamed()
        {
            return Resources.WorkspaceCouldNotBeRenamed;
        }

        /// <summary>
        /// Creates a user friendly WorkspaceNameInvalid message.
        /// </summary>
        /// <param name="workspace">The workspace name.</param>
        /// <returns>A user friendly error message.</returns>
        public static string WorkspaceNameInvalid(string workspace)
        {
            if (!string.IsNullOrEmpty(workspace))
            {
                return string.Format(Resources.NameContainsIllegalCharacters, Resources.Workspace);
            }

            return string.Format(Resources.NameCannotBeNullOrEmpty, Resources.Workspace.ToLower());
        }
    }
}
