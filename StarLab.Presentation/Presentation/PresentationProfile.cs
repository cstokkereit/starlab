using AutoMapper;
using StarLab.Application.DataTransfer;
using StarLab.Presentation.Model;

namespace StarLab.Presentation
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
