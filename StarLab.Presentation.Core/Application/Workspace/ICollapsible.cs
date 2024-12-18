namespace StarLab.Application.Workspace
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface ICollapsible
    {
        /// <summary>
        /// 
        /// </summary>
        bool Expanded { get; }

        /// <summary>
        /// 
        /// </summary>
        void Collapse();

        /// <summary>
        /// 
        /// </summary>
        void CollapseAll();

        /// <summary>
        /// 
        /// </summary>
        void Expand();

        /// <summary>
        /// 
        /// </summary>
        void ExpandAll();
    }
}
