using NBUCareers.Models.Base;
using System.Collections.Generic;

namespace NBUCareers.Models
{
    public class Company : Entity
    {
        public Company()
        {
            this.JobOffers = new HashSet<JobOffer>();
            this.Offices = new HashSet<Office>();
        }

        public string Name { get; set; }

        public virtual ICollection<JobOffer> JobOffers { get; set; }

        public virtual ICollection<Office> Offices { get; set; }
    }
}
