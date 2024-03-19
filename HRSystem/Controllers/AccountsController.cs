using HRDomain.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace HRSystem.Controllers
{
    public class AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService) : HRBaseController
    {
       readonly UserManager<AppUser> _userManager = userManager;
       readonly SignInManager<AppUser> _signInManager = signInManager;
       readonly ITokenService _tokenService = tokenService;

        [HttpPost("login")] // /api/Accounts/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new StatusResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return Unauthorized(new StatusResponse(401));

            return Ok(new UserDTO()
            {
                Message = "succsess",
                FullName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user,_userManager)
            }) ;
        }

        [HttpPost("register")] // /api/Accounts/register
        public async Task<ActionResult<UserDTO>> Register(RegisterDTO registerDTO)
        {
            if (_userManager.FindByEmailAsync(registerDTO.Email) is not null)
                return BadRequest(new ValidationErrorResponse()
                { Errors = new[] { "This email is already in use" } });
            var user = new AppUser()
            {
                FullName = registerDTO.FullName,
                Email = registerDTO.Email,
                UserName = registerDTO.UserName,
            };

            var result = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!result.Succeeded) return BadRequest(new StatusResponse(400));

            return Ok(new UserDTO()
            {
                Message = "succsess",
                FullName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }
    }
}
