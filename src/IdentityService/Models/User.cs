using System;
using Microsoft.AspNetCore.Identity;

public class User : IdentityUser
{
 public string FirstName { get; set; } 
 public string LastName { get; set; }
 public string ProfilePictureUrl { get; set; } 
 public int Age { get; set; }
 public string BirthDate { get; set; } 
 public string City { get; set; } 
 public string Gender { get; set; } 
 public string HowDidYouHear { get; set; }
 public DateTimeOffset CreatedAt { get; set; }
 public DateTimeOffset UpdatedAt { get; set; } 
 public bool IsDeleted { get; set; }
}
