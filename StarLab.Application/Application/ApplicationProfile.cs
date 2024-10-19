using AutoMapper;
using StarLab.Application.DataTransfer;
using StarLab.Application.Model;

namespace StarLab.Application
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Model.Workspace, WorkspaceDTO>();
            CreateMap<Content, ContentDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Folder, FolderDTO>();
            //CreateMap<Chart, ChartDTO>();
            //CreateMap<Title, TitleDTO>();
            //CreateMap<Font, FontDTO>();
            //CreateMap<Axis, AxisDTO>();
            //CreateMap<Grid, GridDTO>();
        }
    }
}
