using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public class PresentationProfile : Profile
    {
        public PresentationProfile()
        {
            CreateMap<IContent, ContentDTO>();
            CreateMap<IDocument, DocumentDTO>();
            CreateMap<IFolder, FolderDTO>().ForMember(dest => dest.Path, a => a.MapFrom(src => src.Key));
            CreateMap<IWorkspace, WorkspaceDTO>();
        }
    }
}
