using StarLab.Presentation;
using System.ComponentModel;

namespace StarLab.UI.Core
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design")]
    public partial class ChildView : UserControl
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChildView"/> class.
        /// </summary>
        /// <param name="id">The view ID.</param>
        /// <param name="panel">The preferred panel in which to display the view.</param>
        public ChildView(ViewID id, SplitViewPanels panel)
        {
            InitializeComponent();

            Clipboard = new Clipboard();

            Panel = panel;
            ID = id;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChildView"/> class.
        /// </summary>
        /// <param name="id"></param>
        public ChildView(ViewID id)
            : this(id, SplitViewPanels.Any) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ChildView"/> class.
        /// </summary>
        public ChildView()
            : this(new ViewID()) { }

        /// <summary>
        /// Gets the <see cref="IClipboard"/>.
        /// </summary>
        public IClipboard Clipboard { get; private set; }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public ViewID ID { get; }

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel { get; }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public virtual void Attach(IChildViewPresenter presenter)
        {
            throw new NotImplementedException(); // This method must be overridden in the derived class;
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public virtual void Detach()
        {
            throw new NotImplementedException(); // This method must be overridden in the derived class;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        public virtual void Initialise()
        {
            // Do Nothing
        }
    }
}
