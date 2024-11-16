namespace StarLab.Application.Workspace.Documents.Charts
{
    public interface IChartViewPresenter : IControlViewPresenter
    {
        void Initialise(IApplicationController controller, IDocumentController parentController);
    }
}
