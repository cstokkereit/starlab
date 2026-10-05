using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="FileBackedDataset"/> class.
    /// </summary>
    public class FileBackedDatasetTests
    {
        private readonly string csvWithHeaders = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DataWithHeaders.csv");

        private readonly string csvWithoutHeaders = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DataWithoutHeaders.csv");

        private readonly string datWithHeaders = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DataWithHeaders.dat");

        private readonly string datWithoutHeaders = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DataWithoutHeaders.dat");

        private readonly string csvNoData = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "NoData.csv");

        /// <summary>
        /// Test that the <see cref="FileBackedDataset(string, IImportDefinition)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance().Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            Assert.That(dataset, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.BOF"/> property is true when a file is first opened.
        /// </summary>
        [Test]
        public void TestBOFAtBeginningOfFile()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            Assert.That(dataset.BOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.BOF"/> property is false when the pointer is positioned after the beginning of a file containing no data.
        /// </summary>
        [Test]
        public void TestBOFForFileWithNoData()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvNoData, importDefinition);

            Assert.That(dataset.BOF, Is.True);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.BOF"/> property is false when the pointer is positioned after the beginning of the file.
        /// </summary>
        [Test]
        public void TestBOFAfterBeginningOfFile()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is false when a file is frst opened.
        /// </summary>
        [Test]
        public void TestEOFAtBeginningOfFile()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            Assert.That(dataset.EOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is false when the pointer is positioned before the end of the file.
        /// </summary>
        [Test]
        public void TestEOFBeforeEndOfFile()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            for (int i = 0; i < 17; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is true when the pointer is positioned at the end of the file.
        /// </summary>
        [Test]
        public void TestEOFAtEndOfFile()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            for (int i = 0; i < 18; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is true when the pointer is positioned at the end of a file containing no data.
        /// </summary>
        [Test]
        public void TestEOFForFileWithNoData()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(csvNoData, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(string)"/> method works correctly for a compound field from a delimited text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetCompoundFieldValueForADelimitedTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddCompoundField("NameAndClass", "{0} ({1})", [0, 1])
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>("SpectralClass"), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>("AbsoluteMagnitude"), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<string>("NameAndClass"), Is.EqualTo("Achernar (B3V)"));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> method works correctly for a formatted compound field from a delimited text file.
        /// </summary>
        [Test]
        public void TestGetCompoundFieldValueForADelimitedTextFileWithFormatting()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .AddCompoundField("NameAndClass", "{0} ({1})", [0, 1])
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
            Assert.That(dataset.GetValue<string>(4), Is.EqualTo("Achernar (B3V)"));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> method works correctly for a non-formatted compound field from a delimited text file.
        /// </summary>
        [Test]
        public void TestGetCompoundFieldValueForADelimitedTextFileWithoutFormatting()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .AddCompoundField("NameAndClass", [0, 1])
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
            Assert.That(dataset.GetValue<string>(4), Is.EqualTo("AchernarB3V"));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(string)"/> method works correctly for a compound field from a fixed width text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetCompoundFieldValueForAFixedWidthTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .ExcludeField(0, 10)
                .AddField(1, "SpectralClass", 5, DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .ExcludeField(3, 5)
                .AddCompoundField("NameAndClass", "{0} ({1})", [0, 1])
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>("SpectralClass"), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>("AbsoluteMagnitude"), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<string>("NameAndClass"), Is.EqualTo("Achernar (B3V)"));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> method works correctly for a formatted compound field from a fixed width text file.
        /// </summary>
        [Test]
        public void TestGetCompoundFieldValueForAFixedWidthTextFileWithFormatting()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .AddField(0, "Name", 10, DataTypes.Text)
                .AddField(1, "SpectralClass", 5, DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .AddCompoundField("NameAndClass", "{0} ({1})", [0, 1])
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
            Assert.That(dataset.GetValue<string>(4), Is.EqualTo("Achernar (B3V)"));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> method works correctly for a non-formatted compound field from a fixed width text file.
        /// </summary>
        [Test]
        public void TestGetCompoundFieldValueForAFixedWidthTextFileWithoutFormatting()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .AddField(0, "Name", 10, DataTypes.Text)
                .AddField(1, "SpectralClass", 5, DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .AddCompoundField("NameAndClass", [0, 1])
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
            Assert.That(dataset.GetValue<string>(4), Is.EqualTo("AchernarB3V"));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.Fields"/> property returns the correct fields for a delimited text file.
        /// </summary>
        [Test]
        public void TestGetFieldsWithDelimitedText()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            Assert.That(dataset.Fields, Is.Not.Null);

            var fields = dataset.Fields.ToList();

            Assert.That(fields.Count, Is.EqualTo(4));

            Assert.That(fields[0].Index, Is.EqualTo(0));
            Assert.That(fields[0].Name, Is.EqualTo("Name"));
            Assert.That(fields[0].DataType, Is.EqualTo(DataTypes.Text));
            Assert.That(fields[0].Width, Is.EqualTo(-1));

            Assert.That(fields[1].Index, Is.EqualTo(1));
            Assert.That(fields[1].Name, Is.EqualTo("SpectralClass"));
            Assert.That(fields[1].DataType, Is.EqualTo(DataTypes.Text));
            Assert.That(fields[1].Width, Is.EqualTo(-1));

            Assert.That(fields[2].Index, Is.EqualTo(2));
            Assert.That(fields[2].Name, Is.EqualTo("AbsoluteMagnitude"));
            Assert.That(fields[2].DataType, Is.EqualTo(DataTypes.Decimal));
            Assert.That(fields[2].Width, Is.EqualTo(-1));

            Assert.That(fields[3].Index, Is.EqualTo(3));
            Assert.That(fields[3].Name, Is.EqualTo("B-V"));
            Assert.That(fields[3].DataType, Is.EqualTo(DataTypes.Decimal));
            Assert.That(fields[3].Width, Is.EqualTo(-1));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.Fields"/> property returns the correct fields for a fixed width text file.
        /// </summary>
        [Test]
        public void TestGetFieldsWithFixedWidthText()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .AddField(0, "Name", 10, DataTypes.Text)
                .AddField(1, "SpectralClass", 5, DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            Assert.That(dataset.Fields, Is.Not.Null);

            var fields = dataset.Fields.ToList();

            Assert.That(fields.Count, Is.EqualTo(4));

            Assert.That(fields[0].Index, Is.EqualTo(0));
            Assert.That(fields[0].Name, Is.EqualTo("Name"));
            Assert.That(fields[0].DataType, Is.EqualTo(DataTypes.Text));
            Assert.That(fields[0].Width, Is.EqualTo(10));

            Assert.That(fields[1].Index, Is.EqualTo(1));
            Assert.That(fields[1].Name, Is.EqualTo("SpectralClass"));
            Assert.That(fields[1].DataType, Is.EqualTo(DataTypes.Text));
            Assert.That(fields[1].Width, Is.EqualTo(5));

            Assert.That(fields[2].Index, Is.EqualTo(2));
            Assert.That(fields[2].Name, Is.EqualTo("AbsoluteMagnitude"));
            Assert.That(fields[2].DataType, Is.EqualTo(DataTypes.Decimal));
            Assert.That(fields[2].Width, Is.EqualTo(6));

            Assert.That(fields[3].Index, Is.EqualTo(3));
            Assert.That(fields[3].Name, Is.EqualTo("B-V"));
            Assert.That(fields[3].DataType, Is.EqualTo(DataTypes.Decimal));
            Assert.That(fields[3].Width, Is.EqualTo(5));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(IDataField)"/> function returns the correct value for the specified field.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedField()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B - V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            var fields = dataset.Fields.ToList();

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>(fields[0]), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(fields[1]), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(fields[2]), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(fields[3]), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(IDataField)"/> function returns the correct value for the specified field from a delimited text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedFieldFromDelimitedTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B - V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            var fields = dataset.Fields.ToList();

            dataset.MoveNext();

            Assert.That(dataset.GetValue<double>(fields[0]), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(fields[1]), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(IDataField)"/> function returns the correct value for the specified field from a fixed width text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedFieldFromFixedWidthTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .ExcludeField(0, 10)
                .ExcludeField(1, 5)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            var fields = dataset.Fields.ToList();

            dataset.MoveNext();

            Assert.That(dataset.GetValue<double>(fields[0]), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(fields[1]), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> function returns the correct value for the field with the specified index.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndex()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B - V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> function returns the correct value for the specified index from a delimited text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndexFromDelimitedTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B - V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<double>(0), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(1), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue{T}(int)"/> function returns the correct value for the specified index from a fixed width text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndexFromFixedWidthTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .ExcludeField(0, 10)
                .ExcludeField(1, 5)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<double>(0), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(1), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue(string)"/> function returns the correct value for the field with the specified name.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedName()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>("Name"), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>("SpectralClass"), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>("AbsoluteMagnitude"), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>("B-V"), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue(string)"/> function returns the correct value for the field with the specified name from a delimited text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedNameFromDelimitedTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<double>("AbsoluteMagnitude"), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>("B-V"), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue(string)"/> function returns the correct value for the field with the specified name from a fixed width text file when some fields have been excluded.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedNameFromFixedWidthTextFileWithExcludedFields()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .ExcludeField(0, 10)
                .ExcludeField(1, 5)
                .AddField(2, "AbsoluteMagnitude", 6, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(datWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<double>("AbsoluteMagnitude"), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>("B-V"), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MoveNext()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestMoveNext()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .Build();

            var dataset = new FileBackedDataset(csvWithoutHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));

            for (int i = 1; i < 17; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Vega"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("A0V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(0.5));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(0.00));

            dataset.MoveNext();

            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MoveNext()"/> method works correctly when the data file contains header rows.
        /// </summary>
        [Test]
        public void TestMoveNextForADelimitedTextFileWithHeaderRows()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "B-V", DataTypes.Decimal)
                .AddHeaderRows(1)
                .Build();

            var dataset = new FileBackedDataset(csvWithHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));

            for (int i = 1; i < 17; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Vega"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("A0V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(0.5));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(0.00));

            dataset.MoveNext();

            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MoveNext()"/> method works correctly when the data file contains header rows.
        /// </summary>
        [Test]
        public void TestMoveNextForAFixedWidthTextFileWithHeaderRows()
        {
            var importDefinition = ImportDefinitionBuilder.GetInstance()
                .AddField(0, "Name", 10, DataTypes.Text)
                .AddField(1, "SpectralClass", 14, DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", 18, DataTypes.Decimal)
                .AddField(3, "B-V", 5, DataTypes.Decimal)
                .AddHeaderRows(1)
                .Build();

            var dataset = new FileBackedDataset(datWithHeaders, importDefinition);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));

            for (int i = 1; i < 17; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Vega"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("A0V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(0.5));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(0.00));

            dataset.MoveNext();

            Assert.That(dataset.EOF, Is.True);
        }
    }
}
