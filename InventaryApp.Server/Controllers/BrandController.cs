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

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
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

    }
}
