namespace StarLab.Domain
{
    /// <summary>
    /// Represents a collection of data.
    /// </summary>
    public interface IDataset : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        bool BOF { get; }

        /// <summary>
        /// Returns <see cref="true"/> if the end of the data has been reached; <see cref="false"/> otherwise.
        /// </summary>
        bool EOF { get; }

        /// <summary>
        /// An <see cref="IEnumerable{IDataField}"/> containing the available <see cref="IDataField"/>s.
        /// </summary>
        IEnumerable<IDataField> Fields { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        object GetValue(int index);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        object GetValue(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        object GetValue(IDataField field);

        void Move(int index);

        void MoveFirst();

        void MoveLast();

        /// <summary>
        /// Advances the data set to the next row of data.
        /// </summary>
        void MoveNext();

        void MovePrevious();
    }
}
