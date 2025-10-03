using MongoDB.Bson;
using StarLab.Domain;

namespace StarLab.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <param name="index"></param>
    /// <param name="name"></param>
    internal sealed class ListBackedDataField(int index, string name) : IDataField
    {
        private BsonDocument? document;

        private readonly int index = index;

        private readonly string name = name;

        public int Index => index;

        public string Name => name;

        public object Value
        {
            get
            {
                if (document == null) throw new InvalidOperationException(); // TODO

                return document.GetElement(Index).Value;
            }
        }

        public void SetCurrentDocument(BsonDocument? document)
        {
            this.document = document;
        }
    }
}
