namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IDockableView : IView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        void Initialise(IApplicationController controller);
    }
}
