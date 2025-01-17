using IdentityService.Abstractions;
using IdentityService.Data;
using IdentityService.Implementation;
using IdentityService.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDbContext<AccounntDbContext>(config => {
    config.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<AccounntDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultTokenProviders();

builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapControllers();
builder.Services.AddLogging();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var scope = app.Services.CreateScope();

var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

var roles = new [] {"Admin", "User"};

foreach(var role in roles)
{
    if(! await roleManager.RoleExistsAsync(role))
    {
        await roleManager.CreateAsync(new IdentityRole(role));
    }
}

var roleExists = await roleManager.RoleExistsAsync("USER");
if (!roleExists)
{
    throw new InvalidOperationException("Role USER does not exist.");
}


app.Run();


