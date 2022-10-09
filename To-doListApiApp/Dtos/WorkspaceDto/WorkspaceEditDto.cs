using System.ComponentModel.DataAnnotations;

namespace To_doListApiApp.Dtos.WorkspaceDto
{
    public class WorkspaceEditDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
