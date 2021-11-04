namespace NBUCareers.Services.Models.Identity
{
    public class LoginResponseModel
    {
        public string Token { get; set; }

        public bool Succeeded { get; set; } = true;

        public string ErrorMessage { get; set; }
    }
}
