using To_doListApiApp.Dtos.UserDto;
using To_doListApiApp.Dtos.WorkspaceDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Dtos.UserWorkspaceDto
{
    public class UserWorkspaceGetDto
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public UserWorkspaceRole Role { get; set; }
        public UserGetDto? User { get; set; }
        public WorkspaceGetDto? Workspace { get; set; }
    }
}
