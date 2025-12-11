namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents the chart grid.
    /// </summary>
    public class GridDTO
    {
        public string? BackColour;

        public string? ForeColour;

        public GridLinesDTO? MajorGridLines;

        public GridLinesDTO? MinorGridLines;

        public bool Visible;
    }
}
