namespace To_doListApiApp.Models
{
    public class UserWorkspace
    {
        public int UserId { get; set; }
        public int WorkspaceId { get; set; }
        public UserWorkspaceRole Role { get; set; }
        public User? User { get; set; }
        public Workspace? Workspace { get; set; }
    }
}
