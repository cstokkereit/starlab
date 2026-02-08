namespace StarLab.Data
{
    /// <summary>
    /// Represents a forward only cursor.
    /// </summary>
    /// <typeparam name="T">The type of record that this cursor iterates over.</typeparam>
    public interface IForwardOnlyCursor<T>
    {
        /// <summary>
        /// Gets the current record.
        /// </summary>
        T? Current { get; }

        /// <summary>
        /// Moves the cursor to the next record.
        /// </summary>
        /// <returns>true if there are more records available; false otherwise.</returns>
        bool MoveNext();
    }
}
