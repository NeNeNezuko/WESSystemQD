using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using WmsPlus.Api.Data;
using WmsPlus.Api.Models;

namespace WmsPlus.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username))
                {
                    return BadRequest(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "用户名不能为空" 
                    });
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.NAME == request.Username);

                if (user == null)
                {
                    return Unauthorized(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "用户名或密码错误" 
                    });
                }

                bool passwordValid;
                
                if (string.IsNullOrEmpty(user.PWD))
                {
                    passwordValid = string.IsNullOrEmpty(request.Password) || request.Password == "";
                }
                else
                {
                    passwordValid = user.PWD == request.Password;
                }

                if (!passwordValid)
                {
                    return Unauthorized(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "用户名或密码错误" 
                    });
                }

                var token = GenerateToken(user);
                
                return Ok(new LoginResponse 
                { 
                    Success = true, 
                    Message = "登录成功",
                    User = new UserInfo
                    {
                        Name = user.NAME,
                        Dept = user.DEP ?? "",
                        CompNo = user.COMPNO,
                        Usr = user.USR
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "登录时发生错误");
                return StatusCode(500, new LoginResponse 
                { 
                    Success = false, 
                    Message = "服务器内部错误" 
                });
            }
        }

        [HttpPost("changepassword")]
        public async Task<ActionResult<LoginResponse>> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username))
                {
                    return BadRequest(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "用户名不能为空" 
                    });
                }

                var user = await _context.Users
                    .FirstOrDefaultAsync(u => u.NAME == request.Username);

                if (user == null)
                {
                    return NotFound(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "用户不存在" 
                    });
                }

                if (!string.IsNullOrEmpty(user.PWD) && user.PWD != request.OldPassword)
                {
                    return BadRequest(new LoginResponse 
                    { 
                        Success = false, 
                        Message = "原密码错误" 
                    });
                }

                user.PWD = request.NewPassword;
                user.SYS_DATE = DateTime.Now;
                
                await _context.SaveChangesAsync();

                return Ok(new LoginResponse 
                { 
                    Success = true, 
                    Message = "密码修改成功"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "修改密码时发生错误");
                return StatusCode(500, new LoginResponse 
                { 
                    Success = false, 
                    Message = "服务器内部错误" 
                });
            }
        }

        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            return Ok(new { authenticated = true });
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { success = true, message = "已退出登录" });
        }

        private string GenerateToken(User user)
        {
            var tokenData = $"{user.COMPNO}|{user.USR}|{user.NAME}|{DateTime.Now:yyyyMMddHHmmss}";
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(tokenData));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
