using StarLab.Presentation.Model;

namespace StarLab.Presentation
{
    public interface IControlView
    {
        /// <summary>
        /// 
        /// </summary>
        Size MinimumSize { get; set; }

        /// <summary>
        /// Gets or sets the view text.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        Size Size { get; }

        /// <summary>
        /// 
        /// </summary>
        IEnumerable<IToolbarButton> ToolbarButtons { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="button"></param>
        void AddButton(IToolbarButton button);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        void Initialise(IApplicationController controller);

        /// <summary>
        /// 
        /// </summary>
        void Refresh();
    }
}
