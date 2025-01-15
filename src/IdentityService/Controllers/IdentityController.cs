using IdentityService.Abstractions;
using IdentityService.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly UserManager<User> _userManager;

        public AccountController(IAuthService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
        {
            if (registrationRequestDto == null)
            {
                return BadRequest(new { Error = "Invalid request payload" });
            }

            var result = await _authService.Register(registrationRequestDto);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest(new { Error = "Invalid request payload" });
            }

            var result = await _authService.Login(loginDto);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (resetPasswordDto == null)
            {
                return BadRequest(new { Error = "Invalid request payload" });
            }

            var result = await _authService.ResetPassword(resetPasswordDto);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromQuery] string email, [FromQuery] string token)
        {
            var result = await _authService.ConfirmEmail(email, token);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("forgot-password")]
        public async Task<ActionResult> ForgotPasswordAsync([FromQuery] string email)
        {
            var result = await _authService.ForgotPasswordAsync(email);
            if (result.IsSuccessful)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}






// using IdentityService.Abstractions;
// using IdentityService.Dtos;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Http.HttpResults;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.Data;
// using Microsoft.AspNetCore.Mvc;

// namespace IdentityService.Controllers
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class IdentityController : ControllerBase
//     {
//         private readonly IAuthService _authService;
//         private readonly UserManager<User> _userManager;

//         public IdentityController(IAuthService authService, UserManager<User> userManager)
//         {
//             _authService = authService;
//             _userManager = userManager;
//         }

//         [HttpPost("register")]
//         public async Task<ActionResult> Register([FromBody] RegistrationRequestDto registrationRequestDto)
//         {
//             var result = await _authService.Register(registrationRequestDto);
//             if(result.IsSuccessful)
//              {   
//                 return Ok(result); 
//              }
//             return BadRequest(result);
//         }

//         [HttpPost("login")]
//         public async Task<ActionResult> Login([FromBody] LoginDto loginDto)
//         {
//             var result = await _authService.Login(loginDto);
//             if(result.IsSuccessful)
//             {
//                 return Ok(result);
//             }
//             return BadRequest(result);
//         }

//         [HttpPost("reset-password")]
//         public async Task<ActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
//         {
//             var result = await _authService.ResetPassword(resetPasswordDto);

//             if(result.IsSuccessful)
//             {
//                 return Ok(result);
//             }
//             return BadRequest(result);
//         }

//         [HttpGet("confirm-email")]
//         public async Task<ActionResult> ConfirmEmail(string email, string token)
//         {
//             var result = await _authService.ConfirmEmail(email, token);
//             if(result.IsSuccessful)
//             {
//                 return Ok(result);
//             }

//             return BadRequest(result);
//         }

//         [HttpGet("forgot-password")]
//         public async Task<ActionResult> ForgotPasswordAsync([FromQuery] string email)
//         {
//             var result = await _authService.ForgotPasswordAsync(email);

//             if(result.IsSuccessful)
//             {
//                 return Ok(result);
//             }
//             return BadRequest(result);
//         }
//     }
// }
