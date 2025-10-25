namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// TODO
    /// </summary>
    public interface ITickLabelSettings : IVisibilitySettings
    {
        /// <summary>
        /// TODO
        /// </summary>
        IFontSettings Font { get; }

        // Number/String/DateTime Format

        /// <summary>
        /// TODO
        /// </summary>
        int Rotation { get; set; }
    }
}
