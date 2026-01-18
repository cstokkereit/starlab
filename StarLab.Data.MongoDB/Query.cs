using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="IQuery"/> interface.
    /// </summary>
    internal class Query : QueryBase
    {
        /// <summary>
        /// Gets the <see cref="FilterDefinitionBuilder{BsonDocument}"/> specified by the where clause.
        /// </summary>
        /// <returns>An <see cref="FilterDefinitionBuilder{BsonDocument}"/> that specifies which records will be returned from the MongoDB database.</returns>
        public FilterDefinition<BsonDocument> GetFilter()
        {
            return ((Where)WhereClause).GetFilter();
        }

        /// <summary>
        /// Gets the <see cref="ProjectionDefinition{BsonDocument}"/> specified by the select statement.
        /// </summary>
        /// <returns>A <see cref="ProjectionDefinition{BsonDocument}"/> that specifies which fields will be returned from the MongoDB database.</returns>
        public ProjectionDefinition<BsonDocument> GetProjection()
        {
            return ((Select)SelectStatement).GetProjection();
        }

        /// <summary>
        /// Gets the <see cref="IFrom"/> that specifies the collection(s) containing the documents to retrieved.
        /// </summary>
        protected override IFrom CreateFromClause()
        {
            return new From();
        }

        /// <summary>
        /// Gets the <see cref="IOrderBy"/> that specifies the sort order for the retrieved records.
        /// </summary>
        protected override IOrderBy CreateOrderByClause()
        {
            return new OrderBy();
        }

        /// <summary>
        /// Gets the <see cref="ISelect"/> that specifies which fields to retrieve.
        /// </summary>
        protected override ISelect CreateSelectClause()
        {
            return new Select();
        }

        /// <summary>
        /// Gets the <see cref="IWhere"/> that specifies which documents to retrieve.
        /// </summary>
        protected override IWhere CreateWhereClause()
        {
            return new Where();
        }
    }
}
