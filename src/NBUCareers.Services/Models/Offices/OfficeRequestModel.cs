namespace NBUCareers.Services.Models.Offices
{
    using NBUCareers.Infrastructure.Mapping;
    using NBUCareers.Models;

    public class OfficeRequestModel : IMapTo<Office>
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int CompanyId { get; set; }
    }
}
