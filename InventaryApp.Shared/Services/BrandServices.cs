using AKSoftware.WebApi.Client;
using InventaryApp.Shared.Brand;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventaryApp.Shared.Services
{
     public  class BrandServices
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public BrandServices(string url)
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

        public async Task<BrandCollectionResponse> GetAllBrandAsync()
        {
            var response = await client.GetProtectedAsync<BrandCollectionResponse>($"{_baseUrl}/api/brand/");
            return response.Result;
        }

        public async Task<BrandSingleResponse> BrandPostAsync(BrandViewModel model)
        {

            var response = await client.SendFormProtectedAsync<BrandSingleResponse>($"{_baseUrl}/api/brand", ActionType.POST,
                new StringFormKeyValue("Name", model.Name)
                );
            return response.Result;
        }

        public async Task<BrandSingleResponse> EditBrandAsync(BrandViewModel model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("Name", model.Name)
            };

            var response = await client.SendFormProtectedAsync<BrandSingleResponse>($"{_baseUrl}/api/brand", ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }

        public async Task<BrandSingleResponse> DeleteBrandAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<BrandSingleResponse>($"{_baseUrl}/api/brand/{id}");
            return response.Result;
        }

        public async Task<BrandCollectionPagingResponse> GetAllBrandByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<BrandCollectionPagingResponse>($"{_baseUrl}/api/brand/GetAll?page={page}");
            return response.Result;
        }

        public async Task<BrandCollectionPagingResponse> SearchBrandByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<BrandCollectionPagingResponse>($"{_baseUrl}/api/brand/query={query}/page={page}");
            return response.Result;
        }

        public async Task<BrandSingleResponse> GetBrandByIdAsync(string id)
        {
            var response = await client.GetProtectedAsync<BrandSingleResponse>($"{_baseUrl}/api/brand/{id}");
            return response.Result;
        }

    }
}
