using InventaryApp.Server.Models;
using InventaryApp.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace InventaryApp.Server.Services
{
    public interface IUserService
    {
        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);
        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
    }

    public class UserServices : IUserService
    {
        private UserManager<ConfigUser> _userManager;
        private IConfiguration _configuration;
        public UserServices(UserManager<ConfigUser> userManager,IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }


        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Register Model User is null");
            }

            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the passowrd",
                    IsSuccess = false,
                };

            var identityUser = new ConfigUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(identityUser, model.Password);

            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "User Create Succeedfully",
                    IsSuccess = true
                    
                };
            }

            return new UserManagerResponse
            {
                Message = "User Dit not Create",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)

            };

        }
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There Invalid Email or  Password",
                    IsSuccess = false
                };
            }

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "There Invalid Email or  Password",
                    IsSuccess = false
                };
            }

            var claims = new[]
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AuthSettings:Key"]));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature));

            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);

            return new UserManagerResponse
            {
                Message = tokenAsString,
                ExpireDate = token.ValidTo,
                IsSuccess = true
            };
        }
    }
}
