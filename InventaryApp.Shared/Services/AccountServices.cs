using AKSoftware.WebApi.Client;
using InventaryApp.Shared.Account;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventaryApp.Shared.Services
{
    public class AccountServices
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public AccountServices(string url)
        {
            _baseUrl = url;
        }

        public string AccessToken
        {
            get => client.AccessToken;
            set
            {
                client.AccessToken = value;
            }
        }

        public async Task<AccountSingleResponse> GetAccountByIdAsync(string id)
        {
            var response = await client.GetProtectedAsync<AccountSingleResponse>($"{_baseUrl}/api/account/{id}");
            return response.Result;
        }

        public async Task<AccountSingleResponse> AccountPostAsync(AccountViewModel model)
        {

            var response = await client.SendFormProtectedAsync<AccountSingleResponse>($"{_baseUrl}/api/account", ActionType.POST,
                new StringFormKeyValue("Code", model.Code),
                new StringFormKeyValue("Name", model.Name),
                new StringFormKeyValue("Name", model.Type),
                new StringFormKeyValue("Name", model.BussinessId)
                );
            return response.Result;
        }

        public async Task<AccountSingleResponse> EditAccountAsync(AccountViewModel model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("Code", model.Code),
                new StringFormKeyValue("Name", model.Name),
                new StringFormKeyValue("Name", model.Type),
                new StringFormKeyValue("Name", model.BussinessId)
            };

            var response = await client.SendFormProtectedAsync<AccountSingleResponse>($"{_baseUrl}/api/account", ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }

        public async Task<AccountSingleResponse> DeleteAccountAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<AccountSingleResponse>($"{_baseUrl}/api/account/{id}");
            return response.Result;
        }

        public async Task<AccountCollectionPagingResponse> GetAllAccountByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<AccountCollectionPagingResponse>($"{_baseUrl}/api/product?page={page}");
            return response.Result;
        }

        public async Task<AccountCollectionPagingResponse> SearchAccountByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<AccountCollectionPagingResponse>($"{_baseUrl}/api/account/query={query}/page={page}");
            return response.Result;
        }
    }
}
