using AutoMapper;
using log4net;
using StarLab.Application.Data;
using StarLab.Domain;
using StarLab.Shared;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that .
    /// </summary>
    internal class UpdateChartInteractor : UseCaseInteractor<IChartOutputPort>, IUseCaseAsync<UpdateChartUseCaseArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(UpdateChartInteractor)); // The logger that will be used for writing log messages.

        private readonly IDatabaseManager databases; // TODO

        private readonly IQueryBuilder builder; // TODO

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplyChartSettingsInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="databases">An <see cref="IDatabaseManager"/> that will be used to access the data.</param>
        /// <param name="builder">An <see cref="IQueryBuilder"/> that will be used to build database queries.</param>
        public UpdateChartInteractor(IChartOutputPort outputPort, IMapper mapper, IDatabaseManager databases, IQueryBuilder builder)
            : base(outputPort, mapper)
        {
            this.databases = databases ?? throw new ArgumentNullException(nameof(databases));
            this.builder = builder ?? throw new ArgumentNullException(nameof(builder));
        }

        /// <summary>
        /// Executes the use case asynchronously.
        /// </summary>
        /// <param name="args">The <see cref="UpdateChartUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous operation.</returns>
        public async Task ExecuteAsync(UpdateChartUseCaseArgs args)
        {
            if (log.IsDebugEnabled) StartStopWatch();

            databases.OpenConnection(args.Host, args.Port);

            var database = databases.GetDatabase(args.DatabaseName);

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
                                ColourIndex = star.ColourIndex(ColourIndexTypes.BV)
                            });
                        }

                        rows++;
                    }
                }
            });

            if (log.IsDebugEnabled) log.Debug(LogEntries.QueryPerformance(query.ToString(), rows, GetElapsedTime()));

            OutputPort.SetData(dto);
        }
    }
}
