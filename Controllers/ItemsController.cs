using Application.Contratcs.Services;
using Domain.DTO;

using FullCartAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemsController(IItemService itemService)
        {
                _itemService = itemService; 
        }

        [HttpGet]
        [Route(APIConstants.Item.GetItems)]
        [Authorize]
        public async Task<IActionResult> GetItemsAsync()
        {
            var res = await _itemService.GetItemsAsync();
            return Ok(res);
            
        }
    }
}
