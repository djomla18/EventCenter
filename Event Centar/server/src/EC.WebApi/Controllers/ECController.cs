using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EC.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ECController : ControllerBase
    {

        [HttpGet("[action]")]
        public IActionResult TestMethod() {

            return Ok("This is a test method");
        }
    }
}
