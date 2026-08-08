using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Tables;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.Tests
{
    /// <summary>
    /// A helper class that uses the Builder Pattern to construct the <see cref="TableDTO"/>s used in unit tests.
    /// </summary>
    public class TableDtoBuilder
    {
        private TableDTO table; // The DTO being constructed.

        /// <summary>
        /// Initialises a new instance of the <see cref="TableDtoBuilder"/> class.
        /// </summary>
        public TableDtoBuilder()
        {
            table = new TableDTO
            {
                
            };
        }

        /// <summary>
        /// Creates the <see cref="TableDTO"/>.
        /// </summary>
        /// <returns>The required <see cref="TableDTO"/>.</returns>
        public TableDTO CreateTable()
        {
            return table;
        }
    }
}
