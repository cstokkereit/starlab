namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A data transfer object that represents a font.
    /// </summary>
    public class FontDTO
    {
        public bool Bold;

        public bool Italic;

        public string Family = Constants.DefaultFontFamily;

        public int Size = Constants.DefaultFontSize;

        public bool Underline;
    }
}
