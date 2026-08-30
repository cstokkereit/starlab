namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents the chart plot area.
    /// </summary>
    public class PlotAreaDTO
    {
        public string BackColour = string.Empty;

        public string ForeColour = string.Empty;

        public GridDTO Grid = new GridDTO();

        public bool Visible;
    }
}
