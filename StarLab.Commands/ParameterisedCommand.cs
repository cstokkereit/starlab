namespace StarLab.Commands
{
    /// <summary>
    /// Abstract base class for parameterised commands that act on the specified type of receiver.
    /// </summary>
    /// <typeparam name="TArguments">The type of the arguments required by this command.</typeparam>
    /// <typeparam name="TReceiver">The type of receiver that this command acts on.</typeparam>
    public abstract class ParameterisedCommand<TArguments, TReceiver> : Command<TReceiver>, IParameterisedCommand<TArguments>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ParameterisedCommand&lt;TArguments, TReceiver&gt;"/> class.
        /// </summary>
        /// <param name="receiver">The receiver that this command will act on.</param>
        public ParameterisedCommand(TReceiver receiver)
            : base(receiver) { }

        /// <summary>
        /// Executes the command with the arguments provided when overridden in a derived class.
        /// </summary>
        /// <param name="arguments">The arguments required to execute this command.</param>
        public abstract void Execute(TArguments arguments);
    }
}
