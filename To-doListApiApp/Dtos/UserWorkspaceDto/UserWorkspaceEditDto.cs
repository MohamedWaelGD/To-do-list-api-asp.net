using To_doListApiApp.Models;

namespace To_doListApiApp.Dtos.UserWorkspaceDto
{
    public class UserWorkspaceEditDto
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public UserWorkspaceRole Role { get; set; }
    }
}
