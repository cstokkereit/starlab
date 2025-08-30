namespace StarLab.Application.Data.Import
{
    /// <summary>
    /// The definition of a field containing values that are composed of values from other fields.
    /// </summary>
    public interface ICompoundFieldDefinition
    {
        /// <summary>
        /// Gets an <see cref="int[]"/> containing the indices of the component fields.
        /// </summary>
        int[] Components { get; }

        /// <summary>
        /// Gets the format <see cref="string"/> that defines how the values in the component fields will be combined.
        /// </summary>
        string Format { get; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        string Name { get; }
    }
}
