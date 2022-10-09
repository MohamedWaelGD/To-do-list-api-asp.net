namespace To_doListApiApp.Dtos.ItemDto
{
    public class ItemEditDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
        public int WorkspaceId { get; set; }
    }
}
