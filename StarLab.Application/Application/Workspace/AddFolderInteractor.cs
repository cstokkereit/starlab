using AutoMapper;

namespace StarLab.Application.Workspace
{
    internal class AddFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>
    {
        public AddFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }



    }
}
