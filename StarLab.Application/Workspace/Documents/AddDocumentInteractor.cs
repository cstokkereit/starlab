using AutoMapper;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that adds a document to a folder in the workspace hierarchy.
    /// </summary>
    internal class AddDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<WorkspaceDTO, DocumentDTO>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public AddDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="dtoDocument">A <see cref="DocumentDTO"/> that defines the document being added.</param>
        public void Execute(WorkspaceDTO dtoWorkspace, DocumentDTO dtoDocument)
        {
            ArgumentNullException.ThrowIfNull(dtoWorkspace, nameof(dtoWorkspace));
            ArgumentNullException.ThrowIfNull(dtoDocument, nameof(dtoDocument));

            var workspace = new Workspace(dtoWorkspace);

            if (string.IsNullOrEmpty(dtoDocument.Name))
            {
                dtoDocument.Name = ""; // WorkspaceInteractionHelper.GetDefaultName(dtoDocument.View);
            }

            if (WorkspaceInteractionHelper.IsValid(dtoDocument.Name))
            {
                try
                {
                    var folder = workspace.GetFolder(dtoDocument.Path);
                    var document = CreateDocument(dtoDocument, folder);
                    workspace.AddDocument(document);

                    var dto = Mapper.Map<WorkspaceDTO>(workspace);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));

                    OutputPort.OpenDocument(document.ID);
                }
                catch (NameExistsException e)
                {
                    OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.NameAlreadyExists, e.Target, e.Name), InteractionType.Error, InteractionResponses.OK);
                }
            }
            else
            {
                // TODO if the name is an empty string just create Chart-X or something rather than throw an exception.
                OutputPort.ShowMessage(Resources.StarLab, WorkspaceInteractionHelper.CreateInvalidNameMessage(dtoDocument.Name, Resources.Document), InteractionType.Error, InteractionResponses.OK);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private ChartDTO CreateChart()
        {
            return new ChartDTO { }; // TODO - This should be created from a template that can be configured in the options dialog and/or chart settings. Import/export as XML.
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="folder"></param>
        /// <returns></returns>
        private Document CreateDocument(DocumentDTO dto, IFolder folder)
        {
            // TODO - Names should be specified in a file and made available through cross cutting concerns?? 
            // TODO - Consider having IChartDocumentDefinition and ITableDocumentDefinition both extend IDocumentDefinition - can then switch behaviour using overloading and/or casting

            switch (dto.View)
            {
                case "ColourMagnitudeDiagramView":
                    dto.Chart = CreateChart();
                    break;

            }

            return new Document(dto, folder);
        }
    }
}
