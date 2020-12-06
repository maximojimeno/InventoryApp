using InventaryApp.Server.Entities;
using InventaryApp.Server.Services;
using InventaryApp.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;


namespace InventaryApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categotyService;

        public CategoryController(ICategoryService categotyService)
        {
            _categotyService = categotyService;
        }


        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Category>))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var categories = await _categotyService.GetAllCategotyAsync(userId);

            return Ok(new CollectionResponse<Category>
            {
                
                IsSuccess = true,
                Message = "Category received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = categories
            });
        }

    }
}
