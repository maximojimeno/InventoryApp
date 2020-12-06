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

        public async Task<ProductSingleResponse> GetProductByIdAsync(string id)
        {
            var response = await client.GetProtectedAsync<ProductSingleResponse>($"{_baseUrl}/api/product/{id}");
            return response.Result;
        }

        public async Task<ProductSingleResponse> ProductPostAsync(ProductViewModel model)
        {

            var response = await client.SendFormProtectedAsync<ProductSingleResponse>($"{_baseUrl}/api/product", ActionType.POST,
                new StringFormKeyValue("Code", model.Code),
                new StringFormKeyValue("Name", model.Name),
                new StringFormKeyValue("Description", model.Description),
                new StringFormKeyValue("BrandId", model.BrandId),
                new StringFormKeyValue("CategoryId", model.CategoryId),
                new StringFormKeyValue("Cost", model.Cost.ToString()),
                new StringFormKeyValue("Price", model.Price.ToString())
                );
            return response.Result;
        }

        public async Task<ProductSingleResponse> EditProductAsync(ProductViewModel model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("Code", model.Code),
                new StringFormKeyValue("Name", model.Name),
                new StringFormKeyValue("Description", model.Description),
                new StringFormKeyValue("BrandId", model.BrandId),
                new StringFormKeyValue("CategoryId", model.CategoryId),
                new StringFormKeyValue("Cost", model.Cost.ToString()),
                new StringFormKeyValue("Price", model.Price.ToString())
            };

            var response = await client.SendFormProtectedAsync<ProductSingleResponse>($"{_baseUrl}/api/product", ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }

        public async Task<ProductSingleResponse> DeletePlanAsync(string id)
        {
            var response = await client.DeleteProtectedAsync<ProductSingleResponse>($"{_baseUrl}/api/product/{id}");
            return response.Result;
        }

        public async Task<ProductCollectionPagingResponse> GetAllProductByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<ProductCollectionPagingResponse>($"{_baseUrl}/api/product?page={page}");
            return response.Result;
        }

        public async Task<ProductCollectionPagingResponse> SearchProductByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<ProductCollectionPagingResponse>($"{_baseUrl}/api/product/query={query}&page={page}");
            return response.Result;
        }
   


    }
}
