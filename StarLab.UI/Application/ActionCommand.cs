using StarLab.Commands;

namespace StarLab.Application
{
    internal class ActionCommand : ComponentCommand<IController>
    {
        private readonly string action;

        private readonly object[] args;

        public ActionCommand(ICommandManager commands, IController controller, string action, object[] args)
            : base(commands, controller) 
        {
            this.action = action;
            this.args = args;
        }

        public ActionCommand(ICommandManager commands, IController controller, string action)
            : this(commands, controller, action, Array.Empty<object>()) { }

        public override void Execute()
        {
            var type = receiver.GetType();
            var types = GetArgumentTypes();
            var method = type.GetMethod(action, types);
            method?.Invoke(receiver, args);
        }

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
