namespace StarLab.Domain.Data
{
    /// <summary>
    /// Defines the contract for accessing entity data.
    /// </summary>
    public interface IEntityData
    {
        /// <summary>
        /// Gets the double value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        double GetDoubleValue(string field);

        /// <summary>
        /// Gets the integer value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        int GetIntegerValue(string field);

        /// <summary>
        /// Gets the long value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        int GetLongValue(string field);

        /// <summary>
        /// Gets the string value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        string GetStringValue(string field);
    }
}
