using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface IChartSettingsView : IChildView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void AttachCancelButtonCommand(ICommand command);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        void AttachOKButtonCommand(ICommand command);
    }
}
