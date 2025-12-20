namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents the chart grid.
    /// </summary>
    public class GridDTO
    {
        public string? Colour;

        public GridLinesDTO? MajorGridLines;

        public GridLinesDTO? MinorGridLines;

        public bool Visible;
    }
}
