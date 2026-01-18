namespace StarLab.Data
{
    /// <summary>
    /// Represents part of a database query that specifies which tables contain the required data.
    /// </summary>
    public interface IFrom : IQueryFragment
    {
        /// <summary>
        /// Gets the number of included tables.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Adds the specified table to the list of included tables.
        /// </summary>
        /// <param name="name">The name of the table to include.</param>
        void AddTable(string name);
    }
}
