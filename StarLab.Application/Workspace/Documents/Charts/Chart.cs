using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart.
    /// </summary>
    internal class Chart
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Chart"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Chart"/>.</param>
        public Chart(ChartDTO dto)
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

            Debug.Assert(dto.PlotArea != null);
            Debug.Assert(dto.Title != null);
            Debug.Assert(dto.Font != null);
            Debug.Assert(dto.X1 != null);
            Debug.Assert(dto.X2 != null);
            Debug.Assert(dto.Y1 != null);
            Debug.Assert(dto.Y2 != null);

            if (string.IsNullOrEmpty(dto.BackColour))
            {
                BackColour = Constants.DefaultBackColour;
            }
            else
            {
                BackColour = dto.BackColour;
            }

            if (string.IsNullOrEmpty(dto.ForeColour))
            {
                ForeColour = Constants.DefaultForeColour;
            }
            else
            {
                ForeColour = dto.ForeColour;
            }

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
        /// Gets the font.
        /// </summary>
        public Font Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the plot area.
        /// </summary>
        public PlotArea PlotArea { get; }

        /// <summary>
        /// Gets the chart title label.
        /// </summary>
        public Label Title { get; }

        /// <summary>
        /// Gets the bottom axis.
        /// </summary>
        public Axis X1 { get; }

        /// <summary>
        /// Gets the top axis.
        /// </summary>
        public Axis X2 { get; }

        /// <summary>
        /// Gets the left axis.
        /// </summary>
        public Axis Y1 { get; }

        /// <summary>
        /// Gets the right axis.
        /// </summary>
        public Axis Y2 { get; }
    }
}
