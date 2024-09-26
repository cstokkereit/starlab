using AutoMapper;
using StarLab.Application.Events;
using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.Application
{
    public abstract class ControlViewPresenter<T> : Presenter, IControlViewPresenter where T : IControlView
    {
        private readonly T view;

        public ControlViewPresenter(T view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(useCaseFactory, configuration, mapper, events)
        {
            this.view = view;
        }

        #region IControlViewPresenter Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="splitViewController"></param>
        public virtual void Initialise(IApplicationController controller, ISplitViewController splitViewController)
        {
            base.Initialise(controller);
        }

        #endregion

        protected T View { get => view; }

        protected void CreateToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            View.AddButton(new ToolbarButton(name, tooltip, image, command));
        }

        protected override string GetName()
        {
            return View.Name + Constants.CONTROLLER;
        }
    }
}
