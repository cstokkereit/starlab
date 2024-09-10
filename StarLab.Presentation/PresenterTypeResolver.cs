namespace StarLab.Presentation
{
    internal class PresenterTypeResolver
    {
        private readonly Dictionary<string, string> typeNamesByView = new Dictionary<string, string>();

        public PresenterTypeResolver()
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
            typeNamesByView.Add(Views.ABOUT, "StarLab.Presentation.Help.AboutViewPresenter");
            typeNamesByView.Add(Views.CHART_SETTINGS, "StarLab.Presentation.Charts.ChartSettingsViewPresenter");
            typeNamesByView.Add(Views.COLOUR_MAGNITUDE_CHART, "StarLab.Presentation.Charts.ColourMagnitudeChartViewPresenter");
            typeNamesByView.Add(Views.OPTIONS, "StarLab.Presentation.Options.OptionsViewPresenter");
            typeNamesByView.Add(Views.WORKSPACE, "StarLab.Presentation.Workspaces.WorkspaceViewPresenter");
            typeNamesByView.Add(Views.WORKSPACE_EXPLORER, "StarLab.Presentation.Workspaces.WorkspaceExplorer.WorkspaceExplorerViewPresenter");
        }
    }
}
