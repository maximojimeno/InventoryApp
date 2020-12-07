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

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categotyService)
        {
            _categoryService = categotyService;
        }


        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Category>))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var categories = await _categoryService.GetAllCategotyAsync(userId);

            return Ok(new CollectionResponse<Category>
            {
                
                IsSuccess = true,
                Message = "Category received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = categories
            });
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400, Type = typeof(Category))]
        public async Task<IActionResult> PostAsync([FromForm] CategoryViewModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var addCategory = await _categoryService.AddCategoryAsync(model.Name, userId);

            if (addCategory != null)
            {
                return Ok(new OperationResponse<Category>
                {
                    IsSuccess = true,
                    Message = $"{addCategory.Name} has been added successfully!",
                    Record = addCategory

                });

            }
            return BadRequest(new OperationResponse<Category>
            {
                Message = "Something went wrong",
                IsSuccess = true
            });

        }


    }
}
