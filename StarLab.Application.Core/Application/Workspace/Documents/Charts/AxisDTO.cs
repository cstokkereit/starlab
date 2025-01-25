namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart axis.
    /// </summary>
    public class AxisDTO
    {
        public int Color;

        public FontDTO? Font;

        public double Interval;

        public bool IsReversed;

        public double Maximum;

        public double Minimum;

        public TitleDTO? Title;

        public bool Visible;
    }
}
