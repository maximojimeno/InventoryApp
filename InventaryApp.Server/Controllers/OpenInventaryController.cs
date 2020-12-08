using InventaryApp.Server.Entities;
using InventaryApp.Server.Services;
using InventaryApp.Shared;
using InventaryApp.Shared.OpenInventary;
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
    public class OpenInventaryController : ControllerBase
    {
        private readonly IOpenInventaryService _OpenInventoryService;
        private const int PAGE_SIZE = 10;

        public OpenInventaryController(IOpenInventaryService openInventaryService)
        {
            _OpenInventoryService = openInventaryService;
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<OpenInventary>))]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var openInventaries = await _OpenInventoryService.GetAllOpenInventaryAsync(userId);

            return Ok(new CollectionResponse<OpenInventary>
            {
                IsSuccess = true,
                Message = "Open Inventary received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = openInventaries
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<OpenInventary>))]
        [HttpGet]
        [Route("GetAll")]
        public IActionResult Get(int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalOpenInventary = 0;
            if (page == 0)
                page = 1;
            var openInventaries = _OpenInventoryService.GetAllOpenInventaryCollectionAsync(PAGE_SIZE, page, userId, out totalOpenInventary);

            int totalPages = 0;
            if (totalOpenInventary % PAGE_SIZE == 0)
                totalPages = totalOpenInventary / PAGE_SIZE;
            else
                totalPages = (totalOpenInventary / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<OpenInventary>
            {
                Count = totalOpenInventary,
                IsSuccess = true,
                Message = "Open Account received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = openInventaries
            });
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(OpenInventary))]
        [ProducesResponseType(400, Type = typeof(OpenInventary))]
        public async Task<IActionResult> PostAsync([FromForm] OpenInventaryViewModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var addOpenInventary = await _OpenInventoryService.AddOpenInventaryAsync(model.OpenDate ,model.CloseDate ,model.BussinessId, model.StatusInventary,model.ActualAmountInventary,model.ActualAmountInventary, userId);

            if (addOpenInventary != null)
            {
                return Ok(new OperationResponse<OpenInventary>
                {
                    IsSuccess = true,
                    Message = $"Open Inventary for {addOpenInventary.Bussiness.Name} has been added successfully!",
                    Record = addOpenInventary

                });

            }
            return BadRequest(new OperationResponse<Account>
            {
                Message = "Something went wrong",
                IsSuccess = true
            });
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<OpenInventary>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<OpenInventary>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] OpenInventaryViewModel model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var editedOpenInventary = await _OpenInventoryService.EditOpenInventaryAsync(model.Id, model.OpenDate, model.CloseDate, model.BussinessId, model.StatusInventary, model.ActualAmountInventary, model.ActualAmountInventary, userId);

            if (editedOpenInventary != null)
            {
                return Ok(new OperationResponse<OpenInventary>
                {
                    IsSuccess = true,
                    Message = $" Open Inventary for {editedOpenInventary.Bussiness.Name} has been edited successfully!",
                    Record = editedOpenInventary
                });
            }


            return BadRequest(new OperationResponse<OpenInventary>
            {
                Message = "Something went wrong",
                IsSuccess = false
            });

        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<OpenInventary>))]
        [HttpGet("query={query}/page={page}")]
        public IActionResult Get(string query, int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalOpenInventary = 0;
            if (page == 0)
                page = 1;
            var openInventaries = _OpenInventoryService.SearchOpenInventaryAsync(query, PAGE_SIZE, page, userId, out totalOpenInventary);

            int totalPages = 0;
            if (totalOpenInventary % PAGE_SIZE == 0)
                totalPages = totalOpenInventary / PAGE_SIZE;
            else
                totalPages = (totalOpenInventary / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<OpenInventary>
            {
                Count = totalOpenInventary,
                IsSuccess = true,
                Message = $"Open Inventary of '{query}' received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = openInventaries
            });
        }

    }
}
