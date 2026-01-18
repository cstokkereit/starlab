using System.Text;

namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that specifies how the data returned by the query will be sorted.
    /// </summary>
    public abstract class OrderByFragment : QueryFragment, IOrderBy
    {
        protected readonly List<SortFieldFragment> fields = new List<SortFieldFragment>(); // A list containing the sort fields.

        /// <summary>
        /// Adds an <see cref="IField"/> to the order by clause.
        /// </summary>
        /// <param name="field">The <see cref="IField"/> being added to the order by clause.</param>
        /// <param name="sortOrder">A <see cref="SortOrder"/> that specifies the direction in which the field is to be sorted.</param>
        public virtual void AddSortField(IField field, SortOrder sortOrder)
        {
            fields.Add(new SortFieldFragment(field, sortOrder));
        }

        /// <summary>
        /// Converts the value of the current <see cref="OrderByFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="OrderByFragment"/> object.</returns>
        public string ToString(bool useFullNames)
        {
            if (fields.Count == 0) return string.Empty;

            return $" ORDER BY {GetFields(useFullNames)}";
        }

        /// <summary>
        /// Converts the value of the current <see cref="OrderByFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="OrderByFragment"/> object.</returns>
        public override string ToString()
        {
            return ToString(false);
        }

        /// <summary>
        /// Generates a comma separated list containing the names of the sort fields along with their respective sort directions.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string value that holds a comma separated list containing the names of the sort fields along with their respective sort directions.</returns>
        private string GetFields(bool useFullNames)
        {
            var builder = new StringBuilder();

            var first = true;

            foreach (var field in fields)
            {
                if (!first) builder.Append(SEPARATOR);

                builder.Append($"{(useFullNames ? field.FullName : field.Name)} ");
                builder.Append($"{(field.SortOrder == SortOrder.Ascending ? "ASC" : "DESC")}");

                first = false;
            }

            return builder.ToString();
        }
    }
}
