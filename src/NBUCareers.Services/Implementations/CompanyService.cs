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
    using NBUCareers.Services.Models.Companies;

    public class CompanyService : ICompanyService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;

        public CompanyService(AppDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }


        public async Task<CompanyResponseModel> Create(CompanyRequestModel model)
        {
            var company = this.mapper.Map<Company>(model);

            await this.db.Companies.AddAsync(company);
            await this.db.SaveChangesAsync();

            return new CompanyResponseModel { Id = company.Id };
        }

        public async Task<bool> Delete(int id)
        {
            var company = await this.db.Companies.FindAsync(id);

            if (company == null)
            {
                return false;
            }

            this.db.Remove(company);
            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Edit(CompanyRequestModel model)
        {
            var company = await this.db.Companies.FindAsync(model.Id);

            if (company == null)
            {
                return false;
            }

            this.mapper.Map(model, company);

            await this.db.SaveChangesAsync();

            return true;
        }

        public async Task<CompanyRequestModel> Find(int id)
            => await this.db.Companies
                .ProjectTo<CompanyRequestModel>(this.mapper.ConfigurationProvider)
                .FirstOrDefaultAsync();

        public async Task<List<CompanyRequestModel>> GetAll()
            => await this.db.Companies
                .ProjectTo<CompanyRequestModel>(this.mapper.ConfigurationProvider)
                .ToListAsync();
    }
}
