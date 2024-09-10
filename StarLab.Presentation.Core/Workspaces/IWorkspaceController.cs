namespace StarLab.Presentation.Workspaces
{
    public interface IWorkspaceController : IController
    {
        void CloseWorkspace();

        void NewWorkspace();

        void OpenWorkspace();

        void SaveWorkspace();
    }
}
