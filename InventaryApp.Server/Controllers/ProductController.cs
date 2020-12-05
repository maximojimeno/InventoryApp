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
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        private const int PAGE_SIZE = 2;


        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Product))]
        [ProducesResponseType(400, Type = typeof(Product))]
        public async Task<IActionResult> PostAsync([FromForm] ProductViewModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var addProduct = await _productService.AddProductAsync(model.Code, model.Name, model.Description, model.BrandId, model.CategoryId, model.Cost, model.Price, userId);

            if (addProduct != null)
            {
                return Ok(new OperationResponse<Product> {
                    IsSuccess = true,
                    Message = $"{addProduct.Name} has been added successfully!",
                    Record = addProduct

                });

            }
            return BadRequest(new OperationResponse<Product>
            {
                Message = "Something went wrong",
                IsSuccess = true
            });
        }


        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Product>))]
        [HttpGet("pageSize={pageSize}")]
        public IActionResult Get(int page, int pageSize)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalProducts = 0;
            if (page == 0)
                page = 1;
            var products = _productService.GetAllProductAsync(pageSize, page, userId, out totalProducts);

            int totalPages = 0;
            if (totalProducts % pageSize == 0)
                totalPages = totalProducts / pageSize;
            else
                totalPages = (totalProducts / pageSize) + 1;

            return Ok(new CollectionPagingResponse<Product>
            {
                Count = totalProducts,
                IsSuccess = true,
                Message = "Products received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = pageSize,
                Page = page,
                Records = products
            });
        }


        [ProducesResponseType(200, Type = typeof(OperationResponse<Product>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Product>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] ProductViewModel model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

          
            var editedProduct = await _productService.EditProductAsync(model.Id, model.Code, model.Name, model.Description, model.BrandId, model.CategoryId, model.Cost, model.Price, userId);

            if (editedProduct != null)
            {
                return Ok(new OperationResponse<Product>
                {
                    IsSuccess = true,
                    Message = $"{editedProduct.Name} has been edited successfully!",
                    Record = editedProduct
                });
            }


            return BadRequest(new OperationResponse<Product>
            {
                Message = "Something went wrong",
                IsSuccess = false
            });

        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Product>))]
        [ProducesResponseType(404)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var getOld = await _productService.GetProductById(id, userId);
            if (getOld == null)
                return NotFound();

            var deletedProduct = await _productService.DeleteProductAsync(id, userId);

            return Ok(new OperationResponse<Product>
            {
                IsSuccess = true,
                Message = $"{getOld.Name} has been deleted successfully!",
                Record = deletedProduct
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Product>))]
        [HttpGet("query={query}/pageSize={pageSize}")]
        public IActionResult Get(string query, int page, int pageSize)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalProducts = 0;
            if (page == 0)
                page = 1;
            var products = _productService.SearchProductAsync(query, pageSize, page, userId, out totalProducts);

            int totalPages = 0;
            if (totalProducts % pageSize == 0)
                totalPages = totalProducts / pageSize;
            else
                totalPages = (totalProducts / pageSize) + 1;

            return Ok(new CollectionPagingResponse<Product>
            {
                Count = totalProducts,
                IsSuccess = true,
                Message = $"Product of '{query}' received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = pageSize,
                Page = page,
                Records = products
            });
        }
    }
}
