using StarLab.Application.Workspace.Documents.Tables;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// View model representation of a table.
    /// </summary>
    public class Table : ITable
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Table"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Table"/>.</param>
        public Table(TableDTO dto)
        {

        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Table"> class.
        /// </summary>
        public Table()
        {

        }
    }
}
