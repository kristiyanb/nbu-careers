namespace NBUCareers.Services.Models.Companies
{
    using System.Collections.Generic;

    using NBUCareers.Infrastructure.Mapping;
    using NBUCareers.Models;
    using NBUCareers.Services.Models.Offices;

    public class CompanyRequestModel : IMapFrom<Company>, IMapTo<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<OfficeRequestModel> Offices { get; set; }
    }
}
