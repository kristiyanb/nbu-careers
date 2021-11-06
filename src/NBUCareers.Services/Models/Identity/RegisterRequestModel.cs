namespace NBUCareers.Services.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using NBUCareers.Infrastructure.Mapping;
    using NBUCareers.Models;

    public class RegisterRequestModel : IMapTo<User>
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
