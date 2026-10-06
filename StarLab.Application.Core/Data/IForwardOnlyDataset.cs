namespace StarLab.Application.Data
{
    /// <summary>
    /// Represents a collection of data.
    /// </summary>
    public interface IForwardOnlyDataset : IDisposable
    {
        /// <summary>
        /// A flag that indicates that the current row index is before the start of the dataset.
        /// </summary>
        bool BOF { get; }

        /// <summary>
        /// A flag that indicates that the current row index is beyond the end of the dataset.
        /// </summary>
        bool EOF { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IDataField}"/> that contains the available data fields.
        /// </summary>
        IEnumerable<IDataField> Fields { get; }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="index">The index of the field.</param>
        /// <returns>The value of the field with the specified index.</returns>
        T GetValue<T>(int index);

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field with the specified name.</returns>
        T GetValue<T>(string name);

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="field">The <see cref="IDataField"/> that contains the required value.</param>
        /// <returns>The value of the specified <see cref="IDataField">.</returns>
        T GetValue<T>(IDataField field);

        /// <summary>
        /// Moves the pointer to the next row of data.
        /// </summary>
        void MoveNext();
    }
}
