using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the foreground and background colour of chart elements.
    /// </summary>
    public partial class ColourSection : UserControl, ISettingsSection
    {
        private const string AXES = $"{Constants.Chart}/{Constants.Axes}";
        private const string CHART = Constants.Chart;
        private const string TITLE = $"{Constants.Chart}/{Constants.Title}";
        private const string X1 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisX1}";
        private const string X1_LABEL = $"{X1}/{Constants.Label}";
        private const string X2 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisX2}";
        private const string X2_LABEL = $"{X2}/{Constants.Label}";
        private const string Y1 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisY1}";
        private const string Y1_LABEL = $"{Y1}/{Constants.Label}";
        private const string Y2 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisY2}";
        private const string Y2_LABEL = $"{Y2}/{Constants.Label}";

        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        /// <param name="group">The name of the settings group that this control represents.</param>
        public ColourSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;

            var converter = new ColorConverter();
            var colours = converter.GetStandardValues();

            if (colours != null)
            {
                foreach (var colour in colours)
                {
                    comboBoxBackground.Items.Add(colour);
                    comboBoxForeground.Items.Add(colour);
                }
            }

            var colourSettings = GetSettings();

            comboBoxForeground.SelectedIndex = comboBoxForeground.FindStringExact(colourSettings.ForeColour);
            comboBoxBackground.SelectedIndex = comboBoxBackground.FindStringExact(colourSettings.BackColour);

            AttachEventHandlers();
        }

        /// <summary>
        /// Attaches the event handlers for the child <see cref="Control"/>s that comprise this <see cref="UserControl"/>
        /// </summary>
        private void AttachEventHandlers()
        {
            comboBoxForeground.TextChanged += OnColourChanged;
            comboBoxBackground.TextChanged += OnColourChanged;

            //buttonCustom1.Click += OnShowCustomForegroundColourDialog;
            //buttonCustom2.Click += OnShowCustomBackgroundColourDialog;
        }

        /// <summary>
        /// Gets the <see cref="IColourSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IColourSettings"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IColourSettings GetSettings()
        {
            IColourSettings? settings = null;

            switch (group)
            {
                case AXES:
                    settings = this.settings.Axes;
                    break;

                case CHART:
                    settings = this.settings;
                    break;

                case TITLE:
                    settings = this.settings.Title;
                    break;

                case X1:
                    settings = this.settings.Axes.X1;
                    break;

                case X1_LABEL:
                    settings = this.settings.Axes.X1.Label;
                    break;

                case X2:
                    settings = this.settings.Axes.X2;
                    break;

                case X2_LABEL:
                    settings = this.settings.Axes.X2.Label;
                    break;

                case Y1:
                    settings = this.settings.Axes.Y1;
                    break;

                case Y1_LABEL:
                    settings = this.settings.Axes.Y1.Label;
                    break;

                case Y2:
                    settings = this.settings.Axes.Y2;
                    break;

                case Y2_LABEL:
                    settings = this.settings.Axes.Y2.Label;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(group), group);
            }

            return settings;
        }

        /// <summary>
        /// Event handler for the <see cref="ComboBox.TextChanged"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnColourChanged(object? sender, EventArgs e)
        {
            var colourSettings = GetSettings();

            colourSettings.ForeColour = comboBoxForeground.Text;
            colourSettings.BackColour = comboBoxBackground.Text;

            SectionChanged?.Invoke(this, settings);
        }

        //private void OnShowCustomForegroundColourDialog(object? sender, EventArgs e)
        //{
        //    var colour = dialogCustomColour.ShowDialog(this);
        //}

        //private void OnShowCustomBackgroundColourDialog(object? sender, EventArgs e)
        //{
        //    var colour = dialogCustomColour.ShowDialog(this);
        //}
    }
}
