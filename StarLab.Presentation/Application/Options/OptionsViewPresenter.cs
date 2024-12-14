using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application.Options
{
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-application-settings?view=netframeworkdesktop-4.8

    internal class OptionsViewPresenter : ChildViewPresenter<IOptionsView, IDialogController>, IOptionsViewPresenter, IChildViewController
    {
        public OptionsViewPresenter(IOptionsView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

            }
        }
    }
}
