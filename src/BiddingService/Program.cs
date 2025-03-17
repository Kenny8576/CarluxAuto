using BiddingService.Consumer;
using BiddingService.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MongoDB.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();

builder.Services.AddMassTransit(x => {

    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("bids", false));

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

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//         .AddJwtBearer(options => 
//         {
//             options.Authority = builder.Configuration["IdentityService2Url"];
//             options.RequireHttpsMetadata = false;
//             options.TokenValidationParameters.ValidateAudience = false;
//             options.TokenValidationParameters.NameClaimType = "username";
//         });

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["IdentityService2Url"]; // Identity service URL
        options.RequireHttpsMetadata = false; // Change to true in production
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false, // Optional: Disable audience validation if you don't have a specific audience
            ValidIssuer = "http://localhost:5000", // Set this to match the actual issuer URL
            NameClaimType = "username"
        };

    });



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHostedService<CheckAuctionFinished>();
builder.Services.AddScoped<GrpcAuctionClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

await DB.InitAsync("BidDb", MongoClientSettings.
FromConnectionString(builder.Configuration.GetConnectionString("BidDbConnection")));

app.Run();

