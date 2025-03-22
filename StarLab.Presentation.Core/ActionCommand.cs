using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class that inherits from <see cref="ComponentCommand{IController}"/> and executes the specified action on the <see cref="IController"> provided.
    /// </summary>
    public class ActionCommand : ComponentCommand<IController>
    {
        private readonly string action; // Specifies the method on the receiver that will be invoked when the Execute method is called.

        private readonly object[] args; // Contains the arguments that will be passed in when the specified method on the receiver is invoked.

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="commands">The <see cref="ICommandManager"/> used to create and manage commands.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked when the <see cref="Execute"/> method is called.</param>
        /// <param name="action">The action to be performed when the <see cref="Execute"/> method is called.</param>
        /// <param name="args">An <see cref="object"/> array containing the arguments that will be passed in when the specified method on the receiver is invoked.</param>
        public ActionCommand(ICommandManager commands, IController controller, string action, object[] args)
            : base(commands, controller)
        {
            this.action = action;
            this.args = args;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ActionCommand"/> class.
        /// </summary>
        /// <param name="commands">The <see cref="ICommandManager"/> used to create and manage commands.</param>
        /// <param name="controller">The <see cref="IController"/> that contains the method that will be invoked when the <see cref="Execute"/> method is called.</param>
        /// <param name="action">The action to be performed when the <see cref="Execute"/> method is called.</param>
        public ActionCommand(ICommandManager commands, IController controller, string action)
            : this(commands, controller, action, Array.Empty<object>()) { }

        /// <summary>
        /// Invokes the specified method on the <see cref="IController"> supplied.
        /// </summary>
        public override void Execute()
        {
            var type = receiver.GetType();
            var types = GetArgumentTypes();
            var method = type.GetMethod(action, types);
            method?.Invoke(receiver, args);
        }

        /// <summary>
        /// Determines the types of the arguments that will be passed in when the specified method on the receiver is invoked.
        /// </summary>
        /// <returns>A <see cref="Type"/> array containg the argument types.</returns>
        private Type[] GetArgumentTypes()
        {
            var types = new List<Type>();

            foreach (var arg in args)
            {
                types.Add(arg.GetType());
            }

            return types.ToArray();
        }
    }
}
