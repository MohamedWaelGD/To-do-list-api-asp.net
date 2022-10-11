using To_doListApiApp.Models;

namespace To_doListApiApp.Dtos.UserWorkspaceDto
{
    public class UserWorkspaceCreateDto
    {
        public string UserEmail { get; set; }
        public int WorkspaceId { get; set; }
        public UserWorkspaceRole Role { get; set; }
    }
}
