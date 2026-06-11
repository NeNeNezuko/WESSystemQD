using Microsoft.AspNetCore.Mvc;

namespace WmsPlus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserSettingController : ControllerBase
    {
        [HttpGet("search")]
        public IActionResult Search(
            [FromQuery] string userCode = "",
            [FromQuery] string userName = "",
            [FromQuery] string dept = "",
            [FromQuery] bool includeSub = false,
            [FromQuery] bool showExpired = false,
            [FromQuery] string theme = "")
        {
            // TODO: 后期关联实际数据库表后补充查询逻辑
            return Ok(new
            {
                Success = true,
                Message = "",
                Data = Array.Empty<object>(),
                Total = 0
            });
        }
    }
}
