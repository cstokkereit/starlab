using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Domain.Core;
using StarLab.Domain.Data;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Provides access to the data used to generate the chart overlays.
    /// </summary>
    public class OverlayDataProvider : IOverlayDataProvider
    {
        private readonly Dictionary<string, IOverlayData> dataByOverlay = new Dictionary<string, IOverlayData>(); //

        private ILabelledPoint? sun;

        private readonly IFileImportProvider provider; //

        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayDataProvider"/> class.
        /// </summary>
        /// <param name="provider"></param>
        public OverlayDataProvider(IFileImportProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));

            //var sunSymbol = "\u2A00";

            
            
        }

        /// <summary>
        /// Gets the data for the specified overlay.
        /// </summary>
        /// <param name="filename">The name of the file containing the overlay data.</param>param>
        /// <returns>An <see cref="IMagnitudeClassesOverlayData"/> that contains the overlay data.</returns>
        public IMagnitudeClassesOverlayData GetMagnitudeClassesOverlayData()
        {
            return new MagnitudeClassesOverlayData();
        }

        /// <summary>
        /// Gets the data for the specified overlay.
        /// </summary>
        /// <param name="filename">The name of the file containing the overlay data.</param>param>
        /// <returns>An <see cref="ILabelledPointsOverlayData"/> that contains the overlay data.</returns>
        public ILabelledPointsOverlayData GetMultiplePointsOverlayData(string filename)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the data for the specified overlay.
        /// </summary>
        /// <returns>An <see cref="ILabelledPoint"/> that contains the data for the sun.</returns>
        public ILabelledPoint GetPointOverlayData()
        {
            if (sun == null)
            {
                var point = new LabelledPoint(Resources.Sun);

                point.SetCoordinate(StellarParameters.AbsoluteMagnitude, 4.83);
                point.SetCoordinate(StellarParameters.Luminosity, 1); // TODO : This is L/Lsun
                point.SetCoordinate(StellarParameters.EffectiveTemperature, 5778);
                point.SetCoordinate(StellarParameters.ColourIndex("B", "V"), 0.626);
                point.SetCoordinate(StellarParameters.SpectralClass, SpectralClasses.ToDouble("G5"));

                sun = point;
            }
            
            return sun;
        }

        /// <summary>
        /// Loads the data required to generate the named stars overlay from the data source.
        /// </summary>
        private void LoadNamedStars()
        {
            var definition = provider.CreateImportDefinitionBuilder(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(2, "ColourIndex(B-V)", DataTypes.Decimal)
                .Build();

            provider.ImportData("", definition);
        }
    }
}
