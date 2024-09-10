namespace StarLab.Application.Model
{
    public interface IWorkspace
    {


        bool Dirty { get; }

        IList<IDocument> Documents { get; }

        string FileName { get; }

        IList<IFolder> Folders { get; }

        string Layout { get; }


    }
}
