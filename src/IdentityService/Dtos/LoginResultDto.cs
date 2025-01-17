using System;

namespace IdentityService.Dtos;

public class LoginResultDto
{
  public string Token { get; set; }
  public string UserId { get; set; }
  public List<string> Roles { get; set; } = new List<string>();
}
