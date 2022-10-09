using To_doListApiApp.Dtos.WorkspaceDto;

namespace To_doListApiApp.Dtos.ItemDto
{
    public class ItemGetDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
        public WorkspaceGetDto Workspace { get; set; }
    }
}
