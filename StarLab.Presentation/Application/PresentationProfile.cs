using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<IDocument, DocumentDTO>();
            CreateMap<IFolder, FolderDTO>().ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Key));
            CreateMap<IProject, ProjectDTO>();
            CreateMap<IWorkspace, WorkspaceDTO>().ForMember(dest => dest.ActiveDocument, opt => opt.MapFrom(src => src.ActiveDocument == null ? string.Empty : src.ActiveDocument.ID));  ;
        }
    }
}
