using System;
using IdentityService.Abstractions;
using IdentityService.Data;
using IdentityService.Dtos;
using IdentityService.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Implementation;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;
    private readonly AccounntDbContext _accounntDbContext;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<AuthService> _logger;

    public AuthService(AccounntDbContext accounntDbContext, UserManager<User> userManager, 
    IHttpContextAccessor httpContextAccessor, ILogger<AuthService> logger, IConfiguration config, IEmailService emailService, ITokenService tokenService)
    {
        _config = config;
        _emailService = emailService;
        _tokenService = tokenService;
        _accounntDbContext = accounntDbContext;
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

           private static string CreateForgotPasswordEmailBody(User user, string token, string url)
       {
           string resetUrl = $"{url}/reset-password?email={user.Email}&token={Uri.EscapeDataString(token)}";
           return $@"
                       <html>
                       <body>
                           <p>Dear {user.UserName},</p>
                           <p>You requested to reset your password. Please click the link below to reset your password:</p>
                           <p><a href='{resetUrl}'>Reset Password</a></p>
                           <p>If you did not request a password reset, please ignore this email.</p>
                           <p>Thank you,</p>
                           <p>Room8s Team</p>
                       </body>
                       </html>";
       }

    public async Task<ResponseDto<UserDto>> ConfirmEmail(string email, string token)
    {
        var thisUser = await _userManager.FindByEmailAsync(email);
        if(thisUser != null)
        {
            var result = await _userManager.ConfirmEmailAsync(thisUser, token);

            if(result.Succeeded) return ResponseDto<UserDto>.Success("Email Confirm Successfully", 200);
            return ResponseDto<UserDto>.Failure(result.Errors.Select(e => new Error(e.Code, e.Description)), 400);
        }

        var error = new List<Error>{
            new Error("400", "User does not exist")
        };

        return ResponseDto<UserDto>.Failure(error, 400);
    }

    public async Task<ResponseDto<UserDto>> ForgotPasswordAsync(string email)
    {
        var errors = new List<Error>();

        var thisUser = await _userManager.FindByEmailAsync(email);
        if(thisUser == null)
        {
            errors.Add(new Error("400", "User with this email address does not exist"));
            return ResponseDto<UserDto>.Failure(errors, 400);
        }

        var newToken = await _userManager.GeneratePasswordResetTokenAsync(thisUser);

        var url = _config.GetSection("FrontEndUrl").Value;

        if(string.IsNullOrEmpty(url))
        {
            errors.Add(new Error("400", "The frontend url is missing in the app settings"));
            return ResponseDto<UserDto>.Failure(errors, 400);
        }

        string emailBody =  CreateForgotPasswordEmailBody(thisUser, newToken, url);

         var responseMsg = await _emailService.SendEmail(email, "Reset Password Request", emailBody);

        if (responseMsg != string.Empty)
        {
            //throw new InternalServerException(responseMsg);
            errors.Add(new Error("500", responseMsg));
            return ResponseDto<UserDto>.Failure(errors, 500);
        }

        return ResponseDto<UserDto>.Success("Successfully sent reset token to User's email.", 200);
    }

    public async Task<ResponseDto<LoginResultDto>> Login(LoginDto loginDto)
    {
        var error = new List<Error>();

        var thisUser = await _userManager.FindByEmailAsync(loginDto.Email);
        if(thisUser != null)
        {
           if(await _userManager.CheckPasswordAsync(thisUser, loginDto.Password))
           {
                var roles = await _userManager.GetRolesAsync(thisUser);

                 var returnUser = new LoginResultDto()
                {
                    UserId = thisUser.Id,
                    Roles = roles.ToList(),
                    Token = _tokenService.GenerateJwtToken(thisUser, roles[0])
                };

                return ResponseDto<LoginResultDto>.Success(returnUser);
           };
        }

            error.Add(new Error("400", "Invalid credential"));
            return ResponseDto<LoginResultDto>.Failure(error, 400);
    }

    public async Task<ResponseDto<UserDto>> Register(RegistrationRequestDto registrationRequestDto)
    {
        
            var user = await _userManager.FindByEmailAsync(registrationRequestDto.Email);

            if (user != null )
            {
                var errors = new List<Error>
                  {
                   new Error("400", "User already exists.")
                      };
                return ResponseDto<UserDto>.Failure(errors, 400);
            }

            var userToAdd = new User()
            {
                UserName = registrationRequestDto.Email,
                Email = registrationRequestDto.Email,
                NormalizedEmail = registrationRequestDto.Email.ToUpper(),
                FirstName = registrationRequestDto.Firstname,
                LastName = registrationRequestDto.Lastname,
            };

            var result = await _userManager.CreateAsync(userToAdd, registrationRequestDto.Password);

            if (!result.Succeeded)
                return ResponseDto<UserDto>.Failure(result.Errors.Select(e => new Error(e.Code, e.Description)), 500);

            var userToReturn = _userManager.Users.First(u => u.UserName == registrationRequestDto.Email);

            if (userToReturn != null)
            {

                var requestScheme = _httpContextAccessor.HttpContext.Request.Scheme;
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(userToAdd);
                var confirmationLink = $"{requestScheme}://localhost:5000/api/Account/confirm-email?email={Uri.EscapeDataString(userToAdd.Email)}&token={Uri.EscapeDataString(token)}";

                var body = @$"
                  <h1>Confirmation Email</h1>
                   <p>
                    Please confirm your email address by clicking the link below:
                  <a href='{confirmationLink}'>Confirm Email</a>
                            <p>Carluxmart team</p>
                             <p>twinners</p>
                      </p>
                        ";

                var response = await _emailService.SendEmail(userToAdd.Email, "Confirmation Link", body);

            }

             await _userManager.AddToRoleAsync(userToAdd, "user");
            //await AssignRole(userToReturn.Email, "USER");
            //var roles = await _userManager.GetRolesAsync(userToReturn);

            UserDto UserDTO = new()
            {

                Email = userToReturn.Email,
                FirstName = userToReturn.FirstName,
                LastName = userToReturn.LastName,
                PhoneNumber = userToReturn.PhoneNumber,
            };

            return ResponseDto<UserDto>.Success(UserDTO, "Successfully Registered New User", 201);

    }

    public async Task<ResponseDto<UserDto>> ResetPassword(ResetPasswordDto ResetPasswordDto)
    {
        		var user = await _userManager.FindByEmailAsync(ResetPasswordDto.Email);
		if (user != null)
        {
            var result = await _userManager.ResetPasswordAsync(user, ResetPasswordDto.Token, ResetPasswordDto.NewPassword);

            if (!result.Succeeded)
                return ResponseDto<UserDto>.Failure(result.Errors.Select(e => new Error(e.Code, e.Description)));
            else
                return ResponseDto<UserDto>.Success("Successful Password Reset", 200);
        }
        else
        {
            var errors = new List<Error>
            {
               new Error("404", "User does not exist")
            };
            return ResponseDto<UserDto>.Failure(errors, 404);

        }
    }

}
    

