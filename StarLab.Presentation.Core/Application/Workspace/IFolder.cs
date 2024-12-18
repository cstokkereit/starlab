namespace StarLab.Application.Workspace
{
    /// <summary>
    /// 
    /// </summary>
    public interface IFolder : ICollapsible
    {
        /// <summary>
        /// 
        /// </summary>
        bool IsNew { get; }

        /// <summary>
        /// 
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string ParentKey { get; }
    }
}
