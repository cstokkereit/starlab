namespace StarLab.Application.Data
{
    /// <summary>
    /// Represents a collection of data.
    /// </summary>
    public interface IDataset : IForwardOnlyDataset
    {
        /// <summary>
        /// Gets the number of rows of data.
        /// </summary>
        int Rows { get; }

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
        /// Moves the pointer to the previous row of data.
        /// </summary>
        void MovePrevious();
    }
}
