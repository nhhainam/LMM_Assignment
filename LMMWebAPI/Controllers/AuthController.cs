﻿using AutoMapper;
using LMMWebAPI.DataAccess;
using LMMWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LMMWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;
        private readonly LmmAssignmentContext _context;
        private readonly IMapper mapper;

        public AuthController(LmmAssignmentContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
            var mapconfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            this.mapper = mapconfig.CreateMapper();
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Login([FromBody] UserLogin userLogin)
        {
            var user = Authenticate(userLogin);

            if (user != null)
            {
                var token = Generate(user);
                return Ok(token);
            }

            return NotFound("User not found");
        }

        [AllowAnonymous]
        [HttpPost("[action]")]
        public IActionResult Register([FromBody] UserRegister userRegister)
        {
            // Check if the username already exists
            if (_context.Users.Any(u => u.Username == userRegister.Username))
            {
                return BadRequest("Username already exists");
            }

            // Create a new User object
            var user = new User
            {
                Username = userRegister.Username,
                Password = userRegister.Password,
                UserCode = userRegister.Username,
                Fullname = userRegister.Username,
                Email = userRegister.Email,
                Phone = userRegister.Username,
                RoleId = 3
            };

            // Add the user to the database
            _context.Users.Add(user);
            _context.SaveChanges();

            // Return a success response
            return Ok("User registered successfully");
        }
        private string Generate(User user)
        {
            // var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            //  var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            try
            {
                //create claims details based on the user information
                var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                            new Claim(ClaimTypes.Role, user.RoleId.ToString()),
                            new Claim(ClaimTypes.NameIdentifier, user.UserCode),
                            new Claim(ClaimTypes.Name, user.Username),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.UserData, user.UserId.ToString()),
                        };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
                var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _config["Jwt:Issuer"],
                    _config["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(30),
                    signingCredentials: signIn);

                return new JwtSecurityTokenHandler().WriteToken(token);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private User Authenticate(UserLogin userLogin)
        {
            var currentUser = _context.Users.Where(u => u.Username.Equals(userLogin.Username) && u.Password.Equals(userLogin.Password)).FirstOrDefault();

            if (currentUser != null)
            {
                return currentUser;
            }

            return null;
        }
    }
}
