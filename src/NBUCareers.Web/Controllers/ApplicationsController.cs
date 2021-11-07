namespace NBUCareers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Models.Applications;

    [Authorize]
    public class ApplicationsController : ApiController
    {
        private readonly IApplicationService applicationService;

        public ApplicationsController(IApplicationService applicationService)
        {
            this.applicationService = applicationService;
        }

        [HttpPost]
        public async Task<ActionResult<ApplicationResponseModel>> Create(ApplicationRequestModel model)
            => await this.applicationService.Create(model);

        [HttpGet]
        public async Task<ActionResult<List<ApplicationRequestModel>>> GetAll()
            => await this.applicationService.GetAll();

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<ApplicationRequestModel>> GetById(int id)
        {
            var result = await this.applicationService.Find(id);

            if (result == null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }
    }
}
