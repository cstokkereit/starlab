namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A data transfer object that represents a chart overlay.
    /// </summary>
    public class OverlayDTO
    {
        public OverlayDTO()
        {
            Colour = string.Empty;
            Font = new FontDTO();
            Name = string.Empty;
        }

        public string Colour;

        public FontDTO Font;

        public string Name;

        public OverlayType Type;

        public bool Visible;
    }
}
