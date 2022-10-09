using System.ComponentModel.DataAnnotations;

namespace To_doListApiApp.Models
{
    public class Workspace
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public IEnumerable<UserWorkspaceRole>? UserWorkspaceRoles { get; set; }
        public IEnumerable<Item>? Items { get; set; }
    }
}
