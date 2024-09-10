using StarLab.Presentation;

namespace StarLab.UI
{
    internal class ViewTypeResolver
    {
        private readonly Dictionary<string, string> typeNamesByView = new Dictionary<string, string>();

        public ViewTypeResolver() 
        {
            Initialise();
        }

        public string Resolve(string view)
        {
            var type = string.Empty;

            if (typeNamesByView.ContainsKey(view))
            {
                type = typeNamesByView[view];
            }

            return type;
        }

        private void Initialise()
        {
            typeNamesByView.Add(Views.ABOUT, "StarLab.UI.Help.AboutView");
            typeNamesByView.Add(Views.COLOUR_MAGNITUDE_CHART, "StarLab.UI.Charts.ColourMagnitudeChartView");
            typeNamesByView.Add(Views.CHART_SETTINGS, "StarLab.UI.Charts.ChartSettingsView");
            typeNamesByView.Add(Views.DOCUMENT, "StarLab.UI.Docking.DocumentView");
            typeNamesByView.Add(Views.OPTIONS, "StarLab.UI.Options.OptionsView");
            typeNamesByView.Add(Views.SPLIT_CONTAINER, "StarLab.UI.SplitView");
            typeNamesByView.Add(Views.TOOL, "StarLab.UI.Docking.ToolView");
            typeNamesByView.Add(Views.WORKSPACE, "StarLab.UI.Workspaces.WorkspaceView");
            typeNamesByView.Add(Views.WORKSPACE_EXPLORER, "StarLab.UI.Workspaces.WorkspaceExplorer.WorkspaceExplorerView");
        }
    }
}
