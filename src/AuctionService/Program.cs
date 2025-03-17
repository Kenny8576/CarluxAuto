using AuctionService.Consumers;
using AuctionService.Data;
using AuctionService.Services;
using Grpc.AspNetCore.Server;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<AuctionDbContext>(opt => {
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMassTransit(x => {

    x.AddEntityFrameworkOutbox<AuctionDbContext>(o => {
        o.QueryDelay = TimeSpan.FromSeconds(10);

        o.UsePostgres();
        o.UseBusOutbox();
    });

    x.AddConsumersFromNamespaceContaining<AuctionCreatedFaultConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("auction", false));

    x.UsingRabbitMq((context, cfg) => 
    {

          cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host =>
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Username", "guest"));
            host.Password(builder.Configuration.GetValue("RabbitMq:Password", "guest"));
        });

        cfg.ConfigureEndpoints(context);
    });
});

// var identityServerToUse = builder.Configuration["UseIdentityService"] == "1"
//     ? builder.Configuration["IdentityService2Url"]
//     : builder.Configuration["IdentityServiceUrl"];

// Console.WriteLine($"Using Identity Server: {identityServerToUse}");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityService2Url"];
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters.ValidateAudience = false;
        options.TokenValidationParameters.NameClaimType = "username";
    });


// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Authority = builder.Configuration["IdentityService2Url"]; // Identity service URL
//         options.RequireHttpsMetadata = false; // Change to true in production
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateAudience = false, // Optional: Disable audience validation if you don't have a specific audience
//             ValidIssuer = "http://localhost:5000", // Set this to the actual issuer URL
//             NameClaimType = "username"
//         };
//     });

builder.Services.AddGrpc();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapGrpcService<GrpcAuctionService>();


try{
    DbInitializer.InitDb(app);
}catch (Exception ex)
{
    Console.WriteLine(ex);
}


app.Run();

