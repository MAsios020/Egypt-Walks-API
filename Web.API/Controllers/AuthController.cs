using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.API.Models.Dto;
using Web.API.Repositories.Interfaces;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private UserManager<IdentityUser> UserManager;
        private readonly ItokenRepository itokenRepository;

        public AuthController(UserManager<IdentityUser>userManager,ItokenRepository itokenRepository)
        {
            UserManager = userManager;
            this.itokenRepository = itokenRepository;
        }

      

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthDto RegisterAuthDto)
        {
          
            if (RegisterAuthDto == null || string.IsNullOrEmpty(RegisterAuthDto.Email) || string.IsNullOrEmpty(RegisterAuthDto.Password))
            {
                return BadRequest("Invalid registration data.");
            }

            var user = new IdentityUser
            {
                UserName = RegisterAuthDto.UserName,
                Email = RegisterAuthDto.Email
            };

            var result = await UserManager.CreateAsync(user, RegisterAuthDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            // Simulate successful registration
            return Ok("User registered successfully.");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthDto LoginAuthDto)
        {



            if (LoginAuthDto == null || string.IsNullOrEmpty(LoginAuthDto.UserName) || string.IsNullOrEmpty(LoginAuthDto.Password))
            {
                return BadRequest("Invalid login data.");
            }

            var user = await UserManager.FindByNameAsync(LoginAuthDto.UserName);
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            var result = await UserManager.CheckPasswordAsync(user, LoginAuthDto.Password);
            if (!result)
            {
                return Unauthorized("Invalid password.");
            }

            var roles = await UserManager.GetRolesAsync(user);

            if(roles == null )
            {
                return Unauthorized("User has no roles assigned.");
            }

             var Token =  itokenRepository.GenerateTokenAsync(user, roles.ToArray());
             return Ok(Token);


            // Simulate successful login

        }
    }
}
