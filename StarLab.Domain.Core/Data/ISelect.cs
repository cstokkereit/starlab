namespace StarLab.Data
{
    /// <summary>
    /// Represents part of a database query that specifies which data fields contain the required values.
    /// </summary>
    public interface ISelect : IQueryFragment
    {
        public event EventHandler<string> TableAdded; // An event that notifies subscribers whenever a table is added.

        /// <summary>
        /// A flag indicating that only unique values or value combinations should be returned.
        /// </summary>
        bool Distinct { get; }

        /// <summary>
        /// Gets a list containing the tables from which fields have been selected.
        /// </summary>
        IReadOnlyList<ITable> Tables { get; }

        /// <summary>
        /// Adds an <see cref="IField"/> from the specified table to the query.
        /// </summary>
        /// <param name="table">The name of the table containing the field to be added to the query.</param>
        /// <param name="field">The <see cref="IField"/> being added to the query.</param>
        void AddField(string table, IField field);

        /// <summary>
        /// Adds all of the fields from the <see cref="ITable"/> provided to the query.
        /// </summary>
        /// <param name="table">The <see cref="ITable"/> containing the fields being added to the query.</param>
        void AddTable(ITable table);
    }
}
