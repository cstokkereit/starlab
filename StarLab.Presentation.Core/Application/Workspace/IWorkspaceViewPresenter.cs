namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IWorkspaceViewPresenter : IDialogViewPresenter
    {
        /// <summary>
        /// 
        /// </summary>
        void ClearActiveDocument();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IDockableView CreateView(string id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        void SetActiveDocument(string id);
    }
}
