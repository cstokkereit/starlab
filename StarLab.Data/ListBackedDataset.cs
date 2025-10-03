using MongoDB.Bson;
using StarLab.Domain;
using System.Diagnostics;

namespace StarLab.Data
{
    internal sealed class ListBackedDataset : IDataset
    {
        private readonly Dictionary<string, IDataField> fields = new Dictionary<string, IDataField>();

        internal readonly List<BsonDocument> documents;

        private int index = -1;

        public ListBackedDataset(List<BsonDocument> documents)
        {
            this.documents = documents;

            if (documents.Count > 0)
            {
                LoadFields(documents[0]);
            }
        }

        public bool BOF => index == -1;

        public bool EOF => index == documents.Count;

        public IEnumerable<IDataField> Fields => fields.Values;

        public void Dispose()
        {
            // Do Nothing
        }

        public object GetValue(int index)
        {
            var field = fields.Values.ElementAt(index);

            Debug.Assert(field.Index == index);

            return field.Value;
         }

        public object GetValue(string name)
        {
            return fields[name].Value;
        }

        public object GetValue(IDataField field)
        {
            return GetValue(field.Name);
        }

        public void Move(int index)
        {
            BsonDocument? document = null;

            if (index < 0)
            {
                this.index = -1;
            }
            else if (index > documents.Count)
            {
                this.index = documents.Count;
            }
            else
            {
                document = documents[index];
                this.index = index;
            }

            foreach (var field in fields.Values)
            {
                ((ListBackedDataField)field).SetCurrentDocument(document);
            }
        }

        public void MoveFirst()
        {
            Move(0);
        }

        public void MoveLast()
        {
            Move(documents.Count() - 1);
        }

        public void MoveNext()
        {
            if (EOF) throw new InvalidOperationException(); // TODO

            Move(index + 1);
        }

        public void MovePrevious()
        {
            if (BOF) throw new InvalidOperationException(); // TODO

            Move(index - 1);
        }

        private void LoadFields(BsonDocument document)
        {
            for (int index = 0; index < document.Elements.Count(); index++)
            {
                var field = new ListBackedDataField(index, document.GetElement(index).Name);

                fields.Add(field.Name, field);
            }
        }
    }
}
