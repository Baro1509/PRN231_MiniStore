using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FlowerBouquetAPI.Controllers {
    [Route("odata/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase {
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _config;

        public LoginController(IConfiguration config, IStaffRepository staffRepository) {
            _config = config;
            _staffRepository = staffRepository;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginCredentials Login) {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var email = config.GetValue<string>("Admin:Email");
            var password = config.GetValue<string>("Admin:Password");
            if (Login.Email.Equals(email) && Login.Password.Equals(password)) {
                //HttpContext.Session.SetInt32("id", 0);
                return Ok(new {
                    role = "Admin",
                    id = 0,
                    token = GenerateJSONWebToken(new staff { StaffId = "Admin"})
                });
            }
            var staff = _staffRepository.Login(Login.Email, Login.Password);
            if (staff != null) {
                //HttpContext.Session.SetInt32("id", customer.CustomerId);
                return Ok(new {
                    role = staff.RoleId,
                    id = staff.StaffId,
                    token = GenerateJSONWebToken(staff)
                });
            }
            return Unauthorized();
        }

        private string GenerateJSONWebToken(staff Staff) {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                new Claim("StaffId", Staff.StaffId)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                        _config["Jwt:Issuer"],
                        _config["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class LoginCredentials {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
