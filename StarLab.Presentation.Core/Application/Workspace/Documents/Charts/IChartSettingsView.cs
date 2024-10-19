using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public interface IChartSettingsView : IControlView, IFormContent<IDocumentController>
    {
        void AttachCancelButtonCommand(ICommand command);

        void AttachOKButtonCommand(ICommand command);
    }
}
