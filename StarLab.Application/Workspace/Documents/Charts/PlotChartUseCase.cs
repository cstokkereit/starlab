using AutoMapper;
using log4net;
using StarLab.Application.Data;
using StarLab.Application.Workspace.Documents.Charts.Overlays;
using StarLab.Shared;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that retrieves the specified data from the database and plots the chart.
    /// </summary>
    internal class PlotChartUseCase : UseCase<IChartOutputPort>, IUseCaseAsync<PlotChartUseCaseArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PlotChartUseCase)); // The logger that will be used for writing log messages.

        private readonly IOverlayDataProvider overlayData; // A data provider that imports and caches the overlay data.

        private readonly IDatabaseManager databases; // Provides access to the database containing the data.

        private readonly IQueryBuilder builder; // A fluent builder for creating database queries.

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotChartUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="databases">An <see cref="IDatabaseManager"/> that will be used to access the data.</param>
        /// <param name="builder">An <see cref="IQueryBuilder"/> that will be used to build database queries.</param>
        public PlotChartUseCase(IChartOutputPort outputPort, IMapper mapper, IDatabaseManager databases, IQueryBuilder builder, IOverlayDataProvider overlayData, IUseCaseFactory factory)
            : base(outputPort, mapper)
        {
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            this.overlayData = overlayData ?? throw new ArgumentNullException(nameof(overlayData));
            this.databases = databases ?? throw new ArgumentNullException(nameof(databases));
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// Executes the use case asynchronously.
        /// </summary>
        /// <param name="args">The <see cref="PlotChartUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync(PlotChartUseCaseArgs args)
        {
            if (log.IsDebugEnabled) StartStopWatch();

            databases.OpenConnection(args.Host, args.Port);

            var database = databases.GetDatabase(args.DatabaseName);

            // TODO : This should not be hard coded.

            var query = builder.AddTable("stars")
                               .AddField(builder.CreateField("ApparentMagnitude"))
                               .AddField(builder.CreateField("Parallax"))
                               .AddField(builder.CreateField("B-V"))
                               .BuildQuery();

            var dto = new List<StarDTO>();

            var rows = 0;

            await Task.Run(() => {

                using (var stars = database.GetStars(query))
                {
                    while (stars.MoveNext())
                    {
                        var star = stars.Current;

                        if (star != null)
                        {
                            dto.Add(new StarDTO
                            {
                                AbsoluteMagnitude = star.ApparentMagnitude + 5 * (Math.Log10(star.Parallax / 1000) + 1),
                                ColourIndex = star.ColourIndex("B-V") 
                            }); 
                        }

                        rows++;
                    }
                }
            });

            if (log.IsDebugEnabled) log.Debug(LogEntries.QueryPerformance(query.ToString(), rows, GetElapsedTime()));

            AddOverlays(args.Chart);

            OutputPort.PlotChart(dto);
        }


        private void AddOverlays(ChartDTO chart)
        {
            foreach (var overlay in chart.Overlays)
            {
                switch (overlay.Type)
                {
                    case OverlayType.Point:
                        AddPointOverlay(chart, overlay.Name);
                        break;



                }
            }
        }

        private void AddPointOverlay(ChartDTO chart, string name)
        {
            var data = overlayData.GetPointOverlayData();

            var point = new LabelledPointDTO
            {
                X = data.GetCoordinate(chart.X1.Data),
                y = data.GetCoordinate(chart.Y1.Data),
                Label = data.Label
            };

            OutputPort.AddOverlay(name, point);
        }
    }
}
