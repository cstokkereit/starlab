using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Reflection;

namespace StarLab.Application.Help
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IAboutView"/>.
    /// </summary>
    internal class AboutViewPresenter : ChildViewPresenter<IAboutView, IDialogController>, IAboutViewPresenter, IChildViewController
    {
        private IDialogController parentController; // The controller that can be used to control the parent dialog box. TODO - probably not needed

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IAboutView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IConfigurationProvider"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public AboutViewPresenter(IAboutView view, ICommandManager commands, IUseCaseFactory factory, Configuration.IConfigurationProvider configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, configuration, mapper, events) { }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                //View.SetCompanyName(Resources.CompanyName);
                //View.SetCopyright(Resources.Copyright);
                //View.SetDescription(Resources.ProductDescription);
                //View.SetLogo("");
                //View.SetProductName(Resources.StarLab);
                //View.SetVersion(string.Format(Resources.Version, GetVersion()));
            }
        }

        //public override void Run(IDialogConfiguration config)
        //{
        //    base.Run(config);

        //    ParentController.Show();
        //}

        /// <summary>
        /// Gets the version information.
        /// </summary>
        /// <returns>The version information.</returns>
        private string GetVersion()
        {
            string version = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                AssemblyName name = assembly.GetName();

                if (name != null && name.Version != null)
                {
                    version = name.Version.ToString();
                }
            }

            return version;
        }
    }
}
