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
    using NBUCareers.Services.Models.Applications;

    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public ApplicationService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ApplicationResponseModel> Create(ApplicationRequestModel model)
        {
            var application = this.mapper.Map<Application>(model);

            await this.db.Applications.AddAsync(application);
            await this.db.SaveChangesAsync();

            return new ApplicationResponseModel { Id = application.Id };
        }

        public async Task<ApplicationRequestModel> Find(int id)
            => await this.db.Applications
                .ProjectTo<ApplicationRequestModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<List<ApplicationRequestModel>> GetAll()
            => await this.db.Applications
                .ProjectTo<ApplicationRequestModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
