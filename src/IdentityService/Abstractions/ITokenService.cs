using System;

namespace IdentityService.Abstractions;

public interface ITokenService
{
  string GenerateJwtToken(User user, string role);
}
