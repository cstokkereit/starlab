namespace StarLab.Application.Configuration
{
    public interface IViewConfiguration
    {
        IList<IChildViewConfiguration> ChildViews { get; }

        string Name { get; }

        ViewTypes Type { get; }

        IChildViewConfiguration GetChildViewConfiguration(string name);
    }
}
