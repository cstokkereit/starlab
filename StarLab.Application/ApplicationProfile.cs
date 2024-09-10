using AutoMapper;
using StarLab.Application.Model;

namespace StarLab.Application.DataTransfer
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile() 
        {
            CreateMap<Workspace, WorkspaceDTO>();
            CreateMap<Content, ContentDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Folder, FolderDTO>();
            CreateMap<Chart, ChartDTO>();
            CreateMap<Title, TitleDTO>();
            CreateMap<Font, FontDTO>();
            CreateMap<Axis, AxisDTO>();
            CreateMap<Grid, GridDTO>();
        }
    }
}
