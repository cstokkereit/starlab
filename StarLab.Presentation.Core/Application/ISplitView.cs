using StarLab.Presentation.Model;

namespace StarLab.Application
{
    public interface ISplitView : IControlView
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="panel"></param>
        void AddChild(IControlView child, SplitViewPanels panel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        void AddToolbarButton(IToolbarButton button);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        IEnumerable<IControlView> GetViews(SplitViewPanels panel);

        /// <summary>
        /// Hides the specified view.
        /// </summary>
        void Hide(string view);

        /// <summary>
        /// Shows the specified view.
        /// </summary>
        void Show(string view);
    }
}
