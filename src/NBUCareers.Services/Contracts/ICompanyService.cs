namespace NBUCareers.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NBUCareers.Services.Models.Companies;

    public interface ICompanyService
    {
        Task<CompanyResponseModel> Create(CompanyRequestModel model);

        Task<bool> Delete(int id);

        Task<bool> Edit(CompanyRequestModel model);

        Task<List<CompanyRequestModel>> GetAll();

        Task<CompanyRequestModel> Find(int id);
    }
}
