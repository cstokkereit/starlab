namespace StarLab.Application.Options
{
    public interface IOptionsViewPresenter : IControlViewPresenter
    {
        void Initialise(IApplicationController controller, IDialogController parentController);
    }
}
