using InventaryApp.Server.Entities;
using InventaryApp.Server.Services;
using InventaryApp.Shared;
using InventaryApp.Shared.Account;
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

    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private const int PAGE_SIZE = 10;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Account>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Account>))]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var account = await _accountService.GetAccountById(id, userId);
            if (account == null)
                return BadRequest(new OperationResponse<string>
                {
                    IsSuccess = false,
                    Message = "Invalid operation",
                });

            return Ok(new OperationResponse<Account>
            {
                Record = account,
                Message = "Account retrieved successfully!",
                IsSuccess = true,
                OperationDate = DateTime.UtcNow
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Account>))]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> Get()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var accounts = await _accountService.GetAllAccountAsync(userId);

            return Ok(new CollectionResponse<Account>
            {
                IsSuccess = true,
                Message = "Accounts received successfully!",
                OperationDate = DateTime.UtcNow,
                Records = accounts
            });
        }

        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Account>))]
        [HttpGet]
        public IActionResult Get(int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalAccounts = 0;
            if (page == 0)
                page = 1;
            var accounts = _accountService.GetAllAccountCollectionAsync(PAGE_SIZE, page, userId, out totalAccounts);

            int totalPages = 0;
            if (totalAccounts % PAGE_SIZE == 0)
                totalPages = totalAccounts / PAGE_SIZE;
            else
                totalPages = (totalAccounts / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Account>
            {
                Count = totalAccounts,
                IsSuccess = true,
                Message = "Accounts received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = accounts
            });
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Account))]
        [ProducesResponseType(400, Type = typeof(Account))]
        public async Task<IActionResult> PostAsync([FromForm] AccountViewModel model)
        {

            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var addAccount = await _accountService.AddAccountAsync(model.Code, model.Name, model.Type,model.BussinessId, userId);

            if (addAccount != null)
            {
                return Ok(new OperationResponse<Account>
                {
                    IsSuccess = true,
                    Message = $"{addAccount.Name} has been added successfully!",
                    Record = addAccount

                });

            }
            return BadRequest(new OperationResponse<Account>
            {
                Message = "Something went wrong",
                IsSuccess = true
            });
        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Account>))]
        [ProducesResponseType(400, Type = typeof(OperationResponse<Account>))]
        [HttpPut]
        public async Task<IActionResult> Put([FromForm] AccountViewModel model)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;


            var editedAccount = await _accountService.EditAccountAsync(model.Id, model.Code, model.Name, model.Type, model.BussinessId, userId);

            if (editedAccount != null)
            {
                return Ok(new OperationResponse<Account>
                {
                    IsSuccess = true,
                    Message = $"{editedAccount.Name} has been edited successfully!",
                    Record = editedAccount
                });
            }


            return BadRequest(new OperationResponse<Account>
            {
                Message = "Something went wrong",
                IsSuccess = false
            });

        }

        [ProducesResponseType(200, Type = typeof(OperationResponse<Account>))]
        [ProducesResponseType(404)]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(string id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var getOld = await _accountService.GetAccountById(id, userId);
            if (getOld == null)
                return NotFound();

            var deletedAccount = await _accountService.DeleteAccountAsync(id, userId);

            return Ok(new OperationResponse<Account>
            {
                IsSuccess = true,
                Message = $"{getOld.Name} has been deleted successfully!",
                Record = deletedAccount
            });


        }
        [ProducesResponseType(200, Type = typeof(CollectionPagingResponse<Account>))]
        [HttpGet("query={query}/page={page}")]
        public IActionResult Get(string query, int page)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            int totalAccounts = 0;
            if (page == 0)
                page = 1;
            var accounts = _accountService.SearchAccountAsync(query, PAGE_SIZE, page, userId, out totalAccounts);

            int totalPages = 0;
            if (totalAccounts % PAGE_SIZE == 0)
                totalPages = totalAccounts / PAGE_SIZE;
            else
                totalPages = (totalAccounts / PAGE_SIZE) + 1;

            return Ok(new CollectionPagingResponse<Account>
            {
                Count = totalAccounts,
                IsSuccess = true,
                Message = $"Accounts of '{query}' received successfully!",
                OperationDate = DateTime.UtcNow,
                PageSize = PAGE_SIZE,
                Page = page,
                Records = accounts
            });
        }
    }
}
