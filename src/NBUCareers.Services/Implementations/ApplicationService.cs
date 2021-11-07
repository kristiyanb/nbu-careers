namespace NBUCareers.Services.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using Azure.Storage.Blobs;

    using Microsoft.EntityFrameworkCore;

    using NBUCareers.Data;
    using NBUCareers.Models;
    using NBUCareers.Services.Contracts;
    using NBUCareers.Services.Models.Applications;

    public class ApplicationService : IApplicationService
    {
        private readonly AppDbContext db;
        private readonly IMapper mapper;
        // TODO:
        //private readonly BlobServiceClient blobServiceClient;

        public ApplicationService(
            AppDbContext db, 
            //BlobServiceClient blobServiceClient,
            IMapper mapper) 
        {
            this.db = db;
            this.mapper = mapper;
            //this.blobServiceClient = blobServiceClient;
        }

        public async Task<ApplicationResponseModel> Create(ApplicationRequestModel model)
        {
            var application = this.mapper.Map<Application>(model);

            //var blobId = await this.UploadFile(model.CvData);
            //application.CvUrl = $"{blobId}";

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

        //private async Task<string> UploadFile(byte[] data)
        //{
        //    var containerClient = this.blobServiceClient.GetBlobContainerClient("CVs");

        //    var blobId = Guid.NewGuid().ToString();
        //    using var content = new MemoryStream(data);

        //    await containerClient.UploadBlobAsync(blobId, content);

        //    return blobId;
        //}
    }
}
