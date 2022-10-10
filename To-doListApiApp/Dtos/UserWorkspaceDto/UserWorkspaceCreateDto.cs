using To_doListApiApp.Models;

namespace To_doListApiApp.Dtos.UserWorkspaceDto
{
    public class UserWorkspaceCreateDto
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public UserWorkspaceRole Role { get; set; }
    }
}
