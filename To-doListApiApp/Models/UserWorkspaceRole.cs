using System.Text.Json.Serialization;

namespace To_doListApiApp.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserWorkspaceRole
    {
        Owner,
        Partner,
        Visitor
    }
}
