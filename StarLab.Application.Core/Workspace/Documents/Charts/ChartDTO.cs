namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart.
    /// </summary>
    public class ChartDTO
    {
        public string BackColour = string.Empty;

        public FontDTO Font = new FontDTO();

        public string ForeColour = string.Empty;

        public PlotAreaDTO PlotArea = new PlotAreaDTO();

        public LabelDTO Title = new LabelDTO();

        public AxisDTO X1 = new AxisDTO();

        public AxisDTO X2 = new AxisDTO();

        public AxisDTO Y1 = new AxisDTO();

        public AxisDTO Y2 = new AxisDTO();
    }
}
