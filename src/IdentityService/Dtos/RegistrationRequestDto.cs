using System;
using System.ComponentModel.DataAnnotations;

namespace IdentityService.Dtos;

public class RegistrationRequestDto
{
  [Required]
  [EmailAddress]
  public string Email { get; set; } = "";
  [Required]
  [DataType(DataType.Password)]
  [StringLength(15, MinimumLength = 7, ErrorMessage = "Minimum of 7 charaters")]
  public string Password { get; set; } = "";
  public string Firstname { get; set; } = "";
  public string Lastname { get; set; } = "";
  public string Social { get; set; }
}
