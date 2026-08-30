using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Tables;

namespace StarLab.Application
{
    /// <summary>
    /// Defines mappings used by AutoMapper to copy application model objects to their respective data transfer objects.
    /// </summary>
    public class ApplicationProfile : Profile
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationProfile"/> class.
        /// </summary>
        public ApplicationProfile()
        {
            CreateMap<Axis, AxisDTO>();
            CreateMap<Chart, ChartDTO>();
            CreateMap<Database, DatabaseDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Folder, FolderDTO>();
            CreateMap<Font, FontDTO>();
            CreateMap<Grid, GridDTO>();
            CreateMap<GridLines, GridLinesDTO>();
            CreateMap<Label, LabelDTO>();
            CreateMap<PlotArea, PlotAreaDTO>();
            CreateMap<Project, ProjectDTO>().ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.AllDocuments))
                                            .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => src.AllFolders));

            CreateMap<Scale, ScaleDTO>();
            CreateMap<Table,  TableDTO>  ();
            CreateMap<TickLabels, TickLabelsDTO>();
            CreateMap<TickMarks, TickMarksDTO>();
            CreateMap<Workspace.Workspace, WorkspaceDTO>();
        }
    }
}
