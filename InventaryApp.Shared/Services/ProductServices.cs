using AKSoftware.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventaryApp.Shared.Services
{
    public class ProductServices
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public ProductServices(string url)
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


        public async Task<ProductSingleResponse> ProductPostAsync(ProductViewModel model)
        {

            var response = await client.SendFormProtectedAsync<ProductSingleResponse>($"{_baseUrl}/api/product", ActionType.POST,
                new StringFormKeyValue("Code", model.Code), new StringFormKeyValue("Name", model.Name), new StringFormKeyValue("Description", model.Description),
                new StringFormKeyValue("Brand", model.Brand), new StringFormKeyValue("Category", model.Category), new StringFormKeyValue("Cost", model.Cost.ToString()),
                new StringFormKeyValue("Price", model.Price.ToString()));

            return response.Result;
        }
    }
}
