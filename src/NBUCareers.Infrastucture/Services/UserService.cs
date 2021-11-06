namespace NBUCareers.Infrastucture.Services
{
    using System.Security.Claims;

    using Microsoft.AspNetCore.Http;

    using NBUCareers.Infrastucture.Extensions;

    public class UserService : IUserService
    {
        private readonly ClaimsPrincipal user;

        public UserService(IHttpContextAccessor httpContextAccessor)
            => this.user = httpContextAccessor.HttpContext?.User;

        public string GetUserName()
            => this.user?.Identity?.Name;

        public string GetId()
            => this.user?.GetId();
    }
}
