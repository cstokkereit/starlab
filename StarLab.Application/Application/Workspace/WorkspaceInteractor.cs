﻿using AutoMapper;
using StarLab.Application.Workspace.Documents;
using StarLab.Shared.Properties;
using System.Diagnostics;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// The base class for all workspace interactors.
    /// </summary>
    internal abstract class WorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public WorkspaceInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Displays a confirmation dialog box with the specified message.
        /// </summary>
        /// <param name="message">The message text.</param>
        /// <returns>true if the action was confirmed; false otherwise.</returns>
        protected bool ConfirmAction(string message)
        {
            return OutputPort.ShowMessage(Resources.StarLab, message, InteractionType.Warning, InteractionResponses.OKCancel) == InteractionResult.OK;
        }

        /// <summary>
        /// Creates an error message in response to an invalid name being provided.
        /// </summary>
        /// <param name="name">A name that is not valid for the item being named.</param>
        /// <param name="target">The item being named.</param>
        /// <returns>An error message identifying the issue with the name provided.</returns>
        protected static string CreateInvalidNameMessage(string? name, string target)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return string.Format(Resources.NameCannotInclude, target, string.Join(' ', Constants.IllegalCharacters));
            }

            return string.Format(Resources.NameNullOrEmpty, target.ToLower());
        }

        /// <summary>
        /// Creates an error message in response to a duplicate name being provided.
        /// </summary>
        /// <param name="oldName">The current name of the item being renamed.</param>
        /// <param name="newName">A name that is not valid because an item of the same type with the same name already exists.</param>
        /// <param name="target">The item being renamed.</param>
        /// <returns>An error message identifying the issue with the name provided.</returns>
        protected static string CreateTargetExistsMessage(string oldName, string newName, string target)
        {
            if (target == Resources.Workspace)
            {
                return string.Format(Resources.WorkspaceAlreadyExists, Path.GetFileNameWithoutExtension(oldName), Path.GetFileNameWithoutExtension(newName));
            }

            return string.Format(Resources.NameAlreadyExists, oldName, newName, target.ToLower());
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{DocumentDTO}"/> containing the specified <see cref="DocumentDTO"/>s from the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that contains the required <see cref="DocumentDTO"/>s.</param>
        /// <param name="ids">An <see cref="IEnumerable{string}"/> containing the IDs of the required <see cref="DocumentDTO"/>s.</param>
        /// <returns>An <see cref="IEnumerable{DocumentDTO}"/> containing the required <see cref="DocumentDTO"/>s.</returns>
        protected static IEnumerable<DocumentDTO> GetDocumentDTOs(WorkspaceDTO dto, IEnumerable<string> ids)
        {
            List<DocumentDTO> dtos = new List<DocumentDTO>();

            foreach (var project in dto.Projects)
            {
                dtos.AddRange(project.Documents.Where(d => ids.Contains(d.ID)));
            }

            return dtos;
        }

        /// <summary>
        /// Checks the name provided to make sure that it does not contain any illegal characters.
        /// </summary>
        /// <param name="name">A name that may contain illegal characters.</param>
        /// <returns>true if the name does not contain illegal characters; false otherwise.</returns>
        protected static bool IsValid(string? name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            foreach (var character in Constants.IllegalCharacters)
            {
                if (name.Contains(character)) return false;
            }

            return true;
        }

        /// <summary>
        /// A recursive function that gets all folders that are descendents of the specified <see cref="IFolder"/>.
        /// </summary>
        /// <param name="folder">The top most <see cref="IFolder"/> in the required folder tree.</param>
        /// <returns>An <see cref="IEnumerable{IFolder}"/> containing the required folders.</returns>
        private static IEnumerable<IFolder> GetChildFolders(IFolder folder)
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
        private static IEnumerable<Document> GetDocuments(IEnumerable<IFolder> folders)
        {
            var documents = new List<Document>();

            foreach (var folder in folders)
            {
                documents.AddRange(folder.Documents);
            }

            return documents;
        }
    }
}