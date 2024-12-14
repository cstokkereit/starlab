namespace StarLab.Application.Configuration
{
    public interface IViewConfiguration
    {
        IList<IContentConfiguration> Contents { get; }

        string Name { get; }

        ViewTypes Type { get; }

        IContentConfiguration GetContentConfiguration(string name);
    }
}
