namespace StarLab.Presentation.Charts
{
    public interface IChartSettingsViewPresenter : IControlViewPresenter
    {
        void AttachCommands(IApplicationController controller, ISplitViewController viewController);
    }
}
