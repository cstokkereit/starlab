using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Serialisation.Workspace;
using StarLab.Serialisation.Workspace.Documents;
using StarLab.Serialisation.Workspace.Documents.Charts;

namespace StarLab.Serialisation
{
    /// <summary>
    /// Defines mappings used by AutoMapper to copy POCOs used for XML serialisation/deserialisation to their respective data transfer objects and vice versa.
    /// </summary>
    public class SerialisationProfile : Profile
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SerialisationProfile"> class.
        /// </summary>
        public SerialisationProfile()
        {
            CreateMap<Axis, AxisDTO>().ReverseMap();
            CreateMap<Chart, ChartDTO>().ReverseMap();
            CreateMap<Font, FontDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Folder, FolderDTO>().ReverseMap();
            CreateMap<Label, LabelDTO>().ReverseMap();
            CreateMap<Project, ProjectDTO>().ReverseMap();
            CreateMap<Workspace.Workspace, WorkspaceDTO>().ReverseMap();
        }
    }
}
