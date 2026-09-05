using System.Security.Policy;
using System.Xml.Linq;

namespace StarLab.Shared
{
    /// <summary>
    /// A static class containing the string constants used to create exception messages.
    /// </summary>
    public static class ExceptionMessages
    {
        public const string ComponentFieldIndicesRequired = "The indices of the component fields are required.";

        public const string InvalidFieldIndex = "The field index must be a non-negative integer.";

        public const string UnrecognisedFileType = "Unrecognised file type.";

        public const string WidthNotApplicable = "The width is not applicable for delimited text files.";

        public const string WidthRequired = "The width is required for fixed width text files.";

        /// <summary>
        /// Builds a FieldAlreadyAdded exception message for the specified field.
        /// </summary>
        /// <param name="name">The name of the field.</param>
        /// <returns>The required exception message.</returns>
        public static string FieldAlreadyAdded(string name)
        {
            return $"A field with the name '{name}' has already been added.";
        }

        /// <summary>
        /// Builds a PresenterAlreadyInitialised exception message for the specified presenter type.
        /// </summary>
        /// <param name="type">The type of the presenter.</param>
        /// <returns>The required exception message.</returns>
        public static string PresenterAlreadyInitialised(Type type)
        {
            return $"The {type.Name} has already been initialised.";
        }

        /// <summary>
        /// Builds a TableAlreadyAdded exception message for the specified table.
        /// </summary>
        /// <param name="name">The name of the table.</param>
        /// <returns>The required exception message.</returns>
        public static string TableAlreadyAdded(string name)
        {
            return $"A table with the name '{name}' has already been added.";
        }

        /// <summary>
        /// Builds a WorkspaceNotLoaded exception message for the specified workspace.
        /// </summary>
        /// <param name="filename">The path to the workspace file.</param>
        /// <returns>The required exception message.</returns>
        public static string WorkspaceNotLoaded(string filename)
        {
             return $"The workspace {filename} could not be loaded.";
        }
    }
}
