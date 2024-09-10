namespace StarLab.Commands
{
    /// <summary>
    /// Abstract base class for commands that act on the specified type of receiver.
    /// </summary>
    /// <typeparam name="TReceiver">The type of receiver that this command acts on.</typeparam>
    public abstract class Command<TReceiver> : ICommand
    {
        protected TReceiver receiver; // The instance of the specified type that this command acts on.

        /// <summary>
        /// Initialises a new instance of the <see cref="Command&lt;TReceiver&gt;"/> class.
        /// </summary>
        /// <param name="receiver">The receiver that this command will act on.</param>
        public Command(TReceiver receiver)
        {
            if (receiver == null) throw new ArgumentNullException("receiver");

            this.receiver = receiver;
        }

        /// <summary>
        /// Executes the command when overridden in a derived class.
        /// </summary>
        public abstract void Execute();
    }
}
