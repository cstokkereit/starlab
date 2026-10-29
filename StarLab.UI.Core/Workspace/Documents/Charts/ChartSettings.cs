using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A proxy for the <see cref="IChartSettings"> that handles conversions and updates so that the bound section controls do not need to.
    /// </summary>
    public class ChartSettings : SettingsBase<IChartSettings>
    {
        /// <summary>
        /// Gets the specified colour settings.
        /// </summary>
        /// <param name="key">A key that identifies the required settings.</param>
        /// <returns>The specified <see cref="IColourSettings"/>.</returns>
        public IColourSettings GetColourSettings(string key)
        {
            IColourSettings settings;

            switch (key)
            {
                case Constants.Chart:
                    settings = Settings;
                    break;

                case Constants.ChartPlotArea:
                    settings = Settings.PlotArea;
                    break;

                default:
                    settings = new ChartElementSettingsAdapter(Settings.GetSettings(key));
                    break;
            }

            return settings;
        }

        /// <summary>
        /// Gets the specified font settings.
        /// </summary>
        /// <param name="key">A key that identifies the required settings.</param>
        /// <returns>The specified <see cref="IFontSettings"/>.</returns>
        public IFontSettings GetFontSettings(string key)
        {
            return (IFontSettings)Settings.GetSettings(key);
        }

        /// <summary>
        /// Gets the specified label settings.
        /// </summary>
        /// <param name="key">A key that identifies the required settings.</param>
        /// <returns>The specified <see cref="ILabelSettings"/>.</returns>
        public ILabelSettings GetLabelSettings(string key)
        {
            return (ILabelSettings)Settings.GetSettings(key);
        }

        /// <summary>
        /// Gets the specified scale settings.
        /// </summary>
        /// <param name="key">A key that identifies the required settings.</param>
        /// <returns>The specified <see cref="IChartElementSettings"/>.</returns>
        public IScaleSettings GetScaleSettings(string key)
        {
            return (IScaleSettings)Settings.GetSettings(key);
        }

        /// <summary>
        /// Gets the specified chart element settings.
        /// </summary>
        /// <param name="key">A key that identifies the required settings.</param>
        /// <returns>The specified <see cref="IChartElementSettings"/>.</returns>
        public IChartElementSettings GetSettings(string key)
        {
            return Settings.GetSettings(key);
        }

        /// <summary>
        /// An adapter that allows IChartElementSettings to treated as IColourSettings.
        /// </summary>
        private class ChartElementSettingsAdapter : IColourSettings
        {
            private readonly IChartElementSettings settings;

            public ChartElementSettingsAdapter(IChartElementSettings settings)
            {
                this.settings = settings;
            }

            public string BackColour { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

            public string ForeColour { get => settings.Colour; set { settings.Colour = value; } }
        }
    }
}
