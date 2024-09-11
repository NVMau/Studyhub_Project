using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using StudyHub.DAL.Models;
using Microsoft.Extensions.Options;
using StudyHub.BLL;
using Microsoft.AspNetCore.Authorization;
using StudyHub.modelRequest;
using System.Text.Json.Serialization;

namespace StudyHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly Jwt _jwtSettings;
        private readonly UserBLL _userBLL;
        public AuthenticationController(IOptions<Jwt> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
            _userBLL = new UserBLL();
        }

        // POST: api/User
        [HttpPost("Register")]
        public IActionResult AddUser([FromBody] CreateUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newUser = _userBLL.AddUser(user);
            return Ok(newUser);
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel user)
        {
            // Kiểm tra xác thực người dùng ở đây
            if (IsValidUser(user))
            {
                var token = GenerateJwtToken(user);
                return Ok(new { token });
            }
            else
            {
                return Unauthorized();
            }
        }

        private string GenerateJwtToken(LoginModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                    //new Claim(ClaimTypes.Role, user.Role) // Role của người dùng
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Thời gian hết hạn của token
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
        // ham tim user
        private bool IsValidUser(LoginModel user)
        {
           UserOu u = _userBLL.GetUserByUsernameAndPassword(user.Username, user.Password);
            if (u == null)
                return false;
            else
                return true;
        }
        // ham lay user
        [Authorize] // Đảm bảo chỉ người dùng đã xác thực mới có thể truy cập action này
        [HttpGet("userinfo")]
        public IActionResult GetUserInfo()
        {
            // Lấy thông tin người dùng từ token JWT
            var userIdentity = User.Identity as ClaimsIdentity;
            var username = userIdentity.FindFirst(ClaimTypes.Name)?.Value;
            var role = userIdentity.FindFirst(ClaimTypes.Role)?.Value;

            // Đây là nơi để bạn thực hiện các hoạt động khác, như truy vấn cơ sở dữ liệu
            // để lấy thông tin chi tiết về người dùng

            // Ví dụ: lấy thông tin từ cơ sở dữ liệu
            var user = _userBLL.GetUserByusername(username);

            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

    }

}
