namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that specifies how to sort the data returned by the query.
    /// </summary>
    public class SortFieldFragment : IQueryFragment
    {
        private readonly IField _field; // The inner data field.

        /// <summary>
        /// Initialises a new instance of the <see cref="SortFieldFragment"/> class.
        /// </summary>
        /// <param name="field">An <see cref="IField"/> that will be used to sort the data returned by the query.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the sort order for the field.</param>
        public SortFieldFragment(IField field, SortOrder sortOrder)
        {
            SortOrder = sortOrder;

            _field = field;
        }

        /// <summary>
        /// Gets the table qualified field name.
        /// </summary>
        public string FullName => _field.FullName;

        /// <summary>
        /// Gets the field name.
        /// </summary>
        public string Name => _field.Name;

        /// <summary>
        /// Gets the sort order.
        /// </summary>
        public SortOrder SortOrder { get; }

        /// <summary>
        /// Gets the name of the table that contains the field.
        /// </summary>
        public string Table => _field.Table;

        /// <summary>
        /// Converts the value of the current <see cref="SortFieldFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="SortFieldFragment"/> object.</returns>
        public override string ToString()
        {
            return $"{FullName} {(SortOrder == SortOrder.Ascending ? " ASC" : " DESC")}";
        }
    }
}
