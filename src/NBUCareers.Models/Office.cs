using NBUCareers.Models.Base;

namespace NBUCareers.Models
{
    public class Office : Entity
    {
        public string Country { get; set; }

        public string City { get; set; }

        public string Address { get; set; }

        public int CompanyId { get; set; }

        public Company Company { get; set; }
    }
}
