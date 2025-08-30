namespace Stratosoft.Commands
{
    /// <summary>
    /// Abstract base class for commands that act on the specified type of receiver.
    /// </summary>
    /// <typeparam name="TReceiver">The type of receiver that the command acts on.</typeparam>
    public abstract class Command<TReceiver> : ICommand
    {
        protected readonly TReceiver receiver; // The instance of the specified type that the command acts on.

        /// <summary>
        /// Initialises a new instance of the <see cref="Command{TReceiver}"/> class.
        /// </summary>
        /// <param name="receiver">The receiver that the command acts on.</param>
        public Command(TReceiver receiver)
        {
            if (receiver == null) throw new ArgumentNullException(nameof(receiver));

            this.receiver = receiver;
        }

        /// <summary>
        /// Executes the command when overridden in a derived class.
        /// </summary>
        public abstract void Execute();
    }
}
