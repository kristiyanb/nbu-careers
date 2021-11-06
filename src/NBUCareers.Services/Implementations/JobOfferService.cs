namespace NBUCareers.Services.Implementations
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Microsoft.EntityFrameworkCore;

    using NBUCareers.Data;
    using NBUCareers.Models;
    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Models.JobOffers;

    public class JobOfferService : IJobOfferService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public JobOfferService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<JobOfferResponseModel> Create(JobOfferRequestModel model)
        {
            var jobOffer = this.mapper.Map<JobOffer>(model);
            
            await this.db.JobOffers.AddAsync(jobOffer);
            await this.db.SaveChangesAsync();

            return new JobOfferResponseModel { Id = jobOffer.Id };
        }

        public async Task<bool> Delete(int id)
        {
            var jobOffer = await this.db.JobOffers.FindAsync(id);

            if (jobOffer == null)
            {
                return false;
            }

            this.db.Remove(jobOffer);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(JobOfferRequestModel model)
        {
            var jobOffer = await this.db.JobOffers.FindAsync(model.Id);

            if (jobOffer == null)
            {
                return false;
            }

            this.mapper.Map(model, jobOffer);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<JobOfferRequestModel> Find(int id)
            => await this.db.JobOffers
                .ProjectTo<JobOfferRequestModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<List<JobOfferRequestModel>> GetAll()
            => await this.db.JobOffers
                .ProjectTo<JobOfferRequestModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
