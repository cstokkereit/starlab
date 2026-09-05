using MongoDB.Bson;
using MongoDB.Driver;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    internal class EmptyCursor : IAsyncCursor<BsonDocument>
    {
        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<BsonDocument> Current => new List<BsonDocument>();

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            // Do Nothing
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public bool MoveNext(CancellationToken cancellationToken = default)
        {
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken = default)
        {
            return new Task<bool>(() => false);
        }
    }
}
