using Application.Contratcs.Services;
using Asp.Versioning;
using Domain.DTO;

using FullCartAPI.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
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
        [ApiVersion("1.0")]
        public async Task<IActionResult> GetItemsAsync()
        {
            var res = await _itemService.GetItemsAsync();
            return Ok(res);
            
        }
    }
}
