using System.Configuration;
using System.Security.Policy;

namespace StarLab.Shared
{
    /// <summary>
    /// A static class containing the string constants used to create exception messages.
    /// </summary>
    public static class ExceptionMessages
    {
        public const string ComponentFieldIndicesRequired = "The indices of the component fields are required.";

        public const string CursorAtBeginningOfFile = "The cursor is already positioned at the beginning of the file.";

        public const string CursorAtEndOfFile = "The cursor is already positioned at the end of the file.";

        public static string DocumentNotSet = "The document has not been set.";

        public const string InvalidFieldIndex = "The field index must be a non-negative integer.";

        public const string InvalidPath = "The path cannot be an empty string.";

        public const string InvalidState = "One or more state variables have not been initialised.";

        public static string PresenterAlreadyAttached = "The presenter has already been attached to this view.";

        public const string UnrecognisedFileType = "Unrecognised file type.";

        public const string WidthNotApplicable = "The width is not applicable for delimited text files.";

        public const string WidthRequired = "The width is required for fixed width text files.";

        /// <summary>
        /// Builds a ControllerNotFound exception message for the specified controller id.
        /// </summary>
        /// <param name="id">The controller ID.</param>
        /// <returns>The required exception message.</returns>
        public static string ConfigurationNotFound(string view)
        {
            return $"The {view} configuration could not be found.";
        }

        /// <summary>
        /// Builds a ControllerNotFound exception message for the specified controller id.
        /// </summary>
        /// <param name="id">The controller ID.</param>
        /// <returns>The required exception message.</returns>
        public static string ControllerNotFound(object id)
        {
            return $"A controller with ID '{id}' could not be found.";
        }

        /// <summary>
        /// Builds a DocumentCouldNotBeDeleted exception message for the specified document name and id.
        /// </summary>
        /// <param name="name">The document name.</param>
        /// <param name="id">The document ID.</param>
        /// <returns>The required exception message.</returns>
        public static string DocumentCouldNotBeDeleted(string name, object id)
        {
            return $"The document {name} ({id}) could not be deleted.";
        }

        /// <summary>
        /// Builds a DocumentNotFound exception message for the specified document name and path.
        /// </summary>
        /// <param name="name">The document name.</param>
        /// <param name="path">The document path.</param>
        /// <returns>The required exception message.</returns>
        public static string DocumentNotFound(string name, string path)
        {
            return $"A document with the name '{0}' could not be found in the folder {1}";
        }

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
        /// Builds a FileCouldNotBeLoaded exception message for the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        /// <returns>The required exception message.</returns>
        public static string FileCouldNotBeLoaded(string filename)
        {
            return $"The file {filename} could not be loaded.";
        }

        /// <summary>
        /// Builds a FileCouldNotBeSaved exception message for the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        /// <returns>The required exception message.</returns>
        public static string FileCouldNotBeSaved(string filename)
        {
            return $"The file {filename} could not be saved.";
        }

        /// <summary>
        /// Builds a FileNotFound exception message for the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        /// <returns>The required exception message.</returns>
        public static string FileNotFound(string filename)
        {
            return $"The file {filename} could not be found.";
        }

        /// <summary>
        /// Builds a FolderCouldNotBeAdded exception message for the specified parent folder.
        /// </summary>
        /// <param name="parent">The path to the parent folder.</param>
        /// <returns>The required exception message.</returns>
        public static string FolderCouldNotBeAdded(string parent)
        {
            return $"A folder could not be added to the folder {parent}.";
        }

        /// <summary>
        /// Builds a FolderCouldNotBeDeleted exception message for the specified folder.
        /// </summary>
        /// <param name="folder">The path to the folder.</param>
        /// <returns>The required exception message.</returns>
        public static string FolderCouldNotBeDeleted(string folder)
        {
            return $"The folder {folder} could not be deleted.";
        }

        /// <summary>
        /// Builds a ControllerNotInitialised exception message for the specified controller.
        /// </summary>
        /// <param name="controller">The name of the controller.</param>
        /// <returns>The required exception message.</returns>
        public static string ControllerNotInitialised(string controller)
        {
            return $"The {controller} controller has not been initialised.";
        }

        /// <summary>
        /// Builds a DocumentAlreadyExists exception message for the specified document.
        /// </summary>
        /// <param name="name">The document name.</param>
        /// <returns>The required exception message.</returns>
        public static string DocumentExists(string name)
        {
            return $"A document with the name '{name}' already exists.";
        }

