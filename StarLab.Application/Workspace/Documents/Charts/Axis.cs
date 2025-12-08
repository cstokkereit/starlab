using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart axis.
    /// </summary>
    internal class Axis
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Axis"/>.</param>
        public Axis(AxisDTO dto) 
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

            Debug.Assert(dto.Label != null);
            Debug.Assert(dto.Scale != null);

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

            Label = new Label(dto.Label);
            Scale = new Scale(dto.Scale);
            Visible = dto.Visible;
        }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the axis label.
        /// </summary>
        public Label Label { get; }

        /// <summary>
        /// Gets the axis scale.
        /// </summary>
        public Scale Scale { get; }

        /// <summary>
        /// A flag indicating whether the axis is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
