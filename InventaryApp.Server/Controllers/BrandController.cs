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
    public class BrandController : ControllerBase
    {
        private readonly IBrandService _brandService;
        private const int PAGE_SIZE = 10;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Brand>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Brand>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var brand = await _brandService.GetBrandById(id, userId);
            if (brand== null)
                return BadRequest(new OperationResponse<string>
                {
                    IsSuccess = false,
                    Message = "Invalid operation",
                });

            return Ok(new OperationResponse<Brand>
            {
                Record = brand,
                Message = "Brand retrieved successfully!",
                IsSuccess = true,
                OperationDate = DateTime.UtcNow
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Brand>))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var brands = await _brandService.GetAllBrandAsync(userId);

            return Ok(new CollectionResponse<Brand>
            {

                IsSuccess = true,
                Message = "Brand received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = brands
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Brand>))]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll(int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalBrand = 0;
            if (page == 0)
                page = 1;
            var brand = _brandService.GetAllBrandCollectionAsync(PAGE_SIZE, page, userId, out totalBrand);

            int totalPages = 0;
            if (totalBrand % PAGE_SIZE == 0)
                totalPages = totalBrand / PAGE_SIZE;
            else
                totalPages = (totalBrand / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Brand>
            {
                Count = totalBrand,
                IsSuccess = true,
                Message = "Brand received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = brand
            });
        }

        [ProducesResponseType(200, Type = typeof(Brand))]
        [ProducesResponseType(400, Type = typeof(Brand))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] BrandViewModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var addBrand = await _brandService.AddBrandAsync(model.Name, userId);

            if (addBrand != null)
            {
                return Ok(new OperationResponse<Brand>
                {
                    IsSuccess = true,
                    Message = $"{addBrand.Name} has been added successfully!",
                    Record = addBrand

                });

            }
            return BadRequest(new OperationResponse<Brand>
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
            int totalBrand = 0;
            if (page == 0)
                page = 1;
            var brands = _brandService.SearchBrandAsync(query, PAGE_SIZE, page, userId, out totalBrand);

            int totalPages = 0;
            if (totalBrand % PAGE_SIZE == 0)
                totalPages = totalBrand / PAGE_SIZE;
            else
                totalPages = (totalBrand / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Brand>
            {
                Count = totalBrand,
                IsSuccess = true,
                Message = $"Brand of '{query}' received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = brands
            });
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Brand>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Brand>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] BrandViewModel model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var editedBrand = await _brandService.EditBrandAsync(model.Id, model.Name, userId);

            if (editedBrand != null)
            {
                return Ok(new OperationResponse<Brand>
                {
                    IsSuccess = true,
                    Message = $"{editedBrand.Name} has been edited successfully!",
                    Record = editedBrand
                });
            }


            return BadRequest(new OperationResponse<Brand>
            {
                Message = "Something went wrong",
                IsSuccess = false
            });

        }
        [ProducesResponseType(200, Type = typeof(OperationResponse<Brand>))]
        [ProducesResponseType(404)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var getOld = await _brandService.GetBrandById(id, userId);
            if (getOld == null)
                return NotFound();

            var deletedBrand = await _brandService.DeleteBrandAsync(id, userId);

            return Ok(new OperationResponse<Brand>
            {
                IsSuccess = true,
                Message = $"{getOld.Name} has been deleted successfully!",
                Record = deletedBrand
            });
        }
    }
}
