using StarLab.Commands;

namespace StarLab.Presentation.Charts
{
    public interface IChartSettingsView : IControlView
    {
        void AttachCancelButtonCommand(ICommand command);

        void AttachOKButtonCommand(ICommand command);
    }
}
