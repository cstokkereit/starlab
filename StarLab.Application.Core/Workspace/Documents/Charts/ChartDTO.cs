namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart.
    /// </summary>
    public class ChartDTO
    {
        public string? BackColour;

        public FontDTO? Font;

        public string? ForeColour;

        public PlotAreaDTO? PlotArea;

        public LabelDTO? Title;

        public AxisDTO? X1;

        public AxisDTO? X2;

        public AxisDTO? Y1;

        public AxisDTO? Y2;
    }
}
