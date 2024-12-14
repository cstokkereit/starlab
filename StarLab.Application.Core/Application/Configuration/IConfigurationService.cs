namespace StarLab.Application.Configuration
{
    public interface IConfigurationService
    {
        string Workspace { get; set; }

        IViewConfiguration GetViewConfiguration(string name);

        void Initialise();
    }
}
