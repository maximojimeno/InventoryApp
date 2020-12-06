using InventaryApp.Server.DataAccess;
using InventaryApp.Server.Models;
using InventaryApp.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.Text;

namespace InventaryApp.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews()
               .AddNewtonsoftJson(options =>
               {
                   options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                   options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
               });

            services.AddRazorPages();
            //Cors
            services.AddCors(Options =>
            {
                Options.AddPolicy("CorsPolicy", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });


            //configure EntityFrameCore with SQL SERVER

            services.AddDbContext<ApplicationDbContext>(options =>
              {
                  options.UseSqlServer(Configuration.GetConnectionString("inventoryappdatabase"));
              }
            );

            services.AddIdentity<ConfigUser, IdentityRole>(Options =>
            {
                Options.Password.RequireDigit = false;
                Options.Password.RequireUppercase =true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireNonAlphanumeric = false;
                Options.Password.RequiredLength = 6;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            services.AddAuthentication(Auth =>
            {
                Auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"])),
                    ValidateIssuerSigningKey = true

                };
            });

            services.AddScoped<IUserService, UserServices>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICategoryService, CategotyService>();
            services.AddTransient<IBrandService, BrandServices>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "InventaryApp.Server", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "InventaryApp.Server v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
