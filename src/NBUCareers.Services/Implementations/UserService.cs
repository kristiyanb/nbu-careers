using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NBUCareers.Models;
using NBUCareers.Services.Contracts;
using NBUCareers.Services.Models.Identity;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NBUCareers.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        public UserService(
            UserManager<User> userManager,
            IMapper mapper,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        public async Task<IdentityResult> CreateAsync(RegisterRequestModel model)
            => await this.userManager.CreateAsync(this.mapper.Map<User>(model), model.Password);

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel model)
        {
            var response = new LoginResponseModel();

            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                user = await this.userManager.FindByEmailAsync(model.UserName);

                if (user == null)
                {
                    response.Succeeded = false;
                    response.ErrorMessage = "User name not recognized.";

                    return response;
                }
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                response.Succeeded = false;
                response.ErrorMessage = "Invalid password.";

                return response;
            }

            response.Token = this.GenerateJwtToken(user.Id, user.UserName, this.configuration["AppSecret"]);

            return response;
        }

        private string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
