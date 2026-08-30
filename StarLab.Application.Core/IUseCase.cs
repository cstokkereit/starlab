namespace StarLab.Application
{
    /// <summary>
    /// Defines a use case.
    /// </summary>
    /// <typeparam name="T">The use case argument type.</typeparam>
    public interface IUseCase<T>
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The use case arguments that provide all of the information required to execute the use case.</param>
        void Execute(T args);
    }
}
