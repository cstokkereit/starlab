using MongoDB.Bson;
using System.Diagnostics;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// An implementation of <see cref="IDataset"/> that is backed by a <see cref="List{BsonDocument}"/> that contains the records.
    /// </summary>
    internal sealed class ListBackedDataset : IDataset
    {
        private readonly Dictionary<string, IDataField> fields = new Dictionary<string, IDataField>(); // A dictionary containing the fields indexed by name.

        internal readonly List<BsonDocument> documents; // A list containing the documents.

        private int row = -1; // The row index.

        /// <summary>
        /// Initialises a new instance of the <see cref="ListBackedDataset"/> class.
        /// </summary>
        /// <param name="documents">A <see cref="List{BsonDocument}"/> that contains the records.</param>
        public ListBackedDataset(List<BsonDocument> documents)
        {
            this.documents = documents;

            if (documents.Count > 0)
            {
                LoadFields(documents[0]);
            }
        }

        /// <summary>
        /// A flag that indicates that the current row index is before the start of the file.
        /// </summary>
        public bool BOF => row == -1;

        /// <summary>
        /// A flag that indicates that the current row index is beyond the end of the file.
        /// </summary>
        public bool EOF => row == documents.Count;

        /// <summary>
        /// Gets an <see cref="IEnumerable{IDataField}"/> that contains the available data fields.
        /// </summary>
        public IEnumerable<IDataField> Fields => fields.Values;

        /// <summary>
        /// Releases all resources used by the <see cref="ListBackedDataset"/> object.
        /// </summary>
        public void Dispose()
        {
            // Do Nothing
        }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <param name="index">The index of the field.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified index.</returns>
        public object GetValue(int index)
        {
            var field = fields.Values.ElementAt(index);

            Debug.Assert(field.Index == index);

            return field.Value;
         }

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified name.</returns>
        public object GetValue(string name)
        {
            return fields[name].Value;
        }

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <param name="field">The <see cref="IDataField"/> that contains the required value.</param>
        /// <returns>An <see cref="object"/> that holds the value of the field with the specified name.</returns>
        public object GetValue(IDataField field)
        {
            return GetValue(field.Name);
        }

        /// <summary>
        /// Moves the pointer to the specified row index.
        /// </summary>
        /// <param name="index">The new row index.</param>
        public void Move(int index)
        {
            BsonDocument? document = null;

            if (index < 0)
            {
                row = -1;
            }
            else if (index > documents.Count)
            {
                row = documents.Count;
            }
            else
            {
                document = documents[index];
                row = index;
            }

            foreach (var field in fields.Values)
            {
                ((ListBackedDataField)field).SetCurrentDocument(document);
            }
        }

        /// <summary>
        /// Moves the pointer to the start of the file.
        /// </summary>
        public void MoveFirst()
        {
            Move(0);
        }

        /// <summary>
        /// Mpves the pointer to the end of the file.
        /// </summary>
        public void MoveLast()
        {
            Move(documents.Count() - 1);
        }

        /// <summary>
        /// Moves the pointer one step closer to the end of the file.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void MoveNext()
        {
            if (EOF) throw new InvalidOperationException(); // TODO

            Move(row + 1);
        }

        /// <summary>
        /// Moves the pointer one step closer to the start of the file.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        public void MovePrevious()
        {
            if (BOF) throw new InvalidOperationException(); // TODO

            Move(row - 1);
        }
        
        /// <summary>
        /// Loads the available data fields from the <see cref="BsonDocument"/> provided.
        /// </summary>
        /// <param name="document">A <see cref="BsonDocument"/> whose elements map to the available data fields.</param>
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
