using MongoDB.Bson;
using StarLab.Domain.Data;

namespace StarLab.Data.MongoDB
{
    /// <summary>
    /// 
    /// </summary>
    internal class EntityData : IEntityData
    {
        private BsonDocument? data; //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public void SetData(BsonDocument data)
        {
            this.data = data;
        }

        /// <summary>
        /// Gets the double value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public double GetDoubleValue(string field)
        {
            if (data != null && data.IndexOfName(field) != -1 && data[field].IsDouble)
            {
                return data[field].AsDouble;
            }

            return double.NaN;
        }

        /// <summary>
        /// Gets the integer value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public int GetIntegerValue(string field)
        {
            if (data != null && data.IndexOfName(field) != -1 && data[field].IsInt32)
            {
                return data[field].AsInt32;
            }

            return int.MinValue;
        }

        /// <summary>
        /// Gets the long value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public int GetLongValue(string field)
        {
            if (data != null && data.IndexOfName(field) != -1 && data[field].IsInt64)
            {
                return (int)data[field].AsInt64;
            }

            return int.MinValue;
        }

        /// <summary>
        /// Gets the string value held in the specified field.
        /// </summary>
        /// <param name="field">The name of the field.</param>
        /// <returns>The value of the field.</returns>
        public string GetStringValue(string field)
        {
            if (data != null && data.IndexOfName(field) != -1 && data[field].IsString)
            {
                return data[field].AsString;
            }

            return string.Empty;
        }
    }
}
