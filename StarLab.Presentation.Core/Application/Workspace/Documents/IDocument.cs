namespace StarLab.Application.Workspace.Documents
{
    public interface IDocument
    {
        public event EventHandler<string>? NameChanged;

        string FullName { get; }

        string ID { get; }

        string Name { get; set; }

        string Path { get; }

        string View { get; }
    }
}
