using To_doListApiApp.Dtos.ItemDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Dtos.WorkspaceDto
{
    public class WorkspaceGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<UserWorkspaceRole>? UserWorkspaceRoles { get; set; }
        public IEnumerable<ItemGetDto>? Items { get; set; }
    }
}
