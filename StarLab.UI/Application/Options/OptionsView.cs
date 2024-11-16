using log4net;

namespace StarLab.Application.Options
{
    public partial class OptionsView : UserControl, IOptionsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsView));

        private readonly IOptionsViewPresenter presenter;

        public OptionsView(IPresenterFactory factory)
        {
            InitializeComponent();

            Name = Views.OPTIONS;

            try
            {
                presenter = (IOptionsViewPresenter)factory.CreatePresenter(this);
            }
            catch (Exception e)
            {
                log.Fatal(e.Message, e);
                throw;
            }
        }

        public IChildViewController Controller => (IChildViewController)presenter;
    }
}
