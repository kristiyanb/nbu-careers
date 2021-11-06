namespace NBUCareers.Services.Contracts
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using NBUCareers.Services.Models.Identity;

    public interface IIdentityService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequestModel model);

        Task<LoginResponseModel> LoginAsync(LoginRequestModel model);
    }
}
