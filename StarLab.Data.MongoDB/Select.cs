using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// A MongoDB specific implementation of the <see cref="ISelect"/> interface.
    /// </summary>
    internal class Select : SelectFragment
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Select"/> class.
        /// </summary>
        /// <param name="distinct"></param>
        public Select(bool distinct)
            : base(distinct) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Select"/> class.
        /// </summary>
        public Select()
            : base() { }

        /// <summary>
        /// Gets a <see cref="ProjectionDefinition{BsonDocument}"/> that specifies which fields will be retrieved.
        /// </summary>
        /// <returns>A <see cref="ProjectionDefinition{BsonDocument}"/> that specifies which fields will be retrieved.</returns>
        public ProjectionDefinition<BsonDocument> GetProjection()
        {
            var projection = Builders<BsonDocument>.Projection.Exclude("_id");

            if (tables.Count == 1)
            {

            }

            return projection;
        }

        
    }
}
