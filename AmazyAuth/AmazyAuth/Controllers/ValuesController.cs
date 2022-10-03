using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AmazyAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        [Authorize]
        [Route("getLogin")]
        public IActionResult GetLogin()
        {
            return Ok($"Your login is {User.Identity.Name}");
        }
    }
}
