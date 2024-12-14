using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using System.Reflection;

namespace StarLab.Application.Help
{
    internal class AboutViewPresenter : ChildViewPresenter<IAboutView, IDialogController>, IAboutViewPresenter, IChildViewController
    {
        private IDialogController parentController;

        public AboutViewPresenter(IAboutView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

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
