﻿using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Application
{
    public class SerialisationProfile : Profile
    {
        public SerialisationProfile()
        {
            CreateMap<Axis, AxisDTO>().ReverseMap();
            CreateMap<Chart, ChartDTO>().ReverseMap();
            CreateMap<Content, ContentDTO>().ReverseMap();
            CreateMap<Document, DocumentDTO>().ReverseMap();
            CreateMap<Folder, FolderDTO>().ReverseMap();
            CreateMap<Font, FontDTO>().ReverseMap();
            CreateMap<Grid, GridDTO>().ReverseMap();
            CreateMap<Title, TitleDTO>().ReverseMap();
            CreateMap<Workspace.Workspace, WorkspaceDTO>().ReverseMap();
        }
    }
}
