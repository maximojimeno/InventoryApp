using AKSoftware.WebApi.Client;
using InventaryApp.Shared.Bussiness;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventaryApp.Shared.Services
{
    public class BussinessServices
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public BussinessServices(string url)
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
        public async Task<BussinessSingleResponse> GetBussinessByIdAsync(string id)
        {
            var response = await client.GetProtectedAsync<BussinessSingleResponse>($"{_baseUrl}/api/bussiness/{id}");
            return response.Result;
        }
        public async Task<BussinessSingleResponse> EditBussinessAsync(BussinessViewModel model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("Code", model.Code),
                new StringFormKeyValue("Name", model.Name),
                new StringFormKeyValue("Address", model.Address),
                new StringFormKeyValue("PhoneNumber", model.PhoneNumber),
                new StringFormKeyValue("Email", model.Email),
                new StringFormKeyValue("Owner", model.Owner),
                new StringFormKeyValue("OwnerPhone", model.OwnerPhone)
            };

            var response = await client.SendFormProtectedAsync<BussinessSingleResponse>($"{_baseUrl}/api/bussiness", ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }
        public async Task<BussinessSingleResponse> DeletePlanAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<BussinessSingleResponse>($"{_baseUrl}/api/bussiness/{id}");
            return response.Result;
        }
        public async Task<BussinessSingleResponse> CategoryPostAsync(BussinessViewModel model)
        {

            var response = await client.SendFormProtectedAsync<BussinessSingleResponse>($"{_baseUrl}/api/bussiness", ActionType.POST,
                new StringFormKeyValue("Code", model.Code),
                new StringFormKeyValue("Name", model.Name),
                new StringFormKeyValue("Address", model.Address),
                new StringFormKeyValue("PhoneNumber", model.PhoneNumber),
                new StringFormKeyValue("Email", model.Email),
                new StringFormKeyValue("Owner", model.Owner),
                new StringFormKeyValue("OwnerPhone", model.OwnerPhone)
                );
            return response.Result;
        }
        public async Task<BussinessCollectionPagingResponse> GetAllBussinessByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<BussinessCollectionPagingResponse>($"{_baseUrl}/api/bussiness?page={page}");
            return response.Result;
        }
        public async Task<BussinessCollectionPagingResponse> SearchBussinessByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<BussinessCollectionPagingResponse>($"{_baseUrl}/api/bussiness/query={query}/page={page}");
            return response.Result;
        }
    }
}
