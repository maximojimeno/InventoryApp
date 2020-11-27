using AKSoftware.WebApi.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InventaryApp.Shared.Services
{
   public class AuthenticationServices
    {
        private readonly string _baseUrl;

        ServiceClient client = new ServiceClient();
        public AuthenticationServices(string url)
        {
            _baseUrl = url;
        }

        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel request)
        {
            var response = await client.PostAsync<UserManagerResponse>($"{_baseUrl}/api/auth/login", request);

            return response.Result;
        }

        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel request)
        {
            var response = await client.PostAsync<UserManagerResponse>($"{_baseUrl}/api/auth/register", request);

            return response.Result;
        }
    }
}
