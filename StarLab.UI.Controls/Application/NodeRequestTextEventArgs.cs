using System.ComponentModel;

namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    public class NodeRequestTextEventArgs : CancelEventArgs
    {
        public NodeRequestTextEventArgs(TreeNode node, string label)
        {
            Label = label;
            Node = node;
        }

        protected NodeRequestTextEventArgs() { }

        public string? Label { get; set; }

        public TreeNode? Node { get; protected set; }
    }
}
