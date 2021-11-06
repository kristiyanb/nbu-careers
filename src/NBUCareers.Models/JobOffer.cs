namespace NBUCareers.Models
{
    using System.Collections.Generic;

    using Base;

    public class JobOffer : Entity
    {
        public JobOffer()
        {
            this.Applications = new HashSet<Application>();
            this.Tags = new HashSet<Tag>();
        }

        public string Position { get; set; }

        public string Description { get; set; }

        public decimal? Salary { get; set; }

        public bool ShowSalaryAsRange { get; set; }

        public decimal? SalaryRangeMin { get; set; }

        public decimal? SalaryRangeMax { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }

        public int OfficeId { get; set; }

        public Office Office { get; set; }

        public virtual ICollection<Application> Applications { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}
