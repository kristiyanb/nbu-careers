namespace NBUCareers.Services.Models.Base
{
    public abstract class ResponseModel
    {
        public bool Succeeded { get; set; } = true;

        public string ErrorMessage { get; set; }
    }
}
