using System.ComponentModel.DataAnnotations;

namespace To_doListApiApp.Dtos.WorkspaceDto
{
    public class WorkspaceCreateDto
    {
        [Required]
        public string Name { get; set; }
    }
}
