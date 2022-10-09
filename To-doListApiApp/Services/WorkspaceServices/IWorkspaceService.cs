using To_doListApiApp.Dtos.WorkspaceDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.WorkspaceServices
{
    public interface IWorkspaceService
    {
        Task<ResponseAPI<IEnumerable<WorkspaceGetDto>>> CreateWorkspace(WorkspaceCreateDto workspaceCreateDto);
        Task<ResponseAPI<IEnumerable<WorkspaceGetDto>>> GetWorkspaces();
        Task<ResponseAPI<WorkspaceGetDto>> EditWorkspace(WorkspaceEditDto workspaceEditDto);
        Task<ResponseAPI<IEnumerable<WorkspaceGetDto>>> DeleteWorkspace(int id);
    }
}
