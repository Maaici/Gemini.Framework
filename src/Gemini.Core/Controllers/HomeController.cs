using Microsoft.AspNetCore.Mvc;

namespace Gemini.Core.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Ok(new { code=200,msg ="success"});
        }
    }
}