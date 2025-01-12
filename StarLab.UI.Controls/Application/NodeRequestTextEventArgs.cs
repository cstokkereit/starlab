using System.ComponentModel;

namespace StarLab.Application
{
    /// <summary>
    /// Provides data for the <see cref="TreeView.RequestDisplayText"/> and <see cref="TreeView.RequestEditText"/> events.
    /// </summary>
    public class NodeRequestTextEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NodeRequestTextEventArgs"/> class.
        /// </summary>
        /// <param name="node">The <see cref="TreeNode"/> that owns the label.</param>
        /// <param name="label">The label text.</param>
        public NodeRequestTextEventArgs(TreeNode node, string label)
        {
            Label = label;
            Node = node;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="NodeRequestTextEventArgs"/> class.
        /// </summary>
        protected NodeRequestTextEventArgs() { }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public string? Label { get; set; }

        /// <summary>
        /// Gets the <see cref="TreeNode"/> that owns the label.
        /// </summary>
        public TreeNode? Node { get; protected set; }
    }
}
