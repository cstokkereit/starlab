using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents.Charts;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("StarLab.Presentation.Tests")]

namespace StarLab.Presentation
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
            CreateMap<IAxis, AxisDTO>();
            CreateMap<IAxisSettings, AxisDTO>();
            CreateMap<IChart, ChartDTO>();

            CreateMap<IChartSettings, ChartDTO>()
                .ForMember(dest => dest.X1, opt => opt.MapFrom(src => src.Axes.X1))
                .ForMember(dest => dest.X2, opt => opt.MapFrom(src => src.Axes.X2))
                .ForMember(dest => dest.Y1, opt => opt.MapFrom(src => src.Axes.Y1))
                .ForMember(dest => dest.Y2, opt => opt.MapFrom(src => src.Axes.Y2));

            CreateMap<IDocument, DocumentDTO>();
            CreateMap<IFolder, FolderDTO>().ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Key));
            CreateMap<IFont, FontDTO>();
            CreateMap<IFont, FontDTO>();
            CreateMap<IGrid, GridDTO>();
            CreateMap<IGridLines, GridLinesDTO>();
            CreateMap<IGridLineSettings, GridLinesDTO>();
            CreateMap<IGridSettings, GridDTO>();
            CreateMap<ILabel, LabelDTO>();
            CreateMap<ILabelSettings, LabelDTO>();
            CreateMap<IPlotArea, PlotAreaDTO>();
            CreateMap<IPlotAreaSettings, PlotAreaDTO>();
            CreateMap<IProject, ProjectDTO>();
            CreateMap<IScale, ScaleDTO>();
            CreateMap<IScaleSettings, ScaleDTO>();
            CreateMap<ITickLabels, TickLabelsDTO>();
            CreateMap<ITickLabelSettings, TickLabelsDTO>();
            CreateMap<ITickMarks, TickMarksDTO>();
            CreateMap<ITickMarkSettings, TickMarksDTO>();

            CreateMap<IWorkspace, WorkspaceDTO>().ForMember(dest => dest.ActiveDocument, opt => opt.MapFrom(src => src.ActiveDocument == null ? string.Empty : src.ActiveDocument.ID));
        }
    }
}
