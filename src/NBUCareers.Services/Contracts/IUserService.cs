using Microsoft.AspNetCore.Identity;
using NBUCareers.Services.Models.Identity;
using System.Threading.Tasks;

namespace NBUCareers.Services.Contracts
{
    public interface IUserService
    {
        Task<IdentityResult> CreateAsync(RegisterRequestModel model);

        Task<LoginResponseModel> LoginAsync(LoginRequestModel model);
    }
}
