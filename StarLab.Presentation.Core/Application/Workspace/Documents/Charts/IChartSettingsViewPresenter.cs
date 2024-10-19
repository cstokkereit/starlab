namespace StarLab.Application.Workspace.Documents.Charts
{
    public interface IChartSettingsViewPresenter : IControlViewPresenter
    {
        void Initialise(IApplicationController controller, IDocumentController parentController);
    }
}
