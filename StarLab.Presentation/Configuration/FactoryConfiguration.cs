using StarLab.Shared.Properties;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Provides the type configuration information for the view and presenter factories.
    /// </summary>
    public class FactoryConfiguration : IFactoryConfiguration
    {
        private readonly Dictionary<string, IViewConfiguration> configurations = new Dictionary<string, IViewConfiguration>(); // A dictionary containing the available view configurations indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="FactoryConfiguration"> class.
        /// </summary>
        public FactoryConfiguration()
        {
            CreateDialogViewConfigurations();
            CreateDocumentViewConfigurations();
            CreateToolViewConfigurations();
        }

        /// <summary>
        /// Gets the specified <see cref="IChildViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IChildViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/> instance.</returns>
        /// <exception cref="ArgumentException"></exception>
        public IChildViewConfiguration GetChildConfiguration(string name)
        {
            if (configurations.TryGetValue(name, out IViewConfiguration? configuration))
            {
                var childConfigurations = new List<IChildViewConfiguration>(configuration.ChildConfigurations);

                return childConfigurations[0];
            }

            throw new ArgumentException(string.Format(Resources.ConfigurationNotFound, name), nameof(name));
        }

        /// <summary>
        /// Gets the specified <see cref="IViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the reuired <see cref="IViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/> instance.</returns>
        /// <exception cref="ArgumentException"></exception>
        public IViewConfiguration GetConfiguration(string name)
        {
            if (configurations.TryGetValue(name, out IViewConfiguration? configuration))
            {
                return configuration;
            }

            throw new ArgumentException(string.Format(Resources.ConfigurationNotFound, name), nameof(name));
        }

        /// <summary>
        /// Adds the <see cref="IViewConfiguration"/> provided to the dictionary containing the view configurations.
        /// </summary>
        /// <param name="configuration">The <see cref="IViewConfiguration"/> to be added.</param>
        private void Add(IViewConfiguration configuration)
        {
            configurations.Add(configuration.Name, configuration);
        }

        /// <summary>
        /// Creates the dialog view configurations and adds them to the dictionary.
        /// </summary>
        private void CreateDialogViewConfigurations()
        {
            Add(new ViewConfiguration(ViewNames.About, ViewTypes.Dialog)
                .AddChild(ViewNames.About, "StarLab.UI.Help.AboutView, StarLab.UI", "StarLab.Presentation.Help.AboutViewPresenter, StarLab.Presentation"));

            Add(new ViewConfiguration(ViewNames.AddDocument, ViewTypes.Dialog)
                .AddChild(ViewNames.AddDocument, "StarLab.UI.Workspace.Documents.AddDocumentView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.AddDocumentViewPresenter, StarLab.Presentation"));
            Add(new ViewConfiguration(ViewNames.Options, ViewTypes.Dialog)
                .AddChild(ViewNames.Options, "StarLab.UI.Options.OptionsView, StarLab.UI", "StarLab.Presentation.Options.OptionsViewPresenter, StarLab.Presentation"));
        }

        /// <summary>
        /// Creates the document view configurations and adds them to the dictionary.
        /// </summary>
        private void CreateDocumentViewConfigurations()
        {
            Add(new ViewConfiguration(ViewNames.ColourMagnitudeDiagram, ViewTypes.Document)
                .AddChild(ViewNames.ChartSettings, SplitViewPanels.Panel1, "StarLab.UI.Workspace.Documents.Charts.ChartSettingsView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Charts.ChartSettingsViewPresenter, StarLab.Presentation")
                .AddChild(ViewNames.Chart, SplitViewPanels.Panel2, "StarLab.UI.Workspace.Documents.Charts.ChartView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Charts.ColourMagnitudeChartViewPresenter, StarLab.Presentation"));

            Add(new ViewConfiguration(ViewNames.Table, ViewTypes.Document)
                .AddChild(ViewNames.TableSettings, SplitViewPanels.Panel1, "StarLab.UI.Workspace.Documents.Tables.TableSettingsView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Tables.TableSettingsViewPresenter, StarLab.Presentation")
                .AddChild(ViewNames.Table, SplitViewPanels.Panel2, "StarLab.UI.Workspace.Documents.Tables.TableView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Tables.TableViewPresenter, StarLab.Presentation"));

            Add(new ViewConfiguration(ViewNames.TwoColourDiagram, ViewTypes.Document)
                .AddChild(ViewNames.ChartSettings, SplitViewPanels.Panel1, "StarLab.UI.Workspace.Documents.Charts.ChartSettingsView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Charts.ChartSettingsViewPresenter, StarLab.Presentation")
                .AddChild(ViewNames.Chart, SplitViewPanels.Panel2, "StarLab.UI.Workspace.Documents.Charts.ChartView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Charts.TwoColourChartViewPresenter, StarLab.Presentation"));
        }

        /// <summary>
        /// Creates the tool view configurations and adds them to the dictionary.
        /// </summary>
        private void CreateToolViewConfigurations()
        {
            Add(new ViewConfiguration(ViewNames.WorkspaceExplorer, ViewTypes.Dialog)
                .AddChild(ViewNames.WorkspaceExplorer, "StarLab.UI.Workspace.WorkspaceExplorer.WorkspaceExplorerView, StarLab.UI", "StarLab.Presentation.Workspace.WorkspaceExplorer.WorkspaceExplorerViewPresenter, StarLab.Presentation"));
        }
    }
}
