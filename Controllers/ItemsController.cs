using Application.Contratcs.Services;
using Domain.DTO;
using FullCartAPI.BaseClasses;
using FullCartAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ExceptionHandler
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
                _itemService = itemService; 
        }

        [HttpGet]
        [Route(APIConstants.Item.GetItems)]

        public async Task<IActionResult> GetItemsAsync()
        {
            return await HandleRequestAsync(async () =>
            {
                var res = await _itemService.GetItemsAsync();
                return Ok(res);
            });
        }
    }
}
