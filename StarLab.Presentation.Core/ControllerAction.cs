namespace StarLab.Presentation
{
    public class ControllerAction<T> where T : IController
    {
        private const string CONTROLLER_ACTION = "{0}.{1}{2}";

        private readonly T controller;

        private readonly string target;

        private readonly string verb;

        #region Constructors

        public ControllerAction(T controller, string verb, string target)
        {
            this.controller = controller;
            this.target = target;
            this.verb = verb;
        }

        public ControllerAction(T controller, string verb)
            : this(controller, verb, string.Empty) { }

        #endregion

        public T Controller => controller;

        public string Name => string.Format(CONTROLLER_ACTION, controller.Name, verb, target);

        public string Target => target;

        public string Verb => verb;
    }
}
