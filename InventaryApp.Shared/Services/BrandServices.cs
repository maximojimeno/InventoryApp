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
    }
}
