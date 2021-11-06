namespace NBUCareers.Models
{
    using Base;

    public class Tag : Entity
    {
        public string Label { get; set; }

        public int JobOfferId { get; set; }

        public JobOffer JobOffer { get; set; }
    }
}
