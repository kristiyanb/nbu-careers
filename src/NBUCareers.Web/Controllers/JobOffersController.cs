namespace NBUCareers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Models.JobOffers;

    public class JobOffersController : ApiController
    {
        private readonly IJobOfferService jobOfferService;

        public JobOffersController(IJobOfferService jobOfferService)
        {
            this.jobOfferService = jobOfferService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<JobOfferResponseModel>> Create(JobOfferRequestModel model)
            => await this.jobOfferService.Create(model);

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.jobOfferService.Delete(id);

            if (!result)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Edit(JobOfferRequestModel model)
        {
            var result = await this.jobOfferService.Edit(model);

            if (!result)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<JobOfferRequestModel>>> GetAll()
            => await this.jobOfferService.GetAll();

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<JobOfferRequestModel>> GetById(int id)
        {
            var result = await this.jobOfferService.Find(id);

            if (result == null)
            {
                return this.NotFound(id);
            }

            return this.Ok(result);
        }
    }
}
