using System.Diagnostics;

namespace Stratosoft.Commands
{
    /// <summary>
    /// Abstract base class for parameterised commands that act on the specified type of receiver.
    /// </summary>
    /// <typeparam name="TReceiver">The type of receiver that this command acts on.</typeparam>
    /// <typeparam name="TArguments">The type of the arguments required by this command.</typeparam>
    public abstract class ParameterisedCommand<TReceiver, TArguments> : IParameterisedCommand<TReceiver, TArguments>
    {
        private TReceiver? receiver; // The instance of the specified type that the command acts on.

        /// <summary>
        /// Initialises a new instance of the <see cref="ParameterisedCommand{TReceiver, TArguments}"/> class.
        /// </summary>
        /// <param name="receiver">The receiver that this command will act on.</param>
        public ParameterisedCommand(TReceiver receiver)
        {
            this.receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ParameterisedCommand{TReceiver, TArguments}"/> class.
        /// </summary>
        public ParameterisedCommand() { }

        /// <summary>
        /// Updates the receiver and then executes the command with the arguments provided when overridden in a derived class.
        /// </summary>
        /// <param name="receiver">The receiver that this command will act on.</param>
        /// <param name="arguments">The arguments required to execute this command.</param>
        public void Execute(TReceiver receiver, TArguments arguments)
        {
            this.receiver = receiver ?? throw new ArgumentNullException(nameof(receiver));

            Execute(arguments);
        }

        /// <summary>
        /// Executes the command with the arguments provided when overridden in a derived class.
        /// </summary>
        /// <param name="arguments">The arguments required to execute this command.</param>
        public abstract void Execute(TArguments arguments);

        /// <summary>
        /// Executes the command when overridden in a derived class.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public virtual void Execute()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the command receiver.
        /// </summary>
        protected TReceiver Receiver 
        {
            get 
            {
                Debug.Assert(receiver != null);

                return receiver;
            }
        
        }
    }
}
