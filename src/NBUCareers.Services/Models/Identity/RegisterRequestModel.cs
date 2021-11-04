using NBUCareers.Models;
using NBUCareers.Infrastructure.Mapping;
using System.ComponentModel.DataAnnotations;

namespace NBUCareers.Services.Models.Identity
{
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
