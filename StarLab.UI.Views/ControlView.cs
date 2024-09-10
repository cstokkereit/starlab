using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.UI
{
    public class ControlView : UserControl, IControlView
    {
        private readonly List<IToolbarButton> toolbarButtons = new List<IToolbarButton>();

        /// <summary>
        /// 
        /// </summary>
        public IEnumerable<IToolbarButton> ToolbarButtons => toolbarButtons;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        public void AddButton(IToolbarButton button)
        {
            toolbarButtons.Add(button);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public virtual void Initialise(IApplicationController controller)
        {
            // Do Nothing
        }
    }
}
