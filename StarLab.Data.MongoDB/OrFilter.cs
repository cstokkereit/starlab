using MongoDB.Bson;
using MongoDB.Driver;
using System.Text;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IOrPredicate"/> interface.
    /// </summary>
    internal class OrFilter : Predicate, IOrPredicate, IFilterAdapter
    {
        private const string OR = " OR "; // A constant used to generate the string representation of this object.

        private readonly List<IPredicate> predicates = new List<IPredicate>(); // A list containing the predicates that are being combined using the OR operator.

        /// <summary>
        /// Initialises a new instance of the <see cref="OrFilter"/> class.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the <see cref="IPredicate"/>s that are being combined using the OR operator.</param>
        public OrFilter(IEnumerable<IPredicate> predicates)
        {
            this.predicates.AddRange(predicates);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="OrFilter"/> class.
        /// </summary>
        public OrFilter()
        {
            // Do Nothing
        }

        /// <summary>
        /// Adds an <see cref="IPredicate"/> to the predicates that are being combined using the OR operator.
        /// </summary>
        /// <param name="predicate">The <see cref="IPredicate"/> being added.</param>
        /// <returns>A reference to this <see cref="IOrPredicate"/> object to allow fluent addition of predicates.</returns>
        public IOrPredicate AddPredicate(IPredicate predicate)
        {
            predicates.Add(predicate);

            return this;
        }

        /// <summary>
        /// Gets the <see cref="FilterDefinition{BsonDocument}"/> specified by this predicate.
        /// </summary>
        /// <returns>A <see cref="FilterDefinition{BsonDocument}"/> that specifies which documents to retrieve.</returns>
        public FilterDefinition<BsonDocument> GetFilter()
        {
            FilterDefinition<BsonDocument> filter = Builders<BsonDocument>.Filter.Empty;

            if (predicates.Count == 1)
            {
                filter = ((IFilterAdapter)predicates[0]).GetFilter();
            }
            else
            {
                var filters = new List<FilterDefinition<BsonDocument>>();

                foreach (var predicate in predicates)
                {
                    filters.Add(((IFilterAdapter)predicate).GetFilter());
                }

                filter = Builders<BsonDocument>.Filter.Or(filters);
            }

            return filter;
        }

        /// <summary>
        /// Converts the value of the current <see cref="OrFilter"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="OrFilter"/> object.</returns>
        public override string ToString(bool useFullNames)
        {
            var builder = new StringBuilder();

            var first = true;

            foreach (var predicate in predicates)
            {
                if (!first) builder.Append(OR);

                builder.Append(predicate.ToString(useFullNames));

                first = false;
            }

            return builder.ToString();
        }
    }
}
