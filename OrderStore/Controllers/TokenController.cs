using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OrderStore.Domain.Interfaces;
using OrderStore.Domain.Models.Users;

using OrderStore.Repository;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;



namespace OrderStore.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        public IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        public TokenController(IUnitOfWork unitOfWork ,IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult GetToken(UserViewModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Username or password is not valid");
            }
            if (user != null && user.UserName != null)
            {

                var us = _unitOfWork.User.CheckUserExist(user);
                if (us )
                {
                   var claim = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                    
                        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat,DateTime.UtcNow.ToString()),
                      //  new Claim("UserId" ,user.UserId.ToString()),
                        new Claim("UserName" , user.UserName),
                        new Claim(ClaimTypes.Name,user.UserName),
                        new Claim(ClaimTypes.Email, user.UserName),
                        new Claim(ClaimTypes.Role,"Admin")
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signInCredential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        issuer: _configuration["Jwt:Issuer"],
                        audience: _configuration["Jwt:Audience"],
                        claims: claim,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signInCredential);

                    var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                    return Ok(new { token = tokenString });
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
    
