namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A data transfer object that represents a chart overlay.
    /// </summary>
    public class OverlayDTO
    {
        public string Colour = string.Empty;

        public FontDTO Font = new FontDTO();

        public string Name = string.Empty;

        public OverlayType Type;

        public bool Visible;
    }
}
