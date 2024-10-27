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
            catch (Exception ex)
            {
                log.Fatal(ex.Message, ex);
                throw;
            }
        }

        public void Initialise(IApplicationController controller, IFormController parentController)
        {
            presenter.Initialise(controller);
        }
    }
}
