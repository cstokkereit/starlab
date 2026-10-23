using StarLab.Shared;

namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Provides the type configuration information for the view and presenter factories.
    /// </summary>
    public class FactoryConfiguration : IFactoryConfiguration
    {
        private readonly Dictionary<string, IOverlayConfiguration> overlays = new Dictionary<string, IOverlayConfiguration>(); // A dictionary containing the available overlay configurations indexed by name.

        private readonly Dictionary<string, IViewConfiguration> views = new Dictionary<string, IViewConfiguration>(); // A dictionary containing the available view configurations indexed by name.

        /// <summary>
        /// Initialises a new instance of the <see cref="FactoryConfiguration"> class.
        /// </summary>
        public FactoryConfiguration()
        {
            CreateDialogViewConfigurations();
            CreateDocumentViewConfigurations();
            CreateToolViewConfigurations();

            CreateOverlayConfigurations();
        }

        /// <summary>
        /// Gets the names of the available overlays.
        /// </summary>
        public IEnumerable<string> Overlays => overlays.Keys;

        /// <summary>
        /// Gets the specified <see cref="IOverlayConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IOverlayConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IOverlayConfiguration"/> instance.</returns>
        public IOverlayConfiguration GetOverlayConfiguration(string name)
        {
            if (overlays.TryGetValue(name, out IOverlayConfiguration? configuration))
            {
                return configuration;
            }

            throw new KeyNotFoundException(ExceptionMessages.ConfigurationNotFound(name));
        }

        /// <summary>
        /// Gets the specified <see cref="IViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/> instance.</returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public IViewConfiguration GetViewConfiguration(string name)
        {
            if (views.TryGetValue(name, out IViewConfiguration? configuration))
            {
                return configuration;
            }

            throw new KeyNotFoundException(ExceptionMessages.ConfigurationNotFound(name));
        }

        /// <summary>
        /// Adds the <see cref="IOverlayConfiguration"/> provided to the dictionary containing the view configurations.
        /// </summary>
        /// <param name="configuration">The <see cref="IOverlayConfiguration"/> to be added.</param>
        private void Add(IOverlayConfiguration configuration)
        {
            overlays.Add(configuration.Name, configuration);
        }

        /// <summary>
        /// Adds the <see cref="IViewConfiguration"/> provided to the dictionary containing the view configurations.
        /// </summary>
        /// <param name="configuration">The <see cref="IViewConfiguration"/> to be added.</param>
        private void Add(IViewConfiguration configuration)
        {
            views.Add(configuration.Name, configuration);
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
                .AddChild(ViewNames.OverlayManager, SplitViewPanels.Panel1, "StarLab.UI.Workspace.Documents.Charts.Overlays.OverlayManagerView, StarLab.UI", "StarLab.Presentation.Workspace.Documents.Charts.Overlays.OverlayManagerViewPresenter, StarLab.Presentation")
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
        /// Creates the overlay configurations and adds them to the dictionary.
        /// </summary>
        private void CreateOverlayConfigurations()
        {
            Add(new OverlayConfiguration(Constants.SunOverlay, "StarLab.UI.Workspace.Documents.Charts.Overlays.PointOverlay, StarLab.UI", string.Empty));



            // Brightest stars
            // Stellar types e.g. Wolf rayet, brown dwarf
            // Evolutionary tracks (could prove difficult - can they be calculated?)
            // Standard stars for spectral and magnitude classes

            // Radius (R/Rsun)
            // Mass (M/Msun)
            // Diameter (D/Dsun

            // Custom overlays - for the future
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
