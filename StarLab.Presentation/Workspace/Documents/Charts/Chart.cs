using StarLab.Application.Workspace.Documents.Charts;
using System.Diagnostics;

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
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Chart"/>.</param>
        public Chart(ChartDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.PlotArea != null);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            PlotArea = new PlotArea(dto.PlotArea);
            Title = new Label(dto.Title);
            Font = new Font(dto.Font);
            
            X1 = new Axis(dto.X1);
            X2 = new Axis(dto.X2);
            Y1 = new Axis(dto.Y1);
            Y2 = new Axis(dto.Y2);
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
        /// Gets the chart title <see cref="Label"/>.
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
