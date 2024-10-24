namespace StarLab.Application.Workspace
{
    /// <summary>
    /// The interface for the Main view.
    /// </summary>
    public interface IWorkspaceView : IFormView, IMenuManager, IToolbarManager
    {
        /// <summary>
        /// 
        /// </summary>
        void CloseActiveDocument();

        /// <summary>
        /// 
        /// </summary>
        void CloseAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        string GetLayout();

        void Initialise(IApplicationController controller, IDockableViewFactory factory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="layout"></param>
        void SetLayout(string layout);
    }
}
