namespace To_doListApiApp.Models
{
    public class ResponseAPI<T>
    {
        public T? data { get; set; }
        public bool isSuccess { get; set; } = true;
        public string message { get; set; } = string.Empty;
    }
}
