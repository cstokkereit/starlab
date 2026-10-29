using System.Collections;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// 
    /// </summary>
    internal class OverlayGroupSettings : IOverlayGroupSettings
    {
        private readonly Dictionary<string, IOverlaySettings> overlays = new Dictionary<string, IOverlaySettings>(); // A dictionary that holds the chart overlays indexed by name.

        /// <summary>
        /// Gets the specified chart overlay settings.
        /// </summary>
        /// <param name="name">The name of the chart overlay.</param>
        /// <returns>The settings for the specified chart overlay.</returns>
        public IOverlaySettings this[string name] { get => overlays[name]; }

        /// <summary>
        /// Gets or sets the colour of the chart overlays.
        /// </summary>
        public string Colour 
        {
            get
            {
                var colours = new List<string>();

                foreach (var overlay in overlays.Values)
                {
                    if (overlay.Visible)
                    {
                        colours.Add(overlay.Colour);
                    }
                }

                return colours.Count == 0 ? string.Empty : colours.GroupBy(a => a).OrderByDescending(b => b.Count()).First().Key;
            }

            set
            {
                foreach (var overlay in overlays.Values)
                {
                    overlay.Colour = value;
                }
            }
        }

        /// <summary>
        /// Gets the number of chart overlays.
        /// </summary>
        public int Count => overlays.Count;

        /// <summary>
        /// Gets or sets a flag that determines whether the chart overlays are visible.
        /// </summary>
        public bool Visible 
        {
            get
            {
                foreach (var overlay in overlays.Values)
                {
                    if (overlay.Visible)
                    {
                        return true;
                    }
                }

                return false;
            }

            set
            {
                foreach (var overlay in overlays.Values)
                {
                    overlay.Visible = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the font for the chart overlay labels.
        /// </summary>
        public IFont Font 
        {
            get
            {
                var overlays = new List<IOverlaySettings>();

                foreach (var overlay in this.overlays.Values)
                {
                    if (overlay.Visible)
                    {
                        overlays.Add(overlay);
                    }
                }

                return overlays.GroupBy(a => a.Font).OrderByDescending(b => b.Count()).First().Key;
            }

            set
            {
                foreach (var overlay in overlays.Values)
                {
                    overlay.Font = value;
                }
            }
        }

        /// <summary>
        /// Adds the chart overlay settings provided to the group.
        /// </summary>
        /// <param name="overlay">The overlay settings to add.</param>
        public void Add(IOverlaySettings overlay)
        {
            overlays.Add(overlay.Name, overlay);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the chart overlays in the group.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{IOverlaySettings}"/> for the group.</returns>
        public IEnumerator<IOverlaySettings> GetEnumerator()
        {
            return new List<IOverlaySettings>(overlays.Values).GetEnumerator();
        }

        /// <summary>
        /// Sets the font for the chart overlay labels.
        /// </summary>
        /// <param name="family">The name of the font family.</param>
        /// <param name="size">The font size.</param>
        /// <param name="bold">A flag indiciating whether the font is bold.</param>
        /// <param name="italic">A flag indiciating whether the font has the italic style applied.</param>
        /// <param name="underline">A flag indiciating whether the font is underlined.</param>
        public void SetFont(string family, int size, bool bold, bool italic, bool underline)
        {
            Font = new Font(family, size, bold, italic, underline);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the chart overlays in the group.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> for the group.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
