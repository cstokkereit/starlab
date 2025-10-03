namespace StarLab.Application
{
    /// <summary>
    /// Defines a use case that requires a single execution argument.
    /// </summary>
    /// <typeparam name="T">The execution argument type.</typeparam>
    public interface IUseCase<T>
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="arg">The use case argument.</param>
        void Execute(T arg);
    }

    /// <summary>
    /// Defines a use case that requires two execution arguments.
    /// </summary>
    /// <typeparam name="T1">The first execution argument type.</typeparam>
    /// <typeparam name="T2">The second execution argument type.</typeparam>
    public interface IUseCase<T1, T2>
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="arg1">The first use case argument.</param>
        /// <param name="arg2">The second use case argument.</param>
        void Execute(T1 arg1, T2 arg2);
    }

    /// <summary>
    /// Defines a use case that requires three execution arguments.
    /// </summary>
    /// <typeparam name="T1">The first execution argument type.</typeparam>
    /// <typeparam name="T2">The second execution argument type.</typeparam>
    /// <typeparam name="T3">The third execution argument type.</typeparam>
    public interface IUseCase<T1, T2, T3>
    {
        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="arg1">The first use case argument.</param>
        /// <param name="arg2">The second use case argument.</param>
        /// <param name="arg3">The third use case argument.</param>
        void Execute(T1 arg1, T2 arg2, T3 arg3);
    }
}
