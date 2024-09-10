using ScottPlot;
using StarLab.Presentation;
using StarLab.Presentation.Charts;
using System.Text.RegularExpressions;

namespace StarLab.UI.Charts
{
    //https://scottplot.net/cookbook/5.0/

    public partial class ColourMagnitudeChartView : ControlView, IChartView
    {
        private readonly IChartViewPresenter presenter;

        public ColourMagnitudeChartView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IChartViewPresenter)presenterFactory.CreatePresenter(this);
        }

        #region IChartView Members

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/> </param>
        public void Initialise(IApplicationController controller, ISplitViewController splitViewController)
        {
            Initialise(controller);
        }

        public override void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);



            // This is all temporary

            Tuple<double[], double[]> data = GetData();

            var scatter = formsPlot.Plot.Add.ScatterPoints(data.Item1, data.Item2);
            scatter.MarkerColor = Colors.Green;
            scatter.MarkerSize = 1;

            formsPlot.Refresh();

            formsPlot.Plot.Axes.Bottom.Label.Text = "Spectral Class";

            double[] tickPositions = new double[70];
            string[] tickLabels = new string[70]; // = { "O", "B", "A", "F", "G", "K", "M" };

            string c = "O";
            int s = 2;

            for (int n = 2; n < 70; n++)
            {
                if (s > 9)
                {
                    s = 0;
                }

                if (n > 9)
                {
                    c = "B";
                }
                if (n > 19)
                {
                    c = "A";
                }
                if (n > 29)
                {
                    c = "F";
                }
                if (n > 39)
                {
                    c = "G";
                }
                if (n > 49)
                {
                    c = "K";
                }
                if (n > 59)
                {
                    c = "M";
                }

                tickPositions[n] = n;
                tickLabels[n] = c + s.ToString();
                s++;
            }

            //ScottPlot.TickGenerators.NumericAutomatic tickGenX = new();
            //tickGenX.TargetTickCount = 10;
            //formsPlot.Plot.Axes.Bottom.TickGenerator = tickGenX;

            //ScottPlot.TickGenerators.NumericAutomatic tickGenX = new();
            //tickGenX.MinimumTickSpacing = 5;
            //formsPlot.Plot.Axes.Bottom.TickGenerator = tickGenX;

            formsPlot.Plot.Axes.SetLimits(2, 69, 20, -10);

            formsPlot.Plot.Axes.Bottom.SetTicks(tickPositions, tickLabels);

            formsPlot.Plot.Axes.Left.Label.Text = "Luminosity";

            formsPlot.Plot.Axes.Title.Label.Text = "Plot Title";


            formsPlot.Plot.Grid.XAxisStyle.IsVisible = false;
            formsPlot.Plot.Grid.YAxisStyle.IsVisible = false;


            // some items must be styled directly
            formsPlot.Plot.Grid.MajorLineColor = Colors.Green;
            formsPlot.Plot.FigureBackground.Color = Colors.Black;
            formsPlot.Plot.DataBackground.Color = Colors.Black;

            // the Style object contains helper methods to style many items at once
            formsPlot.Plot.Axes.Color(Colors.Green);

            // reset limits to fit the data
            //formsPlot.Plot.Axes.AutoScale();

        }

        #endregion

        Regex regex = new Regex(@"([OBAFGKM]\d[\.\d]*[\/]?[OBAFGKM]?[\d\.\d]?)(I{0,3}V?[-|\/]?I{0,3}V?)(.*)", RegexOptions.Compiled);

        private Tuple<double[], double[]> GetData()
        {
            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            using (var reader = File.OpenText("D:\\Users\\Colin\\Documents\\WIP\\StarLab\\Photometry.csv"))
            {
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    try
                    {
                        var data = line.Split(',');

                        var absoluteMagnitude = data[6].Trim();
                        var spectralType = data[7].Trim();

                        if (!string.IsNullOrEmpty(absoluteMagnitude) && !string.IsNullOrEmpty(spectralType))
                        {
                            xValues.Add(Parse(spectralType));
                            yValues.Add(double.Parse(absoluteMagnitude));
                        }
                    }
                    catch (Exception e)
                    {
                        var a = e;

                        //logger.Debug("Unable to parse spectral type: " + spectralType); // TODO
                    }
                }
            }

            return new Tuple<double[], double[]>(xValues.ToArray(), yValues.ToArray());
        }

        private double Parse(string s)
        {
            var parts = regex.Split(s);

            string spectralType = "";

            double retval = 0;

            if (parts.Length == 5)
            {
                spectralType = parts[1];

                if (!spectralType.Contains('/'))
                {
                    if (spectralType.StartsWith("B"))
                    {
                        retval = 10;
                    }
                    if (spectralType.StartsWith("A"))
                    {
                        retval = 20;
                    }
                    if (spectralType.StartsWith("F"))
                    {
                        retval = 30;
                    }
                    if (spectralType.StartsWith("G"))
                    {
                        retval = 40;
                    }
                    if (spectralType.StartsWith("K"))
                    {
                        retval = 50;
                    }
                    if (spectralType.StartsWith("M"))
                    {
                        retval = 60;
                    }

                    retval = retval + double.Parse(spectralType.Substring(1));
                }
            }

            return retval;
        }

    }
}
