using System.ComponentModel.DataAnnotations;

namespace To_doListApiApp.Dtos.UserDto
{
    public class UserRegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        public DateOnly Birthdate { get; set; }
    }
}
