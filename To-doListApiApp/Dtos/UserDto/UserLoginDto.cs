using System.ComponentModel.DataAnnotations;

namespace To_doListApiApp.Dtos.UserDto
{
    public class UserLoginDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
