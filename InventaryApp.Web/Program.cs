using Blazored.LocalStorage;
using Blazored.Modal;
using InventaryApp.Shared.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace InventaryApp.Web
{
    public class Program
    {
        private const string URL = "http://localhost:57971";
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
           
            builder.Services.AddScoped<AuthenticationServices>(s =>
            {
                return new AuthenticationServices(URL);
            });

            builder.Services.AddScoped<ProductServices>(s =>
            {
                return new ProductServices(URL);
            });

            builder.Services.AddScoped<CategoryServices>(s =>
            {
                return new CategoryServices(URL);
            });


            builder.Services.AddScoped<BrandServices>(s =>
            {
                return new BrandServices(URL);
            });

            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, LocalAuthenticationStateProvider>();
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
