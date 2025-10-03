using MongoDB.Bson;
using MongoDB.Driver;
using StarLab.Domain;

namespace StarLab.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    public class DataProvider : IDataProvider
    {
        private IMongoDatabase? database;

        public void Connect(string host, string database)
        {
            var client = new MongoClient($"mongodb://{host}");

            this.database = client.GetDatabase(database);
        }

        public IList<IStar> GetStars()
        {
            if (database == null) throw new InvalidOperationException(); // TODO

            var collection = database.GetCollection<BsonDocument>("stars");

            var filter = Builders<BsonDocument>.Filter.Empty;

            var stars = new List<IStar>();

            foreach (var item in collection.Find(filter).ToList())
            {
                try
                {
                    stars.Add(new StarData(item));
                }
                catch (Exception e)
                {

                }
            }

            return stars;
        }
    }
}
