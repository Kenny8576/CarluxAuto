using System;
using IdentityService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.Abstractions;

public interface IAuthService
{
    Task <ResponseDto<UserDto>> Register(RegistrationRequestDto registrationRequestDto);
    Task<ResponseDto<LoginResultDto>> Login(LoginDto loginDto);
    Task<ResponseDto<UserDto>> ResetPassword(ResetPasswordDto ResetPasswordDto);
    Task<ResponseDto<UserDto>> ConfirmEmail(string email, string token);
    Task<ResponseDto<UserDto>> ForgotPasswordAsync(string email);
}
