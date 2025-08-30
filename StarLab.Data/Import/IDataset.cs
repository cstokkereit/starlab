namespace StarLab.Data.Import
{
    /// <summary>
    /// Represents a collection of data.
    /// </summary>
    public interface IDataset : IDisposable
    {
        /// <summary>
        /// Returns <see cref="true"/> if the end of the data has been reached; <see cref="false"/> otherwise.
        /// </summary>
        bool EOF { get; }

        /// <summary>
        /// An <see cref="IEnumerable{IDataField}"/> containing the available <see cref="IDataField"/>s.
        /// </summary>
        IEnumerable<IDataField> Fields { get; }

        /// <summary>
        /// Advances the data set to the next row of data.
        /// </summary>
        void MoveNext();
    }
}
