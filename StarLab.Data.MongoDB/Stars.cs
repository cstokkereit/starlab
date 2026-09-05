using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Domain.Entities;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// An implementation of <see cref="ForwardOnlyCursor{T}"/> that provides records of type <see cref="IStar"/>.
    /// </summary>
    internal class Stars : ForwardOnlyCursor<IStar>
    {
        private readonly EntityData data = new EntityData(); //

        /// <summary>
        /// Initialises a new instance of the <see cref="Stars"/> class.
        /// </summary>
        /// <param name="cursor">An <see cref="IAsyncCursor{BsonDocument}"/> that contains the data.</param>
        public Stars(IAsyncCursor<BsonDocument> cursor)
            : base(cursor) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Stars"/> class.
        /// </summary>
        public Stars()
            : base(new EmptyCursor()) { }

        /// <summary>
        /// Creates a record from the <see cref="BsonDocument"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="BsonDocument"/> that will be used to create the record.</param>
        /// <returns>The newly created <see cref="IStar"/>.</returns>
        protected override IStar CreateRecord(BsonDocument document)
        {
            data.SetData(document);

            return new Star(data);
        }
    }
}
