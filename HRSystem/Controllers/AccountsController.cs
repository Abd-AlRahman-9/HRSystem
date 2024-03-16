using HRDomain.Entities.Identity;
using HRDomain.Services;

//using HRDomain.Services;
using HRSystem.DTO;
using HRSystem.Error_Handling;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HRSystem.Controllers
{
    
    public class AccountsController : HRBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountsController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")] // /api/Accounts/login
        public async Task<ActionResult<UserDTO>> Login(LoginDTO loginDTO)
        {
            var user = await _userManager.FindByEmailAsync(loginDTO.Email);
            if (user == null) return Unauthorized(new ErrorResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDTO.Password, false);
            if (!result.Succeeded) return Unauthorized(new ErrorResponse(401));

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
            if (!result.Succeeded) return BadRequest(new ErrorResponse(400));

            return Ok(new UserDTO()
            {
                Message = "succsess",
                FullName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet] // /api/Accounts => return all logged in users
        public async Task<ActionResult<UserDTO>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByIdAsync(email);
            return Ok(new UserDTO
            {
                Message = "succsess",
                FullName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateToken(user, _userManager)

            });
        }

        //[Authorize]
        //[HttpGet("emailExists")] 
        ////if true, refuse to register else register
        //public async Task<ActionResult<bool>> CheckEmailExists (string email)
        //{
        //    return await _userManager.FindByEmailAsync(email) != null;
        //}

    }
}
