using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;

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
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, DocumentDTO}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, DocumentDTO> CreateAddDocumentUseCase(IAddDocumentOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string> CreateAddFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that adds a project to the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, ProjectDTO}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, ProjectDTO> CreateAddProjectUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string> CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string> CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that loads a workspace from a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{string}"/> that implements the use case.</returns>
        IUseCase<string> CreateOpenWorkspaceUseCase(IApplicationOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that renames a document in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string, string> CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string, string> CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that renames the workspace.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string> CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that saves the current workspace to a file.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO> CreateSaveWorkspaceUseCase(IApplicationOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <param name="operation">A <see cref="ClipboardOperations"/> enum that specifies the clipboard operation.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string> CreateUseCase(IWorkspaceOutputPort outputPort, ClipboardOperations operation);

        /// <summary>
        /// Creates a use case interactor that updates a chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ChartDTO}"/> that implements the use case.</returns>
        IUseCase<ChartDTO> CreateUpdateChartUseCase(IChartOutputPort outputPort);

        /// <summary>
        /// Creates a use case interactor that updates a document in response to a settings change.
        /// </summary>
        /// <param name="outputPort">An <see cref="IChartOutputPort"/> that updates the UI in response to the outputs of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO, string, ChartDTO}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO, string, ChartDTO> CreateUpdateDocumentUseCase(IApplicationOutputPort outputPort);
    }
}
