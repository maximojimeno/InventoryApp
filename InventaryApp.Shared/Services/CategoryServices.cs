using AKSoftware.WebApi.Client;
using InventaryApp.Shared.Category;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace InventaryApp.Shared.Services
{
   public  class CategoryServices
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public CategoryServices(string url)
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

        public async Task<CategoryCollectionResponse> GetAllCategoryAsync()
        {
            var response = await client.GetProtectedAsync<CategoryCollectionResponse>($"{_baseUrl}/api/category/");
            return response.Result;
        }

        public async Task<CategorySingleResponse> CategoryPostAsync(CategoryViewModel model)
        {

            var response = await client.SendFormProtectedAsync<CategorySingleResponse>($"{_baseUrl}/api/category", ActionType.POST,
                new StringFormKeyValue("Name", model.Name)
                );
            return response.Result;
        }

        public async Task<CategorySingleResponse> EditCategoryAsync(CategoryViewModel model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("Name", model.Name) 
            };

            var response = await client.SendFormProtectedAsync<CategorySingleResponse>($"{_baseUrl}/api/category", ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }

        public async Task<CategorySingleResponse> DeleteCategoryAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<CategorySingleResponse>($"{_baseUrl}/api/category/{id}");
            return response.Result;
        }

        public async Task<CategoryCollectionPagingResponse> GetAllCategoryByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<CategoryCollectionPagingResponse>($"{_baseUrl}/api/category/GetAll?page={page}");
            return response.Result;
        }

        public async Task<CategoryCollectionPagingResponse> SearchCategoryByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<CategoryCollectionPagingResponse>($"{_baseUrl}/api/category/query={query}/page={page}");
            return response.Result;
        }

        public async Task<CategorySingleResponse> GetCategoryByIdAsync(string id)
        {
            var response = await client.GetProtectedAsync<CategorySingleResponse>($"{_baseUrl}/api/category/{id}");
            return response.Result;
        }


    }
}
