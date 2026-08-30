namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart axis.
    /// </summary>
    public class AxisDTO
    {
        public string Colour = string.Empty;

        public LabelDTO Label = new LabelDTO();

        public ScaleDTO Scale = new ScaleDTO();

        public bool Visible;
    }
}
