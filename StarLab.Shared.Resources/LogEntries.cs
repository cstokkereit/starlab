namespace StarLab.Shared
{
    /// <summary>
    /// A static class containing the string constants used to create information and warning log entries. Error messages should be logged directly using the exception.
    /// </summary>
    public static class LogEntries
    {
        public const string StartingApplication = "Starting the application.";

        /// <summary>
        /// Builds an ActiveViewChanged log entry for the specified views.
        /// </summary>
        /// <param name="oldView">The ID of the old view.</param>
        /// <param name="newView">The ID of the new view.</param>
        /// <returns>The required log entry.</returns>
        public static string ActiveViewChanged(object oldView, object newView)
        {
            return $"The active view has changed from {oldView} to {newView}.";
        }

        /// <summary>
        /// Builds a PresenterAttached log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <param name="name">The view name</param>
        /// <param name="id">The view ID.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterAttached(Type type, string name, object id)
        {
            return $"The {type.Name}({name} ({id})) has been attached.";
        }

        /// <summary>
        /// Builds a PresenterAttached log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <param name="name">The view name</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterAttached(Type type, string name)
        {
            return $"The {type.Name}({name}) has been attached.";
        }

        /// <summary>
        /// Builds a PresenterAttached log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterAttached(Type type)
        {
            return $"The {type.Name} has been attached.";
        }

        /// <summary>
        /// Builds a PresenterDetached log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <param name="name">The view name</param>
        /// <param name="id">The view ID.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterDetached(Type type, string name, object id)
        {
            return $"The {type.Name}({name} ({id})) has been detached.";
        }

        /// <summary>
        /// Builds a PresenterDetached log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <param name="name">The view name</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterDetached(Type type, string name)
        {
            return $"The {type.Name}({name}) has been detached.";
        }

        /// <summary>
        /// Builds a PresenterDetached log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterDetached(Type type)
        {
            return $"The {type.Name} has been detached.";
        }

        /// <summary>
        /// Builds a PresenterInitialised log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <param name="name">The view name.</param>
        /// <param name="id">The view ID.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterInitialised(Type type, string name, object id)
        {
            return $"The {type.Name}({name} ({id})) has been initialised.";
        }

        /// <summary>
        /// Builds a PresenterInitialised log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <param name="name">The view name.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterInitialised(Type type, string name)
        {
            return $"The {type.Name}({name}) has been initialised.";
        }

        /// <summary>
        /// Builds a PresenterInitialised log entry for the specified presenter.
        /// </summary>
        /// <param name="type">The presenter type name.</param>
        /// <returns>The required log entry.</returns>
        public static string PresenterInitialised(Type type)
        {
            return $"The {type.Name} has been initialised.";
        }

        /// <summary>
        /// Builds an ServiceInitialised log entry for the specified service.
        /// </summary>
        /// <param name="type">The service type name.</param>
        public static string ServiceInitialised(Type type)
        {
            return $"The {type.FullName ?? type.Name} has been initialised.";
        }

        /// <summary>
        /// Builds a ViewClosed log entry for the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="name">The document name.</param>
        /// <returns>The required log entry.</returns>
        public static string ViewClosed(object id, string name)
        {
            return $"The document {name} ({id}) has been closed.";
        }

        /// <summary>
        /// Builds a ViewClosed log entry for the specified view.
        /// </summary>
        /// <param name="view">The view name.</param>
        /// <returns>The required log entry.</returns>
        public static string ViewClosed(string view)
        {
            return $"The {view} has been closed.";
        }

        /// <summary>
        /// Builds a ViewCreated log entry for the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        /// <param name="name">The document name.</param>
        /// <returns>The required log entry.</returns>
        public static string ViewCreated(object id, string name)
        {
            return $"The document {name} ({id}) has been created.";
        }

        /// <summary>
        /// Builds a ViewCreated log entry for the specified view.
        /// </summary>
        /// <param name="view">The view name.</param>
        /// <returns>The required log entry.</returns>
        public static string ViewCreated(string view)
        {
            return $"The {view} view has been created.";
        }
    }
}
