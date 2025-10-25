namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart axis scale.
    /// </summary>
    internal class Scale
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Scale"/>.</param>
        public Scale(ScaleDTO dto)
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

            Reversed = dto.Reversed;
            Maximum = dto.Maximum;
            Minimum = dto.Minimum;
        }

        /// <summary>
        /// Gets the maimum value.
        /// </summary>
        public double Maximum { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public double Minimum { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; }
    }
}
