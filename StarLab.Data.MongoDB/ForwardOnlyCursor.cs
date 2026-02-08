using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// Abstract base implementation of the <see cref="IForwardOnlyCursor{T}"/> interface. Wraps an <see cref="IAsyncCursor{BsonDocument}"/> that contains the data.
    /// </summary>
    /// <typeparam name="T">The type of record that this cursor provides.</typeparam>
    internal abstract class ForwardOnlyCursor<T> : IForwardOnlyCursor<T>
    {
        private readonly IAsyncCursor<BsonDocument> cursor; // The wrapped cursor that contains the data.

        private List<T> buffer = new List<T>(); // A buffer that holds the current batch of records retrieved from the wrapped cursor.

        private int index = -1; // The index of the current reocord within the buffer.

        /// <summary>
        /// Initialises a new instance of the <see cref="ForwardOnlyCursor{T}"/> class.
        /// </summary>
        /// <param name="cursor">An <see cref="IAsyncCursor{BsonDocument}"/> that contains the data.</param>
        public ForwardOnlyCursor(IAsyncCursor<BsonDocument> cursor)
        {
            this.cursor = cursor;
        }

        /// <summary>
        /// Gets the current record.
        /// </summary>
        public T? Current
        {
            get
            {
                if (index > -1 && index < buffer.Count)
                {
                    return buffer[index];
                }

                return default;
            }
        }

        /// <summary>
        /// Moves the cursor to the next record.
        /// </summary>
        /// <returns>true if there are more records available; false otherwise.</returns>
        public bool MoveNext()
        {
            index++;

            if (buffer.Count == index)
            {
                LoadBatch();
            }

            return buffer.Count > 0 && index < buffer.Count;
        }

        /// <summary>
        /// Loads a batch of records from the wrapped cursor into the buffer and resets the buffer index.
        /// </summary>
        private void LoadBatch()
        {
            index = 0;

            buffer.Clear();
            
            if (cursor.MoveNext())
            {
                foreach (var document in cursor.Current)
                {
                    buffer.Add(CreateRecord(document));
                }
            }
        }

        /// <summary>
        /// Creates a record from the <see cref="BsonDocument"/> provided.
        /// </summary>
        /// <param name="document">The <see cref="BsonDocument"/> that will be used to create the record.</param>
        /// <returns>The newly created record.</returns>
        protected abstract T CreateRecord(BsonDocument document);
    }
}
