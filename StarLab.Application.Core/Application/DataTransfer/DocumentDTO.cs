namespace StarLab.Application.DataTransfer
{
    public class DocumentDTO
    {
        public List<ContentDTO> Contents = new List<ContentDTO>();

        public string? ID;

        public string? Name;

        public string? Path;

        public string? View;
    }
}
