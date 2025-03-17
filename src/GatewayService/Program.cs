using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityService2Url"]; // Identity service URL
        options.RequireHttpsMetadata = false; // Change to true in production
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, // Optional: Disable audience validation if you don't have a specific audience
            ValidIssuer = "http://localhost:5000", // Set this to the actual issuer URL
            NameClaimType = "username"
        };
    });


builder.Services.AddCors(options => {
    options.AddPolicy("customPolicy", b => {
        b.AllowAnyHeader()
            .AllowAnyMethod().AllowCredentials().WithOrigins(builder.Configuration["ClientApp"]);
    });
});

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.UseCors();

app.MapReverseProxy();

app.UseAuthentication();
app.UseAuthorization();


app.Run();


// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;

// var builder = WebApplication.CreateBuilder(args);

// // Configure Authentication
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Authority = builder.Configuration["IdentityService2Url"]; // Identity service URL
//         options.RequireHttpsMetadata = false; // Change to true in production
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateAudience = false, // Disable audience validation if necessary
//             ValidIssuer = "http://localhost:5000", // Ensure this matches the actual issuer
//             NameClaimType = "username"
//         };
//     });

// // Configure CORS
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("customPolicy", b =>
//     {
//         b.AllowAnyHeader()
//          .AllowAnyMethod()
//          .AllowCredentials()
//          .WithOrigins(builder.Configuration["ClientApp"]); // Ensure this is set in appsettings.json or env variables
//     });
// });

// // Configure Reverse Proxy (YARP)
// builder.Services.AddReverseProxy()
//                 .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// var app = builder.Build();

// // Apply CORS before routing & YARP
// app.UseCors("customPolicy");

// // Authentication & Authorization (before Reverse Proxy)
// app.UseAuthentication();
// app.UseAuthorization();

// // Reverse Proxy (After CORS & Authentication)
// app.MapReverseProxy();

// app.Run();



