using log4net;
using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    /// <summary>
    /// An abstract base class for factories that use the <see cref="Activator"/> to create object instances from the type name.
    /// </summary>
    public abstract class Factory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Factory)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Creates an object instance from the type name and arguments provided.
        /// </summary>
        /// <param name="typeName">The assembly-qualified type name e.g. StarLab.Application.Help.AboutView, StarLab.UI.</param>
        /// <param name="args">An <see cref="object"/> array containing the arguments that will be passed in when the constructor is invoked.</param>
        /// <returns>The newly created <see cref="object" instance/>.</returns>
        /// <exception cref="UnknownTypeException">An exception will be thrown if an instance of the specified type could not be created.</exception>
        protected object CreateInstance(string typeName, params object?[]? args)
        {
            object? instance = null;

            var type = Type.GetType(typeName);

            if (type != null)
            {
                instance = Activator.CreateInstance(type, args);
            }
            
            if (instance == null)
            {
                if (log.IsFatalEnabled) log.Fatal(string.Format(Resources.UnknownType, typeName));
                throw new UnknownTypeException(typeName);
            }
            
            return instance;
        }
    }
}
