﻿using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public class ApplicationProfile : Profile
    {
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
