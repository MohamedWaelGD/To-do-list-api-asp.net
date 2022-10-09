using AutoMapper;
using Microsoft.EntityFrameworkCore;
using To_doListApiApp.Data;
using To_doListApiApp.Dtos.ItemDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.AuthServices;

namespace To_doListApiApp.Services.ItemServices
{
    public class ItemSerivce : IItemService
    {
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public ItemSerivce(AppDbContext dbContext, IMapper mapper, IAuthService authService)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
            this._authService = authService;
        }

        public async Task<ResponseAPI<IEnumerable<ItemGetDto>>> CreateItem(ItemCreateDto itemCreateDto)
        {
            if (!await _authService.IsHasPermission(itemCreateDto.WorkspaceId))
            {
                var response = new ResponseAPI<IEnumerable<ItemGetDto>>();
                response.isSuccess = false;
                response.message = "User doesn't has permission.";
                return response;
            }

            var item = _mapper.Map<Item>(itemCreateDto);
            await _dbContext.Items.AddAsync(item);
            await _dbContext.SaveChangesAsync();

            return await GetItems(itemCreateDto.WorkspaceId);
        }

        public async Task<ResponseAPI<IEnumerable<ItemGetDto>>> DeleteItem(int id)
        {
            var response = new ResponseAPI<IEnumerable<ItemGetDto>>();
            var item = await _dbContext.Items.FirstOrDefaultAsync(e => e.Id == id);

            if (!await IsItemExists(id))
            {
                response.isSuccess = false;
                response.message = "Item not found.";
                return response;
            }

            if (!await _authService.IsHasPermission(item.Workspace.Id))
            {
                response.isSuccess = false;
                response.message = "User doesn't has permission.";
                return response;
            }

            _dbContext.Items.Remove(item);
            await _dbContext.SaveChangesAsync();

            return await GetItems(id);
        }

        public async Task<ResponseAPI<ItemGetDto>> EditItem(ItemEditDto itemEditDto)
        {
            var response = new ResponseAPI<ItemGetDto>();
            var item = await _dbContext.Items.FirstOrDefaultAsync(e => e.Id == itemEditDto.Id);

            if (!await IsItemExists(itemEditDto.Id))
            {
                response.isSuccess = false;
                response.message = "Item not found.";
                return response;
            }

            if (!await _authService.IsHasPermission(item.Workspace.Id))
            {
                response.isSuccess = false;
                response.message = "User doesn't has permission.";
                return response;
            }

            _mapper.Map(itemEditDto, item);

            await _dbContext.SaveChangesAsync();

            response.data = _mapper.Map<ItemGetDto>(item);
            return response;
        }

        public async Task<ResponseAPI<IEnumerable<ItemGetDto>>> GetItems(int workspaceId)
        {
            var response = new ResponseAPI<IEnumerable<ItemGetDto>>();

            response.data = await _dbContext.Items
                .Include(e => e.Workspace)
                .Where(e => e.Id == _authService.GetUserId() && e.Workspace.Id == workspaceId)
                .Select(e => _mapper.Map<ItemGetDto>(e))
                .ToListAsync();

            return response;
        }
    
        private async Task<bool> IsItemExists(int id)
        {
            return await _dbContext.Items.AnyAsync(e => e.Id == id);
        }
    }
}
