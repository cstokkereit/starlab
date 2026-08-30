namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A data transfer object that represents a label.
    /// </summary>
    public class LabelDTO
    {
        public string Colour = string.Empty;

        public FontDTO Font = new FontDTO();

        public string Text = string.Empty;

        public bool Visible;
    }
}
