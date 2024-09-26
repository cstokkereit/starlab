using AutoMapper;
using StarLab.Application;
using StarLab.Application.Events;
using StarLab.Shared.Properties;
using System.Reflection;

namespace StarLab.Application.Help
{
    internal class AboutViewPresenter : FormViewPresenter<IAboutView>, IAboutViewPresenter
    {
        public AboutViewPresenter(IAboutView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events) { }

        #region IAboutViewPresenter Members

        public override void Initialise(IApplicationController applicationController)
        {
            base.Initialise(applicationController);

            View.Text = Resources.AboutStarLab;

            View.SetCompanyName(Resources.CompanyName);
            View.SetCopyright(Resources.Copyright);
            View.SetDescription(Resources.ProductDescription);
            //View.SetLogo("");
            View.SetProductName(Resources.StarLab);
            View.SetVersion(string.Format(Resources.Version, GetVersion()));
        }

        #endregion

        #region IViewController Members

        public override void Show(IView view)
        {
            throw new NotImplementedException();
        }

        #endregion

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
