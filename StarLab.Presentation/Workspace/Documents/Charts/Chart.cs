using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart.
    /// </summary>
    internal class Chart : IChart
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Chart"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the chart.</param>
        public Chart(ChartDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Font = dto.Font == null ? new Font() : new Font(dto.Font);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.DefaultBackColour : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.DefaultForeColour : dto.ForeColour;

            Title = dto.Title == null ? new Label(ForeColour, Font) : new Label(dto.Title);

            PlotArea = dto.PlotArea == null ? new PlotArea(BackColour, ForeColour) : new PlotArea(dto.PlotArea);
            
            X1 = dto.X1 == null ? new Axis(ForeColour, Font, true) : new Axis(Font, dto.X1);
            X2 = dto.X2 == null ? new Axis(ForeColour, Font, false) : new Axis(Font, dto.X2);
            Y1 = dto.Y1 == null ? new Axis(ForeColour, Font,true) : new Axis(Font, dto.Y1);
            Y2 = dto.Y2 == null ? new Axis(ForeColour, Font, false) : new Axis(Font, dto.Y2);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Chart"> class.
        /// </summary>
        public Chart()
        {
            Font = new Font();

            BackColour = Constants.DefaultBackColour;
            ForeColour = Constants.DefaultForeColour;

            Title = new Label(ForeColour, Font);

            PlotArea = new PlotArea(BackColour, ForeColour);

            X1 = new Axis(ForeColour, Font, true);
            X2 = new Axis(ForeColour, Font, false);
            Y1 = new Axis(ForeColour, Font, true);
            Y2 = new Axis(ForeColour, Font, false);
        }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the <see cref="IFont"/>.
        /// </summary>
        public IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the plot area.
        /// </summary>
        public IPlotArea PlotArea { get; }

        /// <summary>
        /// Gets the chart title.
        /// </summary>
        public ILabel Title { get; }

        /// <summary>
        /// Gets the bottom <see cref="Axis"/>.
        /// </summary>
        public IAxis X1 { get; }

        /// <summary>
        /// Gets the top <see cref="Axis"/>.
        /// </summary>
        public IAxis X2 { get; }

        /// <summary>
        /// Gets the left <see cref="Axis"/>.
        /// </summary>
        public IAxis Y1 { get; }

        /// <summary>
        /// Gets the right <see cref="Axis"/>.
        /// </summary>
        public IAxis Y2 { get; }
    }
}
