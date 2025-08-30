namespace StarLab.Data.Import
{
    /// <summary>
    /// Represents a converter that can be used to convert a <see cref="string"/> representation of a value to the data type specified in the field definition.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Converts the <see cref="string"/> value provided to the data type specified in the field definition.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to be converted.</param>
        /// <returns>An <see cref="object"/> that holds the converted value.</returns>
        object Convert(string value);
    }
}
