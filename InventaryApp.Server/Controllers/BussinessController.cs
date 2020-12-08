using InventaryApp.Server.Entities;
using InventaryApp.Server.Services;
using InventaryApp.Shared;
using InventaryApp.Shared.Bussiness;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
    [Authorize]
    public class BussinessController : ControllerBase
    {
        private readonly IBussinessService _bussinessService;
        private const int PAGE_SIZE = 10;

        public BussinessController(IBussinessService bussinessService)
        {
            _bussinessService = bussinessService;
        }


        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Bussiness>))]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bussinesses = await _bussinessService.GetAllBussinesAsync(userId);

            return Ok(new CollectionResponse<Bussiness>
            {
                IsSuccess = true,
                Message = "Bussiness received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = bussinesses
            });
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Bussiness>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Bussiness>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var bussiness = await _bussinessService.GetBussinessById(id, userId);
            if (bussiness == null)
                return BadRequest(new OperationResponse<string>
                {
                    IsSuccess = false,
                    Message = "Invalid operation",
                });

            return Ok(new OperationResponse<Bussiness>
            {
                Record = bussiness,
                Message = "Bussiness retrieved successfully!",
                IsSuccess = true,
                OperationDate = DateTime.UtcNow
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Category>))]
        [HttpGet]
        public IActionResult GetAll(int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalBussiness = 0;
            if (page == 0)
                page = 1;
            var bussinesses = _bussinessService.GetAllBussinessCollectionAsync(PAGE_SIZE, page, userId, out totalBussiness);

            int totalPages = 0;
            if (totalBussiness % PAGE_SIZE == 0)
                totalPages = totalBussiness / PAGE_SIZE;
            else
                totalPages = (totalBussiness / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Bussiness>
            {
                Count = totalBussiness,
                IsSuccess = true,
                Message = "Bussiness received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = bussinesses
            });
        }
       
        [ProducesResponseType(200, Type = typeof(Bussiness))]
        [ProducesResponseType(400, Type = typeof(Bussiness))]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromForm] BussinessViewModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var addBussiness = await _bussinessService.AddBussinessAsync(model.Code,model.Name,model.Address,model.PhoneNumber,model.Email,model.Owner,model.OwnerPhone, userId);

            if (addBussiness != null)
            {
                return Ok(new OperationResponse<Bussiness>
                {
                    IsSuccess = true,
                    Message = $"{addBussiness.Name} has been added successfully!",
                    Record = addBussiness
                });

            }
            return BadRequest(new OperationResponse<Bussiness>
            {
                Message = "Something went wrong",
                IsSuccess = true
            });

        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Bussiness>))]
        [HttpGet("query={query}/page={page}")]
        public IActionResult Get(string query, int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalBussiness = 0;
            if (page == 0)
                page = 1;
            var bussinesses = _bussinessService.SearchBussinessAsync(query, PAGE_SIZE, page, userId, out totalBussiness);

            int totalPages = 0;
            if (totalBussiness % PAGE_SIZE == 0)
                totalPages = totalBussiness / PAGE_SIZE;
            else
                totalPages = (totalBussiness / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Bussiness>
            {
                Count = totalBussiness,
                IsSuccess = true,
                Message = $"Bussiness of '{query}' received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = bussinesses
            });
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Bussiness>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Bussiness>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] BussinessViewModel model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var editedBussiness = await _bussinessService.EditBussinessAsync(model.Id, model.Code, model.Name, model.Address, model.PhoneNumber, model.Email, model.Owner, model.OwnerPhone, userId);

            if (editedBussiness != null)
            {
                return Ok(new OperationResponse<Bussiness>
                {
                    IsSuccess = true,
                    Message = $"{editedBussiness.Name} has been edited successfully!",
                    Record = editedBussiness
                });
            }


            return BadRequest(new OperationResponse<Category>
            {
                Message = "Something went wrong",
                IsSuccess = false
            });

        }
        [ProducesResponseType(200, Type = typeof(OperationResponse<Bussiness>))]
        [ProducesResponseType(404)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var getOld = await _bussinessService.DeleteBussinessAsync(id, userId);
            if (getOld == null)
                return NotFound();

            var deletedBussiness = await _bussinessService.DeleteBussinessAsync(id, userId);

            return Ok(new OperationResponse<Bussiness>
            {
                IsSuccess = true,
                Message = $"{getOld.Name} has been deleted successfully!",
                Record = deletedBussiness
            });
        }

    }
}
