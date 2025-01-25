namespace StarLab.Application.Workspace
{
    /// <summary>
    /// Represents an element in the workspace hierarchy that can be collapsed and expanded.
    /// </summary>
    public interface ICollapsible
    {
        /// <summary>
        /// Returns <see cref="true"/> if this element is currently expanded; <see cref="false"/> otherwise.
        /// </summary>
        bool Expanded { get; }

        /// <summary>
        /// Collapses the current element;
        /// </summary>
        void Collapse();

        /// <summary>
        /// Collpases the current element and all elements beneath it in the hierarchy.
        /// </summary>
        void CollapseAll();

        /// <summary>
        /// Expands the current element;
        /// </summary>
        void Expand();

        /// <summary>
        /// Expands the current element and all elements beneath it in the hierarchy.
        /// </summary>
        void ExpandAll();
    }
}
