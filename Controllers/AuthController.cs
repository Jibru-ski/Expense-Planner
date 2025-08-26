using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpensePlanner.Api.Data;
using ExpensePlanner.Api.Dtos;
using ExpensePlanner.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePlanner.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegistrationDto dto)
        {
            if (_context.Users.Any(u => u.Name == dto.Name))
            {
                return BadRequest("Username already exists.");
            }

            // Map DTO to User entity
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = HashPassword(dto.Password)
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            var UserDto = new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                CreatedAt = user.CreatedAt
            };

            return Ok(new { Message = "User registered successfully", User = UserDto });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginDto dto)
        {
            var user = _context.Users.SingleOrDefault(u => u.Name == dto.Name);
            if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(user);
            
            return Ok(new
            {
                token = token,
                user = new UserDto
                {
                    UserId = user.UserId,
                    Name = user.Name,
                    Email = user.Email,
                    CreatedAt = user.CreatedAt
                }
            });
        }

        private string HashPassword(string password)
        {
            // Use BCrypt
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(password));
        }
        
        private bool VerifyPassword(string enteredPassword, string storedHash)
        {
            return Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(enteredPassword)) == storedHash;
        }

        private string GenerateJwtToken(User user)
        {
            var securityKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration.")));
            var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials(securityKey, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);

            var claims = new List<System.Security.Claims.Claim>
            {
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, user.Name),
                new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials);

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}