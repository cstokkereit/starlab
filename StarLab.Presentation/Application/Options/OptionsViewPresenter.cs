using AutoMapper;
using StarLab.Application;
using StarLab.Application.Events;
using StarLab.Commands;
using StarLab.Shared.Properties;

namespace StarLab.Application.Options
{
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-application-settings?view=netframeworkdesktop-4.8

    internal class OptionsViewPresenter : ControlViewPresenter<IOptionsView>, IOptionsViewPresenter
    {
        public OptionsViewPresenter(IOptionsView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public override string Name => throw new NotImplementedException();

        public override void Initialise(IApplicationController applicationController)
        {
            base.Initialise(applicationController);
        }
    }
}
