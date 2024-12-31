using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Diagnostics;

namespace StarLab.Application
{
    /// <summary>
    /// TODO
    /// </summary>
    public abstract class Presenter : Controller, IPresenter
    {
        private readonly IConfigurationService configuration; //

        private readonly ICommandManager commands; // 

        private readonly IMapper mapper; //

        private IApplicationController? controller; //

        /// <summary>
        /// Initialises a new instance of the <see cref="Presenter"/> class.
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="useCaseFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        /// <param name="events"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public Presenter(ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, events)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.commands = commands ?? throw new ArgumentNullException(nameof(commands));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Presenter"/> class.
        /// </summary>
        /// <param name="controller"></param>
        public virtual void Initialise(IApplicationController controller)
        {
            Debug.Assert(!Initialised);

            this.controller = controller ?? throw new ArgumentNullException(nameof(controller));

            AppController.RegisterCommandInvokers(commands);

            Events.Subsribe(this);
        }

        /// <summary>
        /// 
        /// </summary>
        protected IApplicationController AppController
        {
            get
            {
                Debug.Assert(controller != null);
                return controller;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        protected IConfigurationService Configuration => configuration;

        /// <summary>
        /// 
        /// </summary>
        protected bool Initialised => controller != null;

        /// <summary>
        /// 
        /// </summary>
        protected IMapper Mapper => mapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        protected ICommand GetCommand(IController controller, string action, string target)
        {
            var name = action + target;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, controller, action, target));
            }

            return commands.GetCommand(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        protected ICommand GetCommand(string action, string target)
        {
            var name = action + target;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, this, action, target));
            }

            return commands.GetCommand(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected ICommand GetCommand(IController controller, string action)
        {
            if (!commands.ContainsCommand(action))
            {
                commands.AddCommand(action, AppController.CreateCommand(commands, controller, action));
            }

            return commands.GetCommand(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        protected ICommand GetCommand(string action)
        {
            if (!commands.ContainsCommand(action))
            {
                commands.AddCommand(action, AppController.CreateCommand(commands, this, action));
            }

            return commands.GetCommand(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected ICommandChain GetCommandChain(string name)
        {
            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, new CommandChain(commands));
            }

            return (ICommandChain)commands.GetCommand(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected ICommandChain GetCommandChain()
        {
            return new CommandChain(commands);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        protected ICommand GetShowViewCommand(string view)
        {
            var name = Actions.SHOW + view;

            if (!commands.ContainsCommand(name))
            {
                commands.AddCommand(name, AppController.CreateCommand(commands, view));
            }

            return commands.GetCommand(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="target"></param>
        /// <param name="enabled"></param>
        protected void UpdateCommandState(string action, string target, bool enabled)
        {
            if (GetCommand(action + target) is IComponentCommand command) command.Enabled = enabled;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="enabled"></param>
        protected void UpdateCommandState(string action, bool enabled)
        {
            if (GetCommand(action) is IComponentCommand command) command.Enabled = enabled;
        }
    }
}
