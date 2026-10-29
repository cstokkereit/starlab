using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart.
    /// </summary>
    public class ChartDTO
    {
        public ChartDTO()
        {
            Overlays = new List<OverlayDTO>();
            PlotArea = new PlotAreaDTO();
            Title = new LabelDTO();
            Font = new FontDTO();
            X1 = new AxisDTO();
            X2 = new AxisDTO();
            Y1 = new AxisDTO();
            Y2 = new AxisDTO();

            BackColour = string.Empty;
            ForeColour = string.Empty;
        }

        public string BackColour;

        public FontDTO Font;

        public string ForeColour;

        public List<OverlayDTO> Overlays;

        public PlotAreaDTO PlotArea;

        public LabelDTO Title;

        public AxisDTO X1;

        public AxisDTO X2;

        public AxisDTO Y1;

        public AxisDTO Y2;
    }
}
