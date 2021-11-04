using Microsoft.AspNetCore.Mvc;
using NBUCareers.Services.Contracts;
using NBUCareers.Services.Models.Identity;
using System.Threading.Tasks;

namespace NBUCareers.Web.Controllers
{
    public class IdentityController : ApiController
    {
        private readonly IUserService userService;

        public IdentityController(IUserService userService) 
            => this.userService = userService;

        public async Task<IActionResult> Register(RegisterRequestModel model)
        {
            var result = await this.userService.CreateAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var response = await this.userService.LoginAsync(model);

            if (!response.Succeeded)
            {
                return Unauthorized(response.ErrorMessage);
            }

            return Ok();
        }
    }
}
