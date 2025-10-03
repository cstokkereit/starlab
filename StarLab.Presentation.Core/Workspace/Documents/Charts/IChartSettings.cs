namespace StarLab.Presentation.Workspace.Documents.Charts
{
    public interface IChartSettings : IColourSettings
    {
        IAxesSettings Axes { get; }

        IFontSettings Font { get; }

        ILabelSettings Title { get; }
    }
}
