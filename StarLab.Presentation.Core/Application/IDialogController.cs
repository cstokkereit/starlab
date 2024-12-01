namespace StarLab.Application
{
    public interface IDialogController : IViewController
    {
        void Close(); // Do the same for chart settings??

        void Show();
    }
}
