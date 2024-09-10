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
            CreateMap<IFolder, FolderDTO>();
            CreateMap<IWorkspace, WorkspaceDTO>();
        }
    }
}
