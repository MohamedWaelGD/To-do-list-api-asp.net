using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using To_doListApiApp.Dtos.ItemDto;
using To_doListApiApp.Models;
using To_doListApiApp.Services.ItemServices;

namespace To_doListApiApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            this._itemService = itemService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseAPI<IEnumerable<ItemGetDto>>>> CreateItems(ItemCreateDto itemCreateDto)
        {
            var response = await _itemService.CreateItem(itemCreateDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("{workspaceId}")]
        public async Task<ActionResult<ResponseAPI<IEnumerable<ItemGetDto>>>> GetItems(int workspaceId)
        {
            var response = await _itemService.GetItems(workspaceId);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<ResponseAPI<ItemGetDto>>> EditItem(ItemEditDto itemEditDto)
        {
            var response = await _itemService.EditItem(itemEditDto);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{itemId}")]
        public async Task<ActionResult<ResponseAPI<IEnumerable<ItemGetDto>>>> DeleteItem(int itemId)
        {
            var response = await _itemService.DeleteItem(itemId);

            if (!response.isSuccess)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
