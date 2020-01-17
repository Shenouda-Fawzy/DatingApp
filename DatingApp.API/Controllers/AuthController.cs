using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;

        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto userDTO) 
        {
// This used if not useing [ApiController]
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);

            userDTO.Username = userDTO.Username.ToLower();

            if (await _repo.UserExsits(userDTO.Username))
                return BadRequest("Username already exists");

            // Prepare the user
            User userToCreate = new User { UserName = userDTO.Username };

            User cratedUser = await _repo.Register(userToCreate, userDTO.Password);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// This Method for login the user and generate a JWT token
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto) 
        {

            User user = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if (user == null)
                return Unauthorized();

            // Claim is which will be in the payload
            Claim [] claims = new[] 
            {
                new Claim(ClaimTypes.NameIdentifier , user.Id.ToString()),
                new Claim(ClaimTypes.Name , user.UserName)
            };

            // Secret key
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("AppSettings:Token").Value));


            SigningCredentials creds = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDiscriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDiscriptor);
            return Ok(new { token = tokenHandler.WriteToken(token) });
        }
    }
}