using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
        private readonly IConfiguration _configuration;

        public AuthController(AppDbContext context, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
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

                var token = GenerateJwtToken(user);

                return Ok(new LoginResponse
                {
                    Success = true,
                    Message = "登录成功",
                    Token = token,
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

        [Authorize]
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

        [Authorize]
        [HttpGet("check")]
        public IActionResult CheckAuth()
        {
            var userName = User.FindFirst(ClaimTypes.Name)?.Value;
            return Ok(new { authenticated = true, username = userName });
        }

        [Authorize]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new { success = true, message = "已退出登录" });
        }

        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"] ?? "WmsPlus_SecretKey_2024!@#$%^&*()_+QwErTyUiOp"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireMinutes = int.TryParse(_configuration["Jwt:ExpireMinutes"], out var mins) ? mins : 480;

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.NAME),
                new Claim("CompNo", user.COMPNO),
                new Claim("Usr", user.USR),
                new Claim("Dept", user.DEP ?? "")
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"] ?? "WmsPlus.Api",
                audience: _configuration["Jwt:Audience"] ?? "WmsPlus",
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
