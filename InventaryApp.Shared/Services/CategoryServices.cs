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
    }
}
