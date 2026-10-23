using StarLab.Presentation;
using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// The base class for all chart overlays.
    /// </summary>
    internal abstract class Overlay
    {
        private readonly Plot plot; // The plot that displays the overlay.

        private bool visible; // A flag that indicates whether the overlay is visable.

        private ScottPlot.Color colour; // The overlay colour.

        private Font font; // The overlay font.

        private bool enabled; // A flag that determines whether the overlay is enabled.

        /// <summary>
        /// Initialises a new instance of the <see cref="Overlay"> class.
        /// </summary>
        /// <param name="plot">The <see cref="Plot"/> that displays the overlay.</param>
        /// <param name="name">The overlay name.</param>
        public Overlay(Plot plot, string name)
        {
            this.plot = plot ?? throw new ArgumentNullException(nameof(plot));

            colour = ScottPlot.Color.FromColor(System.Drawing.Color.FromName(Constants.DefaultForeColour));

            font = new Font(Constants.DefaultFontFamily, Constants.DefaultFontSize);

            visible = true;

            UpdateElements();

            Enabled = true;

            Name = name;
        }

        /// <summary>
        /// Gets or sets the overlay colour.
        /// </summary>
        public ScottPlot.Color Colour 
        {
            get => colour;
            
            set
            {
                colour = value;
                UpdateElements();
            }
        }

        /// <summary>
        /// Gets or sets the enabled status.
        /// </summary>
        public bool Enabled 
        {
            get => enabled;
            
            set
            {
                enabled = value;

                if (!enabled)
                {
                    Clear(plot);
                }
            }
        }

        /// <summary>
        /// Gets or sets the overlay font.
        /// </summary>
        public Font Font 
        {
            get => font;
            
            set
            {
                font = value;
                UpdateElements();
            }
        }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets a flag indicating that the overlay is visible.
        /// </summary>
        public bool Visible 
        {
            get => visible;

            set
            {
                visible = value;
                UpdateElements();
            }
        }

        /// <summary>
        /// Renders the overlay.
        /// </summary>
        public void Render()
        {
            if (Enabled && Visible)
            {
                Render(plot);
            }
        }

        /// <summary>
        /// Removes the elements that compose the overlay from the chart.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected abstract void Clear(Plot plot);

        /// <summary>
        /// Renders the overlay on the specified <see cref="Plot"/>.
        /// </summary>
        /// <param name="plot">The plot that displays the overlay.</param>
        protected abstract void Render(Plot plot);

        /// <summary>
        /// Updates the elements that compose the overlay.
        /// </summary>
        protected abstract void UpdateElements();
    }
}
