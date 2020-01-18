using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI.Model;

namespace WebAPIUsingEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        public IConfiguration configuration;

        public UserController(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("InsertNewUser")]
        public async Task<bool> InsertUser(User user)
        {
            IdentityUser newUser = new IdentityUser
            {
                UserName = user.UserEmail,
                Email = user.UserEmail
            };
            var result = await userManager.CreateAsync(newUser, user.Password);
            return true;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(User user)
        {
            try
            {
                IdentityUser dbUser = await userManager.FindByNameAsync(user.UserEmail);
                if (dbUser != null && await userManager.CheckPasswordAsync(dbUser, user.Password))
                {

                    var authClaims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Sub, dbUser.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SigningKey"]));

                    var token = new JwtSecurityToken(
                        issuer: configuration["JWT:ValidIssuer"],
                        audience: configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(3),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
            }
            catch (Exception ex)
            {

            }
            
            return Unauthorized();
        }

    }
}