namespace To_doListApiApp.Dtos.ItemDto
{
    public class ItemCreateDto
    {
        public string Content { get; set; }
        public bool IsDone { get; set; }
        public int WorkspaceId { get; set; }
    }
}
