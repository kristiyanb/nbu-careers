namespace NBUCareers.Web.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Models.Companies;

    [Authorize]
    public class CompaniesController : ApiController
    {
        private readonly ICompanyService companyService;

        public CompaniesController(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        [HttpPost]
        public async Task<ActionResult<CompanyResponseModel>> Create(CompanyRequestModel model)
            => await this.companyService.Create(model);

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await this.companyService.Delete(id);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Edit(CompanyRequestModel model)
        {
            var result = await this.companyService.Edit(model);

            if (!result)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyRequestModel>>> GetAll()
            => await this.companyService.GetAll();

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<CompanyRequestModel>> GetById(int id)
        {
            var result = await this.companyService.Find(id);

            if (result == null)
            {
                return NotFound(id);
            }

            return Ok(result);
        }
    }
}
