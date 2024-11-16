namespace StarLab.Application
{
    public interface IViewBundle
    {
        IViewController Controller { get; }

        IView View { get; }
    }
}
