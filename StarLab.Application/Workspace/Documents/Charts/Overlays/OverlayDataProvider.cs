using StarLab.Application.Data.Import;
using StarLab.Domain.Data;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Provides access to the data used to generate the chart overlays.
    /// </summary>
    public class OverlayDataProvider : IOverlayDataProvider
    {
        private readonly Dictionary<string, IOverlayData> dataByOverlay = new Dictionary<string, IOverlayData>(); //

        private readonly IFileImportProvider provider; //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public OverlayDataProvider(IFileImportProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));

            //var sunSymbol = "\u2A00";

            
            
        }




        /// <summary>
        /// Gets the data for the magnitude classes overlay.
        /// </summary>
        /// <returns>An <see cref="IMagnitudeClassesOverlayData"/> that contains the overlay data.</returns>
        public IMagnitudeClassesOverlayData GetMagnitudeClassesOverlayData()
        {
            return new MagnitudeClassesOverlayData();
        }

        /// <summary>
        /// Gets the data for the named stars overlay.
        /// </summary>
        /// <returns>An <see cref="ILabelledPointsOverlayData"/> that contains the overlay data.</returns>
        public ILabelledPointsOverlayData GetNamedStarsOverlayData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ILabelledPointsOverlayData GetSunOverlayData()
        {
            if (!dataByOverlay.ContainsKey(OverlayNames.Sun))
            {
                var data = new LabelledPointsOverlayData();

                data.AddLabel(Resources.Sun);
                data.AddValue(StellarParameters.AbsoluteMagnitude, 4.83);
                data.AddValue(StellarParameters.Luminosity, 1); // TODO : This is L/Lsun
                data.AddValue(StellarParameters.EffectiveTemperature, 5778);
                data.AddValue(StellarParameters.ColourIndex("B", "V"), 0.626);
                data.AddValue(StellarParameters.SpectralClass, SpectralClasses.ToDouble("G5"));

                dataByOverlay.Add(OverlayNames.Sun, data);
            }

            return (ILabelledPointsOverlayData)dataByOverlay[OverlayNames.Sun];
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
