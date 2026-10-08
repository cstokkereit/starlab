using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Shared;

namespace StarLab.Data
{
    /// <summary>
    /// An in memory implementation of <see cref="IDataset"/>.
    /// </summary>
    public class Dataset : IDataset
    {
        private readonly List<object[]> rows = new List<object[]>(); // A list containing the rows of data where each row is an array containing the field values.

        private readonly Dictionary<string, IDataField> fields = new Dictionary<string, IDataField>(); // A dictionary containing the available fields.

        private int index = 0; // The index of the current row.

        /// <summary>
        /// Initialises a new instance of the <see cref="Dataset"/> class.
        /// </summary>
        /// <param name="dataset">An <see cref="IForwardOnlyDataset"/> containing the data.</param>
        public Dataset(IForwardOnlyDataset dataset)
        {
            ArgumentNullException.ThrowIfNull(dataset, nameof(dataset));

            CopyFields(dataset);

            dataset.MoveNext();

            LoadData(dataset);
        }

        /// <summary>
        /// A flag that indicates that the current row index is before the start of the dataset.
        /// </summary>
        public bool BOF => index == 0;

        /// <summary>
        /// A flag that indicates that the current row index is beyond the end of the dataset.
        /// </summary>
        public bool EOF => index > Rows;

        /// <summary>
        /// Gets an <see cref="IEnumerable{IDataField}"/> that contains the available data fields.
        /// </summary>
        public IEnumerable<IDataField> Fields => fields.Values;

        /// <summary>
        /// Gets the number of rows of data.
        /// </summary>
        public int Rows => rows.Count;

        /// <summary>
        /// Releases all resources used by the <see cref="Dataset"/> object.
        /// </summary>
        public void Dispose()
        {
            // Do Nothing
        }

        /// <summary>
        /// Gets the value of the field with the specified index.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="index">The index of the field.</param>
        /// <returns>The value of the field with the specified index.</returns>
        public T GetValue<T>(int index)
        {
            var values = GetValues();

            return (T)Convert.ChangeType(values[index], typeof(T));
        }

        /// <summary>
        /// Gets the value of the field with the specified name.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="name">The name of the field.</param>
        /// <returns>The value of the field with the specified name.</returns>
        public T GetValue<T>(string name)
        {
            var values = GetValues();

            return (T)Convert.ChangeType(values[fields[name].Index], typeof(T));
        }

        /// <summary>
        /// Gets the value of the specified field.
        /// </summary>
        /// <typeparam name="T">The type of the value to retrieve.</typeparam>
        /// <param name="field">The <see cref="IDataField"/> that contains the required value.</param>
        /// <returns>The value of the specified <see cref="IDataField">.</returns>
        public T GetValue<T>(IDataField field)
        {
            var values = GetValues();

            return (T)Convert.ChangeType(values[field.Index], typeof(T));
        }

        /// <summary>
        /// Moves the pointer to the specified row index.
        /// </summary>
        /// <param name="index">The new row index.</param>
        public void Move(int index)
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index, Rows, nameof(index));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(index, nameof(index));

            this.index = index;
        }

        /// <summary>
        /// Moves the pointer to the start of the dataset.
        /// </summary>
        public void MoveFirst()
        {
            index = 1;
        }

        /// <summary>
        /// Moves the pointer to the end of the dataset.
        /// </summary>
        public void MoveLast()
        {
            index = Rows;
        }

        /// <summary>
        /// Moves the pointer to the next row of data.
        /// </summary>
        public void MoveNext()
        {
            if (EOF) throw new InvalidOperationException(ExceptionMessages.AlreadyAtEndOfFile);

            index++;
        }

        /// <summary>
        /// Moves the pointer to the previous row of data.
        /// </summary>
        public void MovePrevious()
        {
            if (BOF) throw new InvalidOperationException(ExceptionMessages.AlreadyAtBeginningOfFile);

            index--;
        }

        /// <summary>
        /// Copies the fields from the <see cref="IForwardOnlyDataset"/> provided.
        /// </summary>
        /// <param name="dataset">The dataset that contains the fields being copied.</param>
        private void CopyFields(IForwardOnlyDataset dataset)
        {
            foreach (var field in dataset.Fields)
            {
                fields.Add(field.Name, new DataField(field));
            }
        }

        /// <summary>
        /// Gets the object array that holds the values for the current row.
        /// </summary>
        /// <returns>An <see cref="object[]"/> that contains the values for the current row.</returns>
        private object[] GetValues()
        {
            if (BOF) throw new InvalidOperationException(ExceptionMessages.AlreadyAtBeginningOfFile);
            if (EOF) throw new InvalidOperationException(ExceptionMessages.AlreadyAtEndOfFile);

            return rows[index - 1];
        }

        /// <summary>
        /// Loads the data from the <see cref="IForwardOnlyDataset"/> provided.
        /// </summary>
        /// <param name="dataset">The dataset that contains the data.</param>
        private void LoadData(IForwardOnlyDataset dataset)
        {
            while (!dataset.EOF)
            {
                var values = new object[fields.Count];

                foreach (var field in fields.Values)
                {
                    switch (field.DataType)
                    {
                        case DataTypes.Decimal:
                            values[field.Index] = dataset.GetValue<double>(field);
                            break;

                        case DataTypes.Integer:
                            values[field.Index] = dataset.GetValue<int>(field);
                            break;

                        case DataTypes.Text:
                            values[field.Index] = dataset.GetValue<string>(field);
                            break;
                    }
                }

                dataset.MoveNext();

                rows.Add(values);
            }
        }
    }
}
