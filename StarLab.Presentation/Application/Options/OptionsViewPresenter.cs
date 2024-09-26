using AutoMapper;
using StarLab.Application;
using StarLab.Application.Events;
using StarLab.Shared.Properties;

namespace StarLab.Application.Options
{
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-application-settings?view=netframeworkdesktop-4.8

    internal class OptionsViewPresenter : FormViewPresenter<IOptionsView>, IOptionsViewPresenter
    {
        public OptionsViewPresenter(IOptionsView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events) { }

        public override void Initialise(IApplicationController applicationController)
        {
            base.Initialise(applicationController);

            View.Text = Resources.Options;
        }

        public override void Show(IView view)
        {
            throw new NotImplementedException();
        }
    }
}
