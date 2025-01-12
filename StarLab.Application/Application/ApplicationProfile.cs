using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    /// <summary>
    /// Defines mappings used by AutoMapper to copy application model objects to their respective data transfer objects.
    /// </summary>
    public class ApplicationProfile : Profile
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationProfile"> class.
        /// </summary>
        public ApplicationProfile()
        {
            CreateMap<Workspace.Workspace, WorkspaceDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Folder, FolderDTO>();
            CreateMap<Project, ProjectDTO>();
            //CreateMap<Chart, ChartDTO>();
            //CreateMap<Title, TitleDTO>();
            //CreateMap<Font, FontDTO>();
            //CreateMap<Axis, AxisDTO>();
            //CreateMap<Grid, GridDTO>();
        }
    }
}
