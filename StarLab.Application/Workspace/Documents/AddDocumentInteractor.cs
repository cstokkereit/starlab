using AutoMapper;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Tables;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that adds a document to a folder in the workspace hierarchy.
    /// </summary>
    internal class AddDocumentInteractor : WorkspaceInteractor, IUseCase<AddDocumentUseCaseArgs>
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
        /// <param name="args">The <see cref="AddDocumentUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(AddDocumentUseCaseArgs args)
        {
            var workspace = new Workspace(args.Workspace);

            if (string.IsNullOrEmpty(args.Document.Name))
            {
                args.Document.Name = GetDefaultName(workspace.GetFolder(args.Document.Path), args.Document);
            }

            if (IsValid(args.Document.Name))
            {
                var document = CreateDocument(args.Document, workspace.GetFolder(args.Document.Path));

                workspace.AddDocument(document);

                var dto = Mapper.Map<WorkspaceDTO>(workspace);

                OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));

                OutputPort.OpenDocument(document.ID.ToString());
            }
            else
            {
                throw new InvalidNameException(args.Document.Name);
            }
        }

        /// <summary>
        /// Creates a <see cref="ChartDTO"> that represents a new chart.
        /// </summary>
        /// <returns>The new <see cref="ChartDTO">.</returns>
        private ChartDTO CreateChart()
        {
            return new ChartDTO { }; // TODO - This should be created from a template that can be configured in the options dialog and/or chart settings. Import/export as XML.
        }

        /// <summary>
        /// Creates a <see cref="TableDTO"> that represents a new table.
        /// </summary>
        /// <returns>The new <see cref="TableDTO">.</returns>
        private TableDTO CreateTable()
        {
            return new TableDTO { }; // TODO - This should be created from a template that can be configured in the options dialog and/or table settings. Import/export as XML.
        }

        /// <summary>
        /// Creates a document in the specified folder from the data transfer object provided.
        /// </summary>
        /// <param name="dto">The data transfer object that represents the document.</param>
        /// <param name="folder">The folder that contains the document.</param>
        /// <returns>The new document.</returns>
        private Document CreateDocument(DocumentDTO dto, IFolder folder)
        {
            switch (dto.Type)
            {
                case Constants.Chart:
                    dto.Chart = CreateChart();
                    break;

                case Constants.Table:
                    dto.Table = CreateTable();
                    break;

                default:
                    throw new Exception(ExceptionMessages.UnknownType(dto.Type));
            }

            return new Document(dto, folder);
        }

        /// <summary>
        /// Gets the default name for a new document based on its type.
        /// </summary>
        /// <param name="folder">The <see cref="IFolder"/> that will contain the new document.</param>
        /// <param name="dtoDocument">A <see cref="DocumentDTO"/> that defines the document being added.</param>
        /// <returns>The default name for the new document.</returns>
        private static string GetDefaultName(IFolder folder, DocumentDTO dtoDocument)
        {
            string seed;

            switch (dtoDocument.Type)
            {
                case Constants.Chart:
                    seed = Resources.Chart;
                    break;

                case Constants.Table:
                    seed = Resources.Table;
                    break;

                default:
                    seed = Resources.Document;
                    break;
            }

            var name = seed;
            int index = 2;

            while (folder.ContainsDocument(name))
            {
                name = $"{seed} ({index++})";
            };

            return name;
        }
    }
}
