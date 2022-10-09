namespace To_doListApiApp.Dtos.UserDto
{
    public class UserGetDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateOnly Birthdate { get; set; }
    }
}
