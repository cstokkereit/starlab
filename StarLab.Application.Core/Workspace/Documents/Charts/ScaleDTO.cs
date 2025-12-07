namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart axis scale.
    /// </summary>
    public class ScaleDTO
    {
        public bool Autoscale;

        public string? BackColour;

        public string? ForeColour;

        public TickMarksDTO? MajorTickMarks;

        public double Maximum;

        public double Minimum;

        public TickMarksDTO? MinorTickMarks;

        public bool Reversed;

        public TickLabelsDTO? TickLabels;

        public bool Visible;
    }
}
