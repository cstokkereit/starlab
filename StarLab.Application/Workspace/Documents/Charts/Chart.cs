using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Application model representation of a chart.
    /// </summary>
    internal class Chart
    {
        private readonly Dictionary<string, Overlay> overlays = new Dictionary<string, Overlay>(); // A dictionary containing the chart overlays.

        /// <summary>
        /// Initialises a new instance of the <see cref="Chart"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Chart"/>.</param>
        public Chart(ChartDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.DefaultBackColour : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.DefaultForeColour : dto.ForeColour;

            PlotArea = new PlotArea(dto.PlotArea);
            Title = new Label(dto.Title);
            Font = new Font(dto.Font);
            X1 = new Axis(dto.X1);
            X2 = new Axis(dto.X2);
            Y1 = new Axis(dto.Y1);
            Y2 = new Axis(dto.Y2);

            CreateOverlays(dto.Overlays);
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
        /// Gets an <see cref="IEnumerable{IFolder}"/> containing all of the folders within the project hierarchy.
        /// </summary>
        public IEnumerable<Overlay> Overlays => overlays.Values;

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

        /// <summary>
        /// Adds the specified overlay to the chart.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        /// <param name="font">The overlay font.</param>
        /// <param name="colour">The overlay colour.</param>
        public void AddOverlay(string name, Font font, string colour)
        {
            if (!overlays.ContainsKey(name))
            {
                //throw new InvalidOperationException(ExceptionMessages.OverlayExists(name)); // TODO : Need to fix this

                overlays.Add(name, new Overlay(name, font, colour));
            }
        }

        /// <summary>
        /// Removes the specified overlay from the chart.
        /// </summary>
        /// <param name="name">The overlay name.</param>
        public void RemoveOverlay(string name)
        {
            overlays.Remove(name);
        }

        /// <summary>
        /// Creates the overlays that belong to this chart from the <see cref="OverlayDTO"/>s provided.
        /// </summary>
        /// <param name="dtos">An <see cref="IEnumerable{OverlayDTO}"/> that contains the dtos.</param>
        private void CreateOverlays(IEnumerable<OverlayDTO> dtos)
        {
            foreach (var dto in dtos)
            {
                var overlay = new Overlay(dto);

                overlays.Add(overlay.Name, overlay);
            }
        }
    }
}
