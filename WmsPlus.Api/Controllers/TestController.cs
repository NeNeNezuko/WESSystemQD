using Microsoft.AspNetCore.Mvc;

namespace WmsPlus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { 
                message = "Test API is working!",
                path = Request.Path,
                method = Request.Method,
                headers = Request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString())
            });
        }

        [HttpPost("echo")]
        public IActionResult Echo([FromBody] object data)
        {
            return Ok(new {
                message = "Echo successful",
                received = data,
                path = Request.Path
            });
        }
    }
}
