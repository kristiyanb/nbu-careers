namespace NBUCareers.Services.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using NBUCareers.Services.Models.JobOffers;

    public interface IJobOfferService
    {
        Task<JobOfferResponseModel> Create(JobOfferRequestModel model);

        Task<bool> Delete(int id);

        Task<bool> Edit(JobOfferRequestModel model);

        Task<List<JobOfferRequestModel>> GetAll();

        Task<JobOfferRequestModel> Find(int id);
    }
}
