using AKSoftware.WebApi.Client;
using InventaryApp.Shared.OpenInventary;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventaryApp.Shared.Services
{
    class OpenInventaryServices 
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public OpenInventaryServices(string url)
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

        public async Task<OpenInventaryCollectionResponse> GetAllOpenInventaryAsync()
        {
            var response = await client.GetProtectedAsync<OpenInventaryCollectionResponse>($"{_baseUrl}/api/openinventary/");
            return response.Result;
        }

        public async Task<OpenInventarySingleResponse> OpenInventaryPostAsync(OpenInventaryViewModel model)
        {

            var response = await client.SendFormProtectedAsync<OpenInventarySingleResponse>($"{_baseUrl}/api/openinventary", ActionType.POST,
                new StringFormKeyValue("OpenDate", model.OpenDate.ToString()),
                new StringFormKeyValue("CloseDate", model.CloseDate.ToString()),
                new StringFormKeyValue("BussinessId", model.BussinessId),
                new StringFormKeyValue("StatusInventary", model.StatusInventary.ToString()),
                new StringFormKeyValue("OldAmountInventary", model.OldAmountInventary.ToString()),
                new StringFormKeyValue("ActualAmountInventary",model.ActualAmountInventary.ToString())
                );
            return response.Result;
        }

        public async Task<OpenInventarySingleResponse> EditAccountAsync(OpenInventaryViewModel model)
        {
            var formKeyValues = new List<FormKeyValue>()
            {
                new StringFormKeyValue("Id", model.Id),
                new StringFormKeyValue("OpenDate", model.OpenDate.ToString()),
                new StringFormKeyValue("CloseDate", model.CloseDate.ToString()),
                new StringFormKeyValue("BussinessId", model.BussinessId),
                new StringFormKeyValue("StatusInventary", model.StatusInventary.ToString()),
                new StringFormKeyValue("OldAmountInventary", model.OldAmountInventary.ToString()),
                new StringFormKeyValue("ActualAmountInventary",model.ActualAmountInventary.ToString())
            };

            var response = await client.SendFormProtectedAsync<OpenInventarySingleResponse>($"{_baseUrl}/api/openinventary", ActionType.PUT, formKeyValues.ToArray());

            return response.Result;
        }

        public async Task<OpenInventaryCollectionPagingResponse> GetAllAccountByPageAsync(int page = 1)
        {
            var response = await client.GetProtectedAsync<OpenInventaryCollectionPagingResponse>($"{_baseUrl}/api/openinventary/GetAll?page={page}");
            return response.Result;
        }
        public async Task<OpenInventaryCollectionPagingResponse> SearchAccountByPageAsync(string query, int page = 1)
        {
            var response = await client.GetProtectedAsync<OpenInventaryCollectionPagingResponse>($"{_baseUrl}/api/openinventary/query={query}/page={page}");
            return response.Result;
        }
    }
}
