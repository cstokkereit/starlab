using System.Text;

namespace StarLab.Data
{
    /// <summary>
    /// Part of a database query that specifies which tables contain the required data.
    /// </summary>
    public class FromFragment : QueryFragment, IFrom
    {
        protected readonly HashSet<string> tables = new HashSet<string>(); // A set containing the names of the tables included in the query.

        /// <summary>
        /// Gets the number of tables included in the query.
        /// </summary>
        public int Size => tables.Count;

        /// <summary>
        /// Adds all of the fields from the specified table to the query.
        /// </summary>
        /// <param name="name">The name of the table containing the fields.</param>
        public virtual void AddTable(string name)
        {
            tables.Add(name);
        }

        /// <summary>
        /// Converts the value of the current <see cref="FromFragment"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="FromFragment"/> object.</returns>
        public override string ToString()
        {
            return $"FROM {GetTables()}";
        }

        /// <summary>
        /// Generates a comma separated list containing the names of all of the tables included in the query.
        /// </summary>
        /// <returns>A comma separated list containing the names of all of the tables included in the query.</returns>
        private string GetTables()
        {
            var builder = new StringBuilder();

            var first = true;

            foreach (var table in tables)
            {
                if (!first) builder.Append(SEPARATOR);

                builder.Append(table);

                first = false;
            }

            return builder.ToString();
        }
    }
}
