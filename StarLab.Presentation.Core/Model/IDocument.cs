namespace StarLab.Presentation.Model
{
    public interface IDocument
    {
        IContent Content { get; }

        string FullName { get; }

        string Name { get; }

        string Path { get; }

        string Type { get; }

        string View { get; }
    }
}
