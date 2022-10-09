namespace To_doListApiApp.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public bool IsDone { get; set; }
        public Workspace Workspace { get; set; }
    }
}
