using System.ComponentModel;

namespace StarLab.Application
{
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
