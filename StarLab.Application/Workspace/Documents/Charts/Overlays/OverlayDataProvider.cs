using StarLab.Application.Data.Import;

namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Provides access to the data used to generate the chart overlays.
    /// </summary>
    public class OverlayDataProvider : IOverlayDataProvider
    {
        private readonly IFileImportProvider provider;

        private readonly LabelledPointsOverlayData namedStars = new LabelledPointsOverlayData(); //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        public OverlayDataProvider(IFileImportProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));




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
            if (namedStars.Labels.Length == 0)
            {
                LoadNamedStars();
            }

            return namedStars;
        }

        /// <summary>
        /// Loads the data required to generate the named stars overlay from the data source.
        /// </summary>
        private void LoadNamedStars()
        {
            




            // Load the named stars data from a data source (e.g., a file, database, or API).
            // For demonstration purposes, we'll add some sample labels.
            namedStars.AddLabel("Achernar");
            namedStars.AddValue("AbsoluteMagnitude", -2.3);
        }
    }
}
