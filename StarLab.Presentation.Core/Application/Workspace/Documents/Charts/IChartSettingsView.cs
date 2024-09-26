using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public interface IChartSettingsView : IControlView
    {
        void AttachCancelButtonCommand(ICommand command);

        void AttachOKButtonCommand(ICommand command);
    }
}
