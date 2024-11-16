using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public interface IChartSettingsView : IChildView
    {
        void AttachCancelButtonCommand(ICommand command);

        void AttachOKButtonCommand(ICommand command);
    }
}
