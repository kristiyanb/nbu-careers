namespace NBUCareers.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NBUCareers.Services.Models.Applications;

    public interface IApplicationService
    {
        Task<ApplicationResponseModel> Create(ApplicationRequestModel model);

        Task<List<ApplicationRequestModel>> GetAll();

        Task<ApplicationRequestModel> Find(int id);
    }
}
