namespace NBUCareers.Services.Models.JobOffers
{
    using System.Collections.Generic;

    using NBUCareers.Infrastructure.Mapping;
    using NBUCareers.Models;

    public class JobOfferRequestModel : IMapFrom<JobOffer>, IMapTo<JobOffer>
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public string Description { get; set; }

        public decimal? Salary { get; set; }

        public bool ShowSalaryAsRange { get; set; }

        public decimal? SalaryRangeMin { get; set; }

        public decimal? SalaryRangeMax { get; set; }

        public int CompanyId { get; set; }

        public int OfficeId { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