        /// <summary>
        /// Builds a FileExists exception message for the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        /// <returns>The required exception message.</returns>
        public static string FileExists(string filename)
        {
            return $"The file '{filename}' already exists.";
        }

        /// <summary>
        /// Builds a FolderAlreadyExists exception message for the specified folder.
        /// </summary>
        /// <param name="path">The folder path.</param>
        /// <returns>The required exception message.</returns>
        public static string FolderExists(string path)
        {
            return $"The folder '{path}' already exists.";
        }

        /// <summary>
        /// Builds a InstanceCouldNotBeCreated exception message for the specified type.
        /// </summary>
        /// <param name="type">The type name.</param>
        /// <returns>The required exception message.</returns>
        public static string InstanceCouldNotBeCreated(string type)
        {
            return "An instance of the type '{type}' could not be created.";
        }

        /// <summary>
        /// Builds a InterfaceNotImplemented exception message for the specified type and interface.
        /// </summary>
        /// <param name="type">The implementing type.</param>
        /// <param name="required">The required interface.</param>
        /// <returns>The required exception message.</returns>
        public static string InterfaceNotImplemented(Type type, Type required)
        {
            return $"{type.Name} dees not implement {required.Name}.";
        }

        /// <summary>
        /// Builds an InvalidDestination exception message for the specified folder.
        /// </summary>
        /// <param name="destination">The destination folder.</param>
        /// <returns>The required exception message.</returns>
        public static string InvalidDestination(string destination)
        {
            return $"{destination} is not a valid destination.";
        }

        /// <summary>
        /// Builds an InvalidSource exception message for the specified document or folder.
        /// </summary>
        /// <param name="source">The source document or folder.</param>
        /// <returns>The required exception message.</returns>
        public static string InvalidSource(string source)
        {
            return $"{source} is not a valid source.";
        }

        /// <summary>
        /// Builds a ParentControllerNotInitialised exception message.
        /// </summary>
        /// <returns>The required exception message.</returns>
        public static string ParentControllerNotInitialised()
        {
            return ControllerNotInitialised("parent");
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
        /// Builds a ParentProjectNotFound exception message for the specified child folder.
        /// </summary>
        /// <param name="folder">The child folder.</param>
        /// <returns>The required exception message.</returns>
        public static string ParentProjectNotFound(string folder)
        {
            return $"A project containing the folder {folder} could not be found";
        }

        /// <summary>
        /// Builds an ObjectNotInitialised exception message for the specified object type.
        /// </summary>
        /// <param name="type">The type of the object.</param>
        /// <returns>The required exception message.</returns>
        public static string ObjectNotInitialised(Type type)
        {
            return $"The {type.Name} has not been initialised.";
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
        /// Builds an UnexpectedArgumentType exception message for the specified type.
        /// </summary>
        /// <param name="expected">The expected type.</param>
        /// <param name="actual">The actual type.</param>
        /// <returns>The required exception message.</returns>
        public static string UnexpectedArgumentType(Type expected, Type actual)
        {
            return $"The argument was not of the correct type. Expected {0} but was {1}";
        }

        /// <summary>
        /// Builds an UnexpectedArgumentType exception message for the specified type.
        /// </summary>
        /// <param name="type">The argument type.</param>
        /// <returns>The required exception message.</returns>
        public static string UnexpectedArgumentType(Type type)
        {
            return $"Unexpected argument type {type.Name}.";
        }

        /// <summary>
        /// Builds an UnknownType exception message for the specified type.
        /// </summary>
        /// <param name="type">The unknown type.</param>
        /// <returns>The required exception message.</returns>
        public static string UnknownType(Type type)
        {
            return $"Unknown type: {type.Name}.";
        }

        /// <summary>
        /// Builds an UnknownType exception message for the specified type.
        /// </summary>
        /// <param name="type">The unknown type name.</param>
        /// <returns>The required exception message.</returns>
        public static string UnknownType(string type)
        {
            return $"Unknown type: {type}.";
        }

        /// <summary>
        /// Builds a ViewNotFound exception message for the specified view id.
        /// </summary>
        /// <param name="id">The view ID.</param>
        /// <returns>The required exception message.</returns>
        public static string ViewNotFound(object id)
        {
            return $"A view with ID '{id}' could not be found.";
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
