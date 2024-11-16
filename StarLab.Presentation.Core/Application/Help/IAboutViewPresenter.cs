namespace StarLab.Application.Help
{
    public interface IAboutViewPresenter : IControlViewPresenter
    {
        void Initialise(IApplicationController controller, IDialogController parentController);
    }
}
