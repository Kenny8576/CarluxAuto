using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Data;

public class AccounntDbContext : IdentityDbContext<User>
{
    public AccounntDbContext(DbContextOptions options) : base(options)
    {
    }
}
