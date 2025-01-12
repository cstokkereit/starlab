namespace StarLab.Application.Configuration
{
    public interface IChildViewConfiguration
    {
        int Panel { get; }

        string Presenter { get; }

        string View { get; }
    }
}
