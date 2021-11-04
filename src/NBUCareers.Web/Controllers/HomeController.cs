using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace NBUCareers.Web.Controllers
{
    public class HomeController : ApiController
    {
        public async Task<IActionResult> Get()
        {
            return Ok("Home");
        }
    }
}
