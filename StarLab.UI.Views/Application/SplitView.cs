using StarLab.Presentation;
using StarLab.Presentation.Model;

namespace StarLab.Application
{
    public partial class SplitView : ControlView, ISplitView
    {
        private readonly Dictionary<string, IControlView> views = new Dictionary<string, IControlView>();

        private readonly List<IControlView> panel1Views = new List<IControlView>();

        private readonly List<IControlView> panel2Views = new List<IControlView> ();

        private ISplitViewPresenter presenter;

        #region Constructors

        public SplitView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = presenterFactory.CreatePresenter(this);
        }

        #endregion

        #region ISplitView Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="child"></param>
        /// <param name="panel"></param>
        public void AddChild(IControlView child, SplitViewPanels panel)
        {
            if (child is Control control)
            {
                switch (panel)
                {
                    case SplitViewPanels.Panel1:
                        splitContainer.Panel1.Controls.Add(control);
                        panel1Views.Add(child);
                        break;

                    case SplitViewPanels.Panel2:
                        splitContainer.Panel2.Controls.Add(control);
                        panel2Views.Add(child);
                        break;
                }

                control.Dock = DockStyle.Fill;

                views.Add(child.Name, child);
            }
        }

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="button"></param>
        public void AddToolbarButton(IToolbarButton button)
        {
            toolStrip.AddButton(button.Name, button.Tooltip, button.Image, button.Command);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="panel"></param>
        /// <returns></returns>
        public IEnumerable<IControlView> GetViews(SplitViewPanels panel)
        {
            var views = new List<IControlView>();

            switch (panel)
            {
                case SplitViewPanels.Panel1:
                    views = panel1Views;
                    break;

                case SplitViewPanels.Panel2:
                    views = panel2Views;
                    break;
            }

            return views;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="view"></param>
        public void Hide(string view)
        {
            if (views.ContainsKey(view))
            {
                if (panel1Views.Contains(views[view]))
                {
                    splitContainer.Panel1Collapsed = true;
                }

                if (panel2Views.Contains(views[view]))
                {
                    splitContainer.Panel2Collapsed = true;
                }
            }
        }

        public override void Initialise(IApplicationController controller)
        {
            var size1 = GetMinimumSize(splitContainer.Panel1.Controls);
            var size2 = GetMinimumSize(splitContainer.Panel2.Controls);

            if (splitContainer.Orientation == Orientation.Vertical)
            {
                MinimumSize = new Size(size1.Width + size2.Width, size1.Height > size2.Height ? size1.Height : size2.Height);

                splitContainer.Panel1MinSize = size1.Width;
                splitContainer.Panel2MinSize = size2.Width;
            }
            else
            {
                MinimumSize = new Size(size1.Width > size2.Width ? size1.Width : size2.Width, size1.Height + size2.Height);

                splitContainer.Panel1MinSize = size1.Height;
                splitContainer.Panel2MinSize = size2.Height;
            }

            splitContainer.Panel1Collapsed = true;

            foreach (var view in views.Values)
            {
                view.Initialise(controller);
                
                if (view is ISplitViewContent content && presenter is ISplitViewController splitViewController)
                {
                    content.AttachCommands(controller, splitViewController);
                }
            }

            presenter.Initialise(controller);
        }

        /// <summary>
        /// Shows the specified view in the left hand panel.
        /// </summary>
        public void Show(string view)
        {
            if (views.ContainsKey(view))
            {
                if (panel1Views.Contains(views[view]))
                {
                    splitContainer.Panel1Collapsed = false;
                }

                if (panel2Views.Contains(views[view]))
                {
                    splitContainer.Panel2Collapsed = false;
                }
            }   
        }

        #endregion

        private Size GetMinimumSize(ControlCollection controls)
        {
            var size = new Size(0, 0);

            foreach (UserControl control in controls)
            {
                if (size.Height < control.MinimumSize.Height)
                {
                    size.Height = control.MinimumSize.Height;
                }

                if (size.Width < control.MinimumSize.Width)
                {
                    size.Width = control.MinimumSize.Width;
                }
            }

            return size;
        }
    }
}
