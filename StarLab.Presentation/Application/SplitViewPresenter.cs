using AutoMapper;
using StarLab.Application;
using StarLab.Application.Events;

namespace StarLab.Application
{
    internal class SplitViewPresenter : ControlViewPresenter<ISplitView>, ISplitViewPresenter, ISplitViewController
    {
        public SplitViewPresenter(ISplitView view, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, useCaseFactory, configuration, mapper, events) { }

        #region ISplitViewController Members

        public void Collapse(string view)
        {
            View.Hide(view);
        }

        public void Expand(string view)
        {
            View.Show(view);
        }

        #endregion

        #region ISplitViewPresenter Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="panel"></param>
        public void AddChild(IControlView child, SplitViewPanels panel)
        {
            View.AddChild(child, panel);
        }

        public override void Initialise(IApplicationController controller)
        {
            CreateToolbar();
        }

        #endregion

        private void CreateToolbar()
        {
            foreach (var view in View.GetViews(SplitViewPanels.Panel1))
            {
                foreach (var button in view.ToolbarButtons)
                {
                    View.AddToolbarButton(button);
                }
            }

            foreach (var view in View.GetViews(SplitViewPanels.Panel2))
            {
                foreach (var button in view.ToolbarButtons)
                {
                    View.AddToolbarButton(button);
                }
            }
        }
    }
}
