namespace StarLab.Application.Workspace.Documents.Charts
{
    public interface IChartSettingsViewPresenter : IControlViewPresenter
    {
        void AttachCommands(IApplicationController controller, ISplitViewController viewController);
    }
}
