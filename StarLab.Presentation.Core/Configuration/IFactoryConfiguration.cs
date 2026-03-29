namespace StarLab.Presentation.Configuration
{
    /// <summary>
    /// Defines the functions used to obtain the type configuration information required by the view and presenter factories.
    /// </summary>
    public interface IFactoryConfiguration
    {
        /// <summary>
        /// Gets the specified <see cref="IViewConfiguration"/> instance.
        /// </summary>
        /// <param name="name">The name of the required <see cref="IViewConfiguration"/> instance.</param>
        /// <returns>The specified <see cref="IViewConfiguration"/> instance.</returns>
        IViewConfiguration GetConfiguration(string name);
    }
}
