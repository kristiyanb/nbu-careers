namespace NBUCareers.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Models.Identity;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService userService)
            => this.identityService = userService;

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            var result = await this.identityService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                return this.BadRequest(result.Errors);
            }

            return this.Ok();
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var response = await this.identityService.LoginAsync(model);

            if (!response.Succeeded)
            {
                return this.Unauthorized(response.ErrorMessage);
            }

            return this.Ok(response.Token);
        }
    }
}
