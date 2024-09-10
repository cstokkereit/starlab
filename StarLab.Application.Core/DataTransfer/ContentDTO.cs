namespace StarLab.Application.DataTransfer
{
    public class ContentDTO
    {
        public ContentDTO()
        {
            Contents = new List<ContentDTO>();
        }

        public List<ContentDTO>? Contents;

        public string? Name;

        public int Panel;

        public string? View;
    }
}
