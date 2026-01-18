namespace StarLab.Data
{
    /// <summary>
    /// Represents part of a database query that specifies how to sort the returned data.
    /// </summary>
    public interface IOrderBy : IQueryFragment
    {
        /// <summary>
        /// Adds an <see cref="IField"/> to the sort.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that will be used to sort the returned data.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort direction for the field.</param>
        void AddSortField(IField field, SortOrder sortOrder);
    }
}
