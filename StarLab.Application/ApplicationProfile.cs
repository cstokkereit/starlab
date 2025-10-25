using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;

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
            CreateMap<Document, DocumentDTO>();
            CreateMap<Folder, FolderDTO>();
            CreateMap<Font, FontDTO>();
            CreateMap<Label, LabelDTO>();
            CreateMap<Project, ProjectDTO>().ForMember(dest => dest.Documents, opt => opt.MapFrom(src => src.AllDocuments))
                                            .ForMember(dest => dest.Folders, opt => opt.MapFrom(src => src.AllFolders));

            CreateMap<Scale, ScaleDTO>();
            CreateMap<TickLabelsDTO, TickLabelsDTO>();
            CreateMap<TickMarksDTO, TickMarksDTO>();
            CreateMap<Workspace.Workspace, WorkspaceDTO>();
        }
    }
}
