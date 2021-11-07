namespace NBUCareers.Services.Models.Applications
{
    using NBUCareers.Infrastructure.Mapping;
    using NBUCareers.Models;

    public class ApplicationRequestModel : IMapFrom<Application>, IMapTo<Application>
    {
        public int Id { get; set; }

        public string CvUrl { get; set; }

        public byte[] CvData { get; set; }

        public string CoverLetter { get; set; }

        public string UserId { get; set; }

        public int JobOfferId { get; set; }
    }
}
