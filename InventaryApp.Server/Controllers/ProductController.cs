using InventaryApp.Server.Entities;
using InventaryApp.Server.Services;
using InventaryApp.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        private const int PAGE_SIZE = 10;


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
        [HttpGet]
        public IActionResult Get(int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalProducts = 0;
            if (page == 0)
                page = 1;
            var products = _productService.GetAllProductAsync(PAGE_SIZE, page, userId, out totalProducts);

            int totalPages = 0;
            if (totalProducts % PAGE_SIZE == 0)
                totalPages = totalProducts / PAGE_SIZE;
            else
                totalPages = (totalProducts / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Product>
            {
                Count = totalProducts,
                IsSuccess = true,
                Message = "Plans received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = products
            });
        }

    }
}
