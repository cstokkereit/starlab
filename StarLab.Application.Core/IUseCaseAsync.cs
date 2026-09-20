namespace StarLab.Application
{
    /// <summary>
    /// Defines an asynchronous use case.
    /// </summary>
    /// <typeparam name="T">The use case argument type.</typeparam>
    public interface IUseCaseAsync<T>
    {
        /// <summary>
        /// Executes the use case asynchronously.
        /// </summary>
        /// <param name="args">The use case arguments that provide all of the information required to execute the use case.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        Task ExecuteAsync(T Args);
    }
}