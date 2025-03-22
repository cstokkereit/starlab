namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Represents a project folder.
    /// </summary>
    public interface IFolder : ICollapsible
    {
        /// <summary>
        /// Gets the folder key. 
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the name of the folder.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the parent folder key.
        /// </summary>
        string ParentKey { get; }
    }
}
