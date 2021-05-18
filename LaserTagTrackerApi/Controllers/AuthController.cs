using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LaserTagTrackerApi.Model;
using LaserTagTrackerApi.Model.DTOs;
using LaserTagTrackerApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace LaserTagTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository userRepository;

        public AuthController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<RegisteredUserDto>> Register([FromBody] CredentialsDto dto)
        {
            if (await userRepository.ExistsByUsername(dto.Username))
            {
                return BadRequest("User already exists");
            }
            
            var user = new User()
            {
                Username = dto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            user = await this.userRepository.CreateUser(user);

            return new RegisteredUserDto()
            {
                Id = user.Id,
                Username = user.Username
            };
        }
        
        [HttpPost("login")]
        public async Task<ActionResult<AuthSuccessDto>> Login([FromBody] CredentialsDto dto)
        {
            var user = await this.userRepository.FindByUsername(dto.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.Password))
            {
                return Unauthorized();
            }

            var tokenGenerator = new JwtSecurityTokenHandler();
            var secret = Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenGenerator.CreateToken(tokenDescriptor);

            return new AuthSuccessDto()
            {
                UserId = user.Id,
                Username = user.Username,
                Token = tokenGenerator.WriteToken(token)
            };
        }
    }
}