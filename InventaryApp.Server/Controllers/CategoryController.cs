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
        private const int PAGE_SIZE = 10;

        public CategoryController(ICategoryService categotyService)
        {
            _categoryService = categotyService;
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<Category>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Category>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var category = await _categoryService.GetCategoryById(id, userId);
            if (category == null)
                return BadRequest(new OperationResponse<string>
                {
                    IsSuccess = false,
                    Message = "Invalid operation",
                });

            return Ok(new OperationResponse<Category>
            {
                Record = category,
                Message = "Category retrieved successfully!",
                IsSuccess = true,
                OperationDate = DateTime.UtcNow
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Category>))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            
            var categories = await _categoryService.GetAllCategoryAsync(userId);

            return Ok(new CollectionResponse<Category>
            {
                
                IsSuccess = true,
                Message = "Category received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = categories
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Category>))]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalCategory = 0;
            if (page == 0)
                page = 1;
            var category = _categoryService.GetAllCategoryCollectionAsync(PAGE_SIZE, page, userId, out totalCategory);

            int totalPages = 0;
            if (totalCategory % PAGE_SIZE == 0)
                totalPages = totalCategory / PAGE_SIZE;
            else
                totalPages = (totalCategory / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Category>
            {
                Count = totalCategory,
                IsSuccess = true,
                Message = "Category received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = category
            });
        }


        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400, Type = typeof(Category))]
        [HttpPost]
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


        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Category>))]
        [HttpGet("query={query}/page={page}")]
        public IActionResult Get(string query, int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalCategory = 0;
            if (page == 0)
                page = 1;
            var category = _categoryService.SearchCategoryAsync(query, PAGE_SIZE, page, userId, out totalCategory);

            int totalPages = 0;
            if (totalCategory % PAGE_SIZE == 0)
                totalPages = totalCategory / PAGE_SIZE;
            else
                totalPages = (totalCategory / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Category>
            {
                Count = totalCategory,
                IsSuccess = true,
                Message = $"Product of '{query}' received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = category
            });
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Category>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Category>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] CategoryViewModel model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var editedCategory = await _categoryService.EditCategoryAsync(model.Id,model.Name, userId);

            if (editedCategory != null)
            {
                return Ok(new OperationResponse<Category>
                {
                    IsSuccess = true,
                    Message = $"{editedCategory.Name} has been edited successfully!",
                    Record = editedCategory
                });
            }


            return BadRequest(new OperationResponse<Product>
            {
                Message = "Something went wrong",
                IsSuccess = false
            });

        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<Category>))]
        [ProducesResponseType(404)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var getOld = await _categoryService.GetCategoryById(id, userId);
            if (getOld == null)
                return NotFound();

            var deletedCategory = await _categoryService.DeleteCategoryAsync(id, userId);

            return Ok(new OperationResponse<Category>
            {
                IsSuccess = true,
                Message = $"{getOld.Name} has been deleted successfully!",
                Record = deletedCategory
            });
        }

        
    }
}
