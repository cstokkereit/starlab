using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    /// <summary>
    /// Defines mappings used by AutoMapper to copy presentation model objects to their respective data transfer objects.
    /// </summary>
    public class PresentationProfile : Profile
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PresentationProfile"> class.
        /// </summary>
        public PresentationProfile()
        {
            CreateMap<IDocument, DocumentDTO>();
            CreateMap<IFolder, FolderDTO>().ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Key));
            CreateMap<IProject, ProjectDTO>();
            CreateMap<IWorkspace, WorkspaceDTO>().ForMember(dest => dest.ActiveDocument, opt => opt.MapFrom(src => src.ActiveDocument == null ? string.Empty : src.ActiveDocument.ID));
        }
    }
}
