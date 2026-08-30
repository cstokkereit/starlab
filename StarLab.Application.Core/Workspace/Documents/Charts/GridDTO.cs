namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents the chart grid.
    /// </summary>
    public class GridDTO
    {
        public string Colour = string.Empty;

        public GridLinesDTO MajorGridLines = new GridLinesDTO();

        public GridLinesDTO MinorGridLines = new GridLinesDTO();

        public bool Visible;
    }
}
