using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IToolbarButton
    {
        /// <summary>
        /// 
        /// </summary>
        ICommand Command { get; }

        /// <summary>
        /// 
        /// </summary>
        Image Image { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Tooltip { get; }
    }
}
