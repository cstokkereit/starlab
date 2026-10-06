using MongoDB.Bson;
using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// An implementation of <see cref="IDataField"/> that gets its values from a list of <see cref="BsonDocument"/>s.
    /// </summary>
    internal sealed class ListBackedDataField : IDataField
    {
        private readonly string name; // The field name.

        private readonly int index; // The field index.

        private BsonDocument? document; // The document that contains the current value of the field.

        private readonly int width; // The width of the field (only used for fixed width files).

        /// <summary>
        /// Initialises a new instance of the <see cref="ListBackedDataField"/> class.
        /// </summary>
        /// <param name="index">The field index.</param>
        /// <param name="name">The field name.</param>
        public ListBackedDataField(int index, string name)
        {
            this.index = index;
            this.name = name;
        }

        /// <summary>
        /// Gets the field index.
        /// </summary>
        public int Index => index;

        /// <summary>
        /// Gets the field name.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the data type of the field.
        /// </summary>
        public DataTypes DataType { get; }

        /// <summary>
        /// Gets the current value of the field.
        /// </summary>
        public object Value
        {
            get
            {
                if (document == null) throw new InvalidOperationException(ExceptionMessages.DocumentNotSet);

                return document.GetElement(Index).Value;
            }
        }

        /// <summary>
        /// Gets the width of the field. If the field is of variable length, this property will return -1.
        /// </summary>
        public int Width { get => width; }

        /// <summary>
        /// Sets the <see cref="BsonDocument"/> that contains the current value of the field.
        /// </summary>
        /// <param name="document">A <see cref="BsonDocument"/> that contains the current value of the field.</param>
        public void SetCurrentDocument(BsonDocument? document)
        {
            this.document = document;
        }
    }
}
