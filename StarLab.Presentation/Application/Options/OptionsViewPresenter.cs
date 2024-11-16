using AutoMapper;
using StarLab.Commands;

namespace StarLab.Application.Options
{
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-application-settings?view=netframeworkdesktop-4.8

    internal class OptionsViewPresenter : ControlViewPresenter<IOptionsView, IDialogController>, IOptionsViewPresenter
    {
        public OptionsViewPresenter(IOptionsView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override void Initialise(IApplicationController controller, IDialogController parentController)
        {
            base.Initialise(controller, parentController);
        }
    }
}
