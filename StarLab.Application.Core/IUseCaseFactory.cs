using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    // See https://medium.com/unil-ci-software-engineering/common-pitfalls-when-implementing-use-cases-in-clean-architecture-6e4bbb1cec5e for what use cases should do!



    // https://medium.com/codenx/code-in-clean-vs-traditional-layered-architecture-net-31c4cad8f815

    // https://www.google.com/search?sca_esv=f7c375419f60a470&sca_upv=1&rlz=1C1ASUM_enGB894GB894&q=clean+architecture&udm=2&fbs=AEQNm0B2yzHMOEf_Yi0v5EYEWbKOCio4_914wq6ufX8pGLlmAryrUrheWY_IoupTN8gakBp5DNxj4sqXIaMhouUHzuVQl9m-o5eJNmtaklRgHL6TWsp5CYTO9THZ4ad6blX5CKzChvyGw1w1auABFngTQM7PvGpXLHC-u82MuCnek6KDeTh8WFYaauER5Nc6ml-30s0Zz72mGswt5KqVaMjZz-I8HrtSbw&sa=X&ved=2ahUKEwjbpqz1ju2HAxVuSkEAHZG8MHgQtKgLegQIDhAB&biw=1920&bih=919&dpr=1#vhid=nPr36GbgH40TfM&vssid=mosaic

    /// <summary>
    /// Represents a factory for creating use case interactors.
    /// </summary>
    public interface IUseCaseFactory
    {
        /// <summary>
        /// Creates a use case interactor that adds a document to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IApplicationOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IAddDocumentUseCase"/> that implements the use case.</returns>
        IAddDocumentUseCase CreateAddDocumentUseCase(IApplicationOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IAddFolderUseCase"/> that implements the use case.</returns>
        IAddFolderUseCase CreateAddFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that adds a project to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IAddProjectUseCase"/> that implements the use case.</returns>
        IAddProjectUseCase CreateAddProjectUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IDeleteItemUseCase"/> that implements the use case.</returns>
        IDeleteItemUseCase CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IDeleteItemUseCase"/> that implements the use case.</returns>
        IDeleteItemUseCase CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that loads a workspace from a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IOpenWorkspaceUseCase"/> that implements the use case.</returns>
        IOpenWorkspaceUseCase CreateOpenWorkspaceUseCase(IApplicationOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that renames a document in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IRenameItemUseCase"/> that implements the use case.</returns>
        IRenameItemUseCase CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IRenameItemUseCase"/> that implements the use case.</returns>
        IRenameItemUseCase CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that renames the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="IRenameWorkspaceUseCase"/> that implements the use case.</returns>
        IRenameWorkspaceUseCase CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that saves the current workspace to a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <returns>An instance of <see cref="ISaveWorkspaceUseCase"/> that implements the use case.</returns>
        ISaveWorkspaceUseCase CreateSaveWorkspaceUseCase(IApplicationOutputPort outputPort);
    }
}
