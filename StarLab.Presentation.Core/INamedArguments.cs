namespace StarLab.Presentation
{
    /// <summary>
    /// Represents a collection of named arguments.
    /// </summary>
    public interface INamedArguments
    {
        /// <summary>
        /// Adds the argument provided with the specified name.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <param name="argument">The argument being added.</param>
        /// <returns>A reference to this instance allowing fluent addition of arguments.</returns>
        INamedArguments Add(string name, object argument);

        /// <summary>
        /// Gets the argument with the specified name.
        /// </summary>
        /// <typeparam name="T">The argument type.</typeparam>
        /// <param name="name">The name of the argument.</param>
        /// <returns>The argument with the specified name.</returns>
        T GetArgument<T>(string name);
    }
}
