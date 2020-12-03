using InventaryApp.Server.Entities;
using InventaryApp.Server.Services;
using InventaryApp.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InventaryApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

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
            var addProduct = await _productService.AddProductAsync(model.Code, model.Name, model.Description, model.Brand, model.Category, model.Cost, model.Price, userId);

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
    }
}
