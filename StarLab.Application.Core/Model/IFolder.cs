namespace StarLab.Application.Model
{
    public interface IFolder
    {
        bool Expanded { get; }

        IEnumerable<IFolder> Folders { get; }

        string Name { get; set; }

        string Path { get; }

        void AddFolder(IFolder folder);
    }
}
