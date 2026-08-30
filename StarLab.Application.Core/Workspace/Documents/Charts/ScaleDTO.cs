namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart axis scale.
    /// </summary>
    public class ScaleDTO
    {
        public bool Autoscale;

        public string Colour = string.Empty;

        public TickMarksDTO MajorTickMarks = new TickMarksDTO();

        public double Maximum;

        public double Minimum;

        public TickMarksDTO MinorTickMarks = new TickMarksDTO();

        public bool Reversed;

        public TickLabelsDTO TickLabels = new TickLabelsDTO();

        public bool Visible;
    }
}
