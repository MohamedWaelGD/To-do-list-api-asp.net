using To_doListApiApp.Dtos.ItemDto;
using To_doListApiApp.Models;

namespace To_doListApiApp.Services.ItemServices
{
    public interface IItemService
    {
        Task<ResponseAPI<IEnumerable<ItemGetDto>>> GetItems(int workspaceId);
        Task<ResponseAPI<IEnumerable<ItemGetDto>>> CreateItem(ItemCreateDto itemCreateDto);
        Task<ResponseAPI<IEnumerable<ItemGetDto>>> DeleteItem(int id);
        Task<ResponseAPI<ItemGetDto>> EditItem(ItemEditDto itemEditDto);
    }
}
