namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents the tick labels on a chart axis.
    /// </summary>
    public class TickLabelsDTO
    {
        public string Colour = string.Empty;

        public FontDTO Font = new FontDTO();

        public int Rotation;

        public bool Visible;
    }
}
