using AutoMapper;
using StarLab.Application.Events;
using StarLab.Presentation;

using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Application.Workspace
{
    public class ToolViewPresenter : Presenter, IDockableViewPresenter
    {
        private readonly IDockableView view;

        public ToolViewPresenter(IDockableView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, configuration, mapper, events)
        {
            this.view = view;
        }

        #region IDockableViewPresenter Members

        public string Location { get; set; }

        public override void Initialise(IApplicationController controller)
        {
            base.Initialise(controller);

            switch (view.Name)
            {
                case Views.WORKSPACE_EXPLORER:
                    view.Text = StringResources.WorkspaceExplorer;
                    Location = Constants.DOCK_RIGHT;
                    break;

                default:
                    Location = Constants.DOCK_LEFT;
                    break;
            }
        }

        public void Show(IView view)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override string GetName()
        {
            return view.Name + Constants.CONTROLLER; // TODO - This wont be valid after a name change
        }
    }
}
