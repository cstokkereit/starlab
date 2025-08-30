namespace StarLab.Data.Import
{
    /// <summary>
    /// Represents a field from a dataset.
    /// </summary>
    public interface IDataField
    {
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
