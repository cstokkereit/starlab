using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ImportDefinition"/> class.
    /// </summary>
    public class ImportDefinitionBuilderTests
    {
        /// <summary>
        /// Test that the static <see cref="ImportDefinitionBuilder.GetInstance()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetInstance()
        {
            // Act
            var builder = ImportDefinitionBuilder.GetInstance();

            // Assert
            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the static <see cref="ImportDefinitionBuilder.GetInstance(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetInstanceWithDelimiter()
        {
            // Act
            var builder = ImportDefinitionBuilder.GetInstance(",");

            // Assert
            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the static <see cref="ImportDefinitionBuilder.GetInstance(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestGetInstanceWithDelimiterAndTextDelimiter()
        {
            // Act
            var builder = ImportDefinitionBuilder.GetInstance(",", "\"");

            // Assert
            Assert.That(builder, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.AddCompoundField(string, string, int[])"/> method throws an <see cref="ArgumentException"/> when a duplicate field is added.
        /// </summary>
        [Test]
        public void TestAddCompoundFieldThrowsExceptionIfNameNotUnique()
        {
            // Act
            var e = Assert.Throws<ArgumentException>(() => ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Field-1", DataTypes.Decimal)
                .AddCompoundField("Field-1", "{0}-{1}", [1, 2]));

            //Assert.That(e.Message, Is.EqualTo(""));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.AddField(int, string, DataTypes)"/> method throws an <see cref="ArgumentException"/> when a duplicate field is added.
        /// </summary>
        [Test]
        public void TestAddFieldThrowsExceptionIfNameNotUnique()
        {
            // Act
            var e = Assert.Throws<ArgumentException>(() => ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Field-1", DataTypes.Decimal)
                .AddField(1, "Field-1", DataTypes.Decimal));

            //Assert.That(e.Message, Is.EqualTo(""));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a fixed width text file import definition.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionForAFixedWidthTextFile()
        {
            // Arrange
            var builder = ImportDefinitionBuilder.GetInstance();

            // Act
            var importDef = builder.Build();

            // Assert
            Assert.That(importDef, Is.Not.Null);
            Assert.That(importDef.FileType, Is.EqualTo(FileTypes.FixedWidthText));
            Assert.That(importDef.TextDelimiter, Is.EqualTo(string.Empty));
            Assert.That(importDef.Delimiter, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a comma delimited text file import definition.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionForACommaDelimitedTextFile()
        {
            // Arrange
            var builder = ImportDefinitionBuilder.GetInstance(",");

            // Act
            var importDef = builder.Build();

            // Assert
            Assert.That(importDef, Is.Not.Null);
            Assert.That(importDef.FileType, Is.EqualTo(FileTypes.DelimitedText));
            Assert.That(importDef.TextDelimiter, Is.EqualTo(string.Empty));
            Assert.That(importDef.Delimiter, Is.EqualTo(","));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a comma and text delimited text file import definition.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionForACommaDelimitedTextFileWithTextDelimiters()
        {
            // Arrange
            var builder = ImportDefinitionBuilder.GetInstance(",", "\"");

            // Act
            var importDef = builder.Build();

            // Assert
            Assert.That(importDef, Is.Not.Null);
            Assert.That(importDef.FileType, Is.EqualTo(FileTypes.DelimitedText));
            Assert.That(importDef.TextDelimiter, Is.EqualTo("\""));
            Assert.That(importDef.Delimiter, Is.EqualTo(","));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a file import definition that includes compound fields.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionThatIncludesCompoundFields()
        {
            // Arrange
            var builder = ImportDefinitionBuilder.GetInstance(",");

            // Act
            var importDef = builder.AddField(0, "Field-1", DataTypes.Decimal)
                .AddField(3, "Field-4", DataTypes.Decimal)
                .AddField(5, "Field-6", DataTypes.Decimal)
                .AddCompoundField("CompoundField-1", "{0}-{1}", [1, 2])
                .AddCompoundField("CompoundField-2", [1, 4])
                .Build();

            // Assert
            Assert.That(importDef.CompoundFields.Count, Is.EqualTo(2));

            var field1 = importDef.CompoundFields[0];
            var field2 = importDef.CompoundFields[1];

            Assert.That(field1.Name, Is.EqualTo("CompoundField-1"));
            Assert.That(field1.Format, Is.EqualTo("{0}-{1}"));
            Assert.That(field1.Components[0], Is.EqualTo(1));
            Assert.That(field1.Components[1], Is.EqualTo(2));

            Assert.That(field2.Name, Is.EqualTo("CompoundField-2"));
            Assert.That(field2.Format, Is.EqualTo(string.Empty));
            Assert.That(field2.Components[0], Is.EqualTo(1));
            Assert.That(field2.Components[1], Is.EqualTo(4));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a file import definition that includes delimited text fields.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionThatIncludesDelimitedTextFields()
        {
            // Arrange
            var builder = ImportDefinitionBuilder.GetInstance(",");

            // Act
            var importDef = builder.AddField(0, "Field-1", DataTypes.Decimal)
                .AddField(3, "Field-4", DataTypes.Decimal)
                .AddField(5, "Field-6", DataTypes.Decimal)
                .Build();

            // Assert
            Assert.That(importDef.Fields.Count, Is.EqualTo(3));

            var field1 = importDef.Fields[0];
            var field2 = importDef.Fields[1];
            var field3 = importDef.Fields[2];

            Assert.That(field1.Index, Is.EqualTo(0));
            Assert.That(field1.Name, Is.EqualTo("Field-1"));
            Assert.That(field1.Include, Is.True);
            Assert.That(field1.Width, Is.EqualTo(-1));

            Assert.That(field2.Index, Is.EqualTo(3));
            Assert.That(field2.Name, Is.EqualTo("Field-4"));
            Assert.That(field2.Include, Is.True);
            Assert.That(field2.Width, Is.EqualTo(-1));

            Assert.That(field3.Index, Is.EqualTo(5));
            Assert.That(field3.Name, Is.EqualTo("Field-6"));
            Assert.That(field3.Include, Is.True);
            Assert.That(field3.Width, Is.EqualTo(-1));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a fixed width file import definition that excludes some fields.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionThatExcludesFixedWidthTextFields()
        {
            // Arrange
            var builder = ImportDefinitionBuilder.GetInstance();

            // Act
            var importDef = builder.AddField(0, "Field-1", 2, DataTypes.Decimal)
                .ExcludeField(1, 8)
                .AddField(2, "Field-3", 7, DataTypes.Decimal)
                .ExcludeField(3, 6)
                .AddField(4, "Field-5", 5, DataTypes.Decimal)
                .Build();

            // Assert
            Assert.That(importDef.Fields.Count, Is.EqualTo(5));

            var field1 = importDef.Fields[0];
            var field2 = importDef.Fields[1];
            var field3 = importDef.Fields[2];
            var field4 = importDef.Fields[3];
            var field5 = importDef.Fields[4];

            Assert.That(field1.Index, Is.EqualTo(0));
            Assert.That(field1.Name, Is.EqualTo("Field-1"));
            Assert.That(field1.Include, Is.True);
            Assert.That(field1.Width, Is.EqualTo(2));

            Assert.That(field2.Index, Is.EqualTo(1));
            Assert.That(field2.Name, Is.EqualTo(string.Empty));
            Assert.That(field2.Include, Is.False);
            Assert.That(field2.Width, Is.EqualTo(8));

            Assert.That(field3.Index, Is.EqualTo(2));
            Assert.That(field3.Name, Is.EqualTo("Field-3"));
            Assert.That(field3.Include, Is.True);
            Assert.That(field3.Width, Is.EqualTo(7));

            Assert.That(field4.Index, Is.EqualTo(3));
            Assert.That(field4.Name, Is.EqualTo(string.Empty));
            Assert.That(field4.Include, Is.False);
            Assert.That(field4.Width, Is.EqualTo(6));

            Assert.That(field5.Index, Is.EqualTo(4));
            Assert.That(field5.Name, Is.EqualTo("Field-5"));
            Assert.That(field5.Include, Is.True);
            Assert.That(field5.Width, Is.EqualTo(5));
        }

        /// <summary>
        /// Test that the <see cref="ImportDefinitionBuilder.Build()"/> method works correctly for a fixed width file import definition.
        /// </summary>
        [Test]
        public void TestBuildImportDefinitionThatIncludesFixedWidthTextFields()
        {
            // Assert
            var builder = ImportDefinitionBuilder.GetInstance();

            // Act
            var importDef = builder.AddField(0, "Field-1", 2, DataTypes.Decimal)
                .AddField(1, "Field-2", 7, DataTypes.Decimal)
                .AddField(2, "Field-3", 5, DataTypes.Decimal)
                .Build();

            // Assert
            Assert.That(importDef.Fields.Count, Is.EqualTo(3));

            var field1 = importDef.Fields[0];
            var field2 = importDef.Fields[1];
            var field3 = importDef.Fields[2];

            Assert.That(field1.Index, Is.EqualTo(0));
            Assert.That(field1.Name, Is.EqualTo("Field-1"));
            Assert.That(field1.Include, Is.True);
            Assert.That(field1.Width, Is.EqualTo(2));

            Assert.That(field2.Index, Is.EqualTo(1));
            Assert.That(field2.Name, Is.EqualTo("Field-2"));
            Assert.That(field2.Include, Is.True);
            Assert.That(field2.Width, Is.EqualTo(7));

            Assert.That(field3.Index, Is.EqualTo(2));
            Assert.That(field3.Name, Is.EqualTo("Field-3"));
            Assert.That(field3.Include, Is.True);
            Assert.That(field3.Width, Is.EqualTo(5));
        }
    }
}
