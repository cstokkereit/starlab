using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Data.Import;

namespace MongoDB.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ImportProvider : IImportProvider
    {
        private const int BATCH_SIZE = 1000; // The number of documents that constitutes a batch.

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        public void Import(IDataset dataset)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("local");
            var collection = database.GetCollection<BsonDocument>("stars");

            while (!dataset.EOF)
            {
                var documents = GetBatch(dataset);

                if (documents.Count > 0) collection.InsertMany(documents);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataset"></param>
        /// <returns></returns>
        private List<BsonDocument> GetBatch(IDataset dataset)
        {
            var documents = new List<BsonDocument>();

            var counter = 0;

            while (counter++ < BATCH_SIZE && !dataset.EOF)
            {
                dataset.MoveNext();

                if (!dataset.EOF)
                {
                    // TODO - Need to wrap the field conversion in a try catch and throw a new custom exception with the details of the failure to be trapped here and added to a report

                    documents.Add(CreateDocument(dataset.Fields));
                }
            }

            return documents;
        }

        /// <summary>
        /// Constructs a <see cref="BsonDocument"/> from the values in the <see cref="IEnumerable{IDataField}"/> provided.
        /// </summary>
        /// <param name="fields">An <see cref="IEnumerable{IDataField}"/> containing the fields that comprise the <see cref="BsonDocument"/>.</param>
        /// <returns>A <see cref="BsonDocument"/> constructed from the specified field values.</returns>
        private static BsonDocument CreateDocument(IEnumerable<IDataField> fields)
        {
            var document = new BsonDocument();

            foreach (var field in fields)
            {
                document.Add(field.Name, BsonValue.Create(field.Value));
            }

            return document;
        }
    }
}
