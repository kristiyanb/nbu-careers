using NBUCareers.Models.Base;

namespace NBUCareers.Models
{
    public class Tag : Entity
    {
        public string Label { get; set; }

        public int JobOfferId { get; set; }

        public JobOffer JobOffer { get; set; }
    }
}
