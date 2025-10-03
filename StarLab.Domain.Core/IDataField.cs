namespace StarLab.Domain
{
    /// <summary>
    /// Represents a field from a dataset.
    /// </summary>
    public interface IDataField
    {
        /// <summary>
        /// Gets the index of the field.
        /// </summary>
        int Index { get; }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the value of the field from the current row in the dataset.
        /// </summary>
        object Value { get; }
    }
}
