namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a chart document.
    /// </summary>
    public class ChartDTO
    {
        public int BackColor;

        public int ForeColor;

        public GridDTO? MajorGrid;

        public GridDTO? MinorGrid;

        public string? Name;

        public string? Path;

        public string? Type;

        public AxisDTO? XAxis;

        public AxisDTO? XAxis2;

        public AxisDTO? YAxis;

        public AxisDTO? YAxis2;
    }
}
