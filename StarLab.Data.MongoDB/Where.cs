using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IWhere"/> interface.
    /// </summary>
    internal class Where : WhereFragment
    {
        private IPredicate predicate = new EmptyFilter(); // The predicate that specifies which documents to retrieve.

        /// <summary>
        /// Initialises a new instance of the <see cref="Where"/> class.
        /// </summary>
        /// <param name="predicates">An <see cref="IEnumerable{IPredicate}"/> containing the predicates that will be used to filter the data.</param>
        public Where(IEnumerable<IPredicate> predicates)
        {
            predicate = new AndFilter(predicates);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Where"/> class.
        /// </summary>
        /// <param name="predicate">An <see cref="IPredicate"/> that will be used to filter the data.</param>
        public Where(IPredicate predicate)
        {
            this.predicate = predicate;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Where"/> class.
        /// </summary>
        public Where() 
        { 
            // Do Nothing
        }

        /// <summary>
        /// Adds an <see cref="IPredicate"/> that will be used to filter the data.
        /// </summary>
        /// <param name="predicate">An <see cref="IPredicate"/> that will be used to filter the data.</param>
        /// <returns>A reference to this <see cref="IWhere"/> object to allow fluent addition of predicates.</returns>
        public override IWhere AddPredicate(IPredicate predicate)
        {
            if (this.predicate is EmptyFilter)
            {
                this.predicate = new AndFilter([predicate]);
            }
            else if (this.predicate is IAndPredicate and)
            {
                and.AddPredicate(predicate);
            }
            else if (this.predicate is IOrPredicate or)
            {
                or.AddPredicate(predicate);
            }

            return this;
        }

        /// <summary>
        /// Gets the <see cref="FilterDefinition{BsonDocument}"/> that specifies which documents to retrieve.
        /// </summary>
        /// <returns>A <see cref="FilterDefinition{BsonDocument}"/> that specifies which documents to retrieve.</returns>
        public FilterDefinition<BsonDocument> GetFilter()
        {
            return ((IFilterAdapter)predicate).GetFilter();
        }

        /// <summary>
        /// Converts the value of the current <see cref="Where"/> object to its equivalent string representation.
        /// </summary>
        /// <param name="useFullNames">A flag that specifies whether field names are to be prefixed with the name of the table that contains the field.</param>
        /// <returns>A string representation of the current <see cref="Where"/> object.</returns>
        public override string ToString(bool useFullNames)
        {
            if (predicate is EmptyFilter || string.IsNullOrEmpty(predicate.ToString()))
            {
                return string.Empty;
            }

            return $" WHERE {predicate.ToString(useFullNames)}";
        }

        /// <summary>
        /// Converts the value of the current <see cref="Where"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Where"/> object.</returns>
        public override string ToString()
        {
            return ToString(true);
        }
    }
}
