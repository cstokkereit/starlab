namespace Pegasus.Measures
{
    /// <summary>
    /// Defines the operators that act on <see cref="Measure"/>s.
    /// </summary>
    public partial struct Measure
    {
        /// <summary>
        /// Converts a <see cref="Measure"> to a <see cref="double"/> with the same numeric value.
        /// </summary>
        /// <param name="measure">The <see cref="Measure"/> being converted.</param>
        public static implicit operator double(Measure measure) => measure.GetDoubleValue();
    }
}