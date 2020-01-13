using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Model;

namespace WebAPIUsingEFCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;

        public UserController(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
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
    }
}