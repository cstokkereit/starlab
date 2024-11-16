using StarLab.Application.Workspace;

namespace StarLab.Application
{
    // https://medium.com/codenx/code-in-clean-vs-traditional-layered-architecture-net-31c4cad8f815

    // https://www.google.com/search?sca_esv=f7c375419f60a470&sca_upv=1&rlz=1C1ASUM_enGB894GB894&q=clean+architecture&udm=2&fbs=AEQNm0B2yzHMOEf_Yi0v5EYEWbKOCio4_914wq6ufX8pGLlmAryrUrheWY_IoupTN8gakBp5DNxj4sqXIaMhouUHzuVQl9m-o5eJNmtaklRgHL6TWsp5CYTO9THZ4ad6blX5CKzChvyGw1w1auABFngTQM7PvGpXLHC-u82MuCnek6KDeTh8WFYaauER5Nc6ml-30s0Zz72mGswt5KqVaMjZz-I8HrtSbw&sa=X&ved=2ahUKEwjbpqz1ju2HAxVuSkEAHZG8MHgQtKgLegQIDhAB&biw=1920&bih=919&dpr=1#vhid=nPr36GbgH40TfM&vssid=mosaic
    public interface IUseCaseFactory
    {
        IAddDocumentUseCase CreateAddDocumentUseCase(IWorkspaceOutputPort outputPort);

        IAddFolderUseCase CreateAddFolderUseCase(IWorkspaceOutputPort outputPort);

        IDeleteItemUseCase CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort);

        IDeleteItemUseCase CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort);

        IOpenWorkspaceUseCase CreateOpenWorkspaceUseCase(IWorkspaceOutputPort outputPort);

        IRenameItemUseCase CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort);

        IRenameWorkspaceUseCase CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort);

        IRenameItemUseCase CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort);

        ISaveWorkspaceUseCase CreateSaveWorkspaceUseCase(IWorkspaceOutputPort outputPort);
    }
}
