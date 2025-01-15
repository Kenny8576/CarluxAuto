using System;

namespace IdentityService.Services;

public interface IEmailService
{
   public Task<string> SendEmail(string recipientEmail, string subject, string body);
}
