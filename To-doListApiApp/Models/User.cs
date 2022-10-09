using System.ComponentModel.DataAnnotations;

namespace To_doListApiApp.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordSalt { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public DateOnly Birthdate { get; set; }
        public IEnumerable<UserWorkspaceRole>? UserWorkspaceRoles { get; set; }
    }
}
