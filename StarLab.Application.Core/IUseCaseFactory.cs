using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Application
{
    // See https://medium.com/unil-ci-software-engineering/common-pitfalls-when-implementing-use-cases-in-clean-architecture-6e4bbb1cec5e for what use cases should do!

    // https://medium.com/codenx/code-in-clean-vs-traditional-layered-architecture-net-31c4cad8f815

    // https://www.google.com/search?sca_esv=f7c375419f60a470&sca_upv=1&rlz=1C1ASUM_enGB894GB894&q=clean+architecture&udm=2&fbs=AEQNm0B2yzHMOEf_Yi0v5EYEWbKOCio4_914wq6ufX8pGLlmAryrUrheWY_IoupTN8gakBp5DNxj4sqXIaMhouUHzuVQl9m-o5eJNmtaklRgHL6TWsp5CYTO9THZ4ad6blX5CKzChvyGw1w1auABFngTQM7PvGpXLHC-u82MuCnek6KDeTh8WFYaauER5Nc6ml-30s0Zz72mGswt5KqVaMjZz-I8HrtSbw&sa=X&ved=2ahUKEwjbpqz1ju2HAxVuSkEAHZG8MHgQtKgLegQIDhAB&biw=1920&bih=919&dpr=1#vhid=nPr36GbgH40TfM&vssid=mosaic

    /// <summary>
    /// Represents a factory for creating use cases.
    /// </summary>
    public interface IUseCaseFactory
    {
        /// <summary>
        /// Creates a use case that adds an overlay to a chart.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddChartOverlayUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<AddChartOverlayUseCaseArgs> CreateAddChartOverlayUseCase(IChartOutputPort outputPort);

        /// <summary>
        /// Creates a use case that adds a document to the workspace.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddDocumentUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<AddDocumentUseCaseArgs> CreateAddDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that adds a folder to the workspace.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddFolderUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<AddFolderUseCaseArgs> CreateAddFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that adds a project to the workspace.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{AddProjectUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<AddProjectUseCaseArgs> CreateAddProjectUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ClipboardUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<ClipboardUseCaseArgs> CreateCopyAndPasteUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that copies a folder in the workspace hierarchy. 
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ClipboardUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<ClipboardUseCaseArgs> CreateCutAndPasteUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that deletes a document from the workspace.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{DeleteDocumentUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<DeleteDocumentUseCaseArgs> CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that deletes a folder from the workspace.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{DeleteFolderUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<DeleteFolderUseCaseArgs> CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that loads a workspace from a file.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{string}"/> that implements the use case.</returns>
        IUseCase<string> CreateOpenWorkspaceUseCase(IApplicationOutputPort outputPort);

        /// <summary>
        /// Creates a use case that TODO
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        IUseCaseAsync<PlotChartUseCaseArgs> CreatePlotChartUseCase(IChartOutputPort outputPort);

        /// <summary>
        /// Creates a use case that removes an overlay from a chart.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RemoveChartOverlayUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<RemoveChartOverlayUseCaseArgs> CreateRemoveChartOverlayUseCase(IChartOutputPort outputPort);

        /// <summary>
        /// Creates a use case that renames a document in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameDocumentUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<RenameDocumentUseCaseArgs> CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that renames a folder in the workspace hierarchy.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameFolderUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<RenameFolderUseCaseArgs> CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that renames the workspace.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{RenameWorkspaceUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<RenameWorkspaceUseCaseArgs> CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort);

        /// <summary>
        /// Creates a use case that saves the current workspace to a file.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{WorkspaceDTO}"/> that implements the use case.</returns>
        IUseCase<WorkspaceDTO> CreateSaveWorkspaceUseCase(IApplicationOutputPort outputPort);

        /// <summary>
        /// Creates a use case that updates a preview chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{ChartDTO}"/> that implements the use case.</returns>
        IUseCase<ChartDTO> CreateUpdateChartPreviewUseCase(IChartOutputPort outputPort);

        /// <summary>
        /// Creates a use case that updates a chart in response to a settings change.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <returns>An instance of <see cref="IUseCase{UpdateDocumentUseCaseArgs}"/> that implements the use case.</returns>
        IUseCase<UpdateChartUseCaseArgs> CreateUpdateChartUseCase(IApplicationOutputPort outputPort);
    }
}
