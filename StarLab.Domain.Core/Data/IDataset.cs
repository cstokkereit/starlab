namespace StarLab.Data
{
    /// <summary>
    /// Represents a collection of data.
    /// </summary>
    public interface IDataset : IDisposable
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
        /// <param name="index">The index of the field.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified index.</returns>
        object GetValue(int index);

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified name.</returns>
        object GetValue(string name);

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <param name="field">The <see cref="IDataField"/> that contains the required value.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified name.</returns>
        object GetValue(IDataField field);

        /// <summary>
        /// Moves the pointer to the specified row index.
        /// </summary>
        /// <param name="index">The new row index.</param>
        void Move(int index);

        /// <summary>
        /// Moves the pointer to the start of the dataset.
        /// </summary>
        void MoveFirst();

        /// <summary>
        /// Moves the pointer to the end of the dataset.
        /// </summary>
        void MoveLast();

        /// <summary>
        /// Moves the pointer to the next row of data.
        /// </summary>
        void MoveNext();

        /// <summary>
        /// Moves the pointer to the previous row of data.
        /// </summary>
        void MovePrevious();
    }
}
