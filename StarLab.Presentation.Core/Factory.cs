using StarLab.Presentation.Configuration;
using StarLab.Shared.Properties;

namespace StarLab.Presentation
{
    /// <summary>
    /// An abstract base class for factories that use the <see cref="Activator"/> to create object instances from the type name.
    /// </summary>
    public abstract class Factory
    {
        private readonly IFactoryConfiguration configuration; // The type configuration information.

        /// <summary>
        /// Initialises a new instance of the <see cref="Factory"/> class.
        /// </summary>
        /// <param name="configuration">The type configuration information.</param>
        /// <exception cref="ArgumentNullException"></exception>
        protected Factory(IFactoryConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        /// <summary>
        /// Gets the specified <see cref="IViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/> instance.</returns>
        protected IViewConfiguration GetViewConfiguration(string name)
        {
            return configuration.GetConfiguration(name);
        }

        /// <summary>
        /// Creates an object instance from the type name and arguments provided.
        /// </summary>
        /// <param name="typeName">The assembly-qualified type name e.g. StarLab.Application.Help.AboutView, StarLab.UI.</param>
        /// <param name="args">An <see cref="object"/> array containing the arguments that will be passed in when the constructor is invoked.</param>
        /// <returns>The newly created <see cref="object" instance/>.</returns>
        /// <exception cref="Exception">An exception will be thrown if an instance of the specified type could not be created.</exception>
        protected static object CreateInstance(string typeName, params object?[]? args)
        {
            object? instance = null;

            var type = Type.GetType(typeName);

            if (type == null)
            {
                throw new Exception(string.Format(Resources.UnknownType, typeName));
            }
            else
            {
                instance = Activator.CreateInstance(type, args);
            }

            if (instance == null) throw new Exception(string.Format(Resources.UnableToCreateInstance, typeName));
            
            return instance;
        }
    }
}
