namespace NBUCareers.Models
{
    using Base;

    public class Application : Entity
    {
        public string CvUrl { get; set; }

        public string CoverLetter { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public int JobOfferId { get; set; }

        public JobOffer JobOffer { get; set; }
    }
}
