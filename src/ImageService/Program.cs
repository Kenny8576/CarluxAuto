using CloudinaryDotNet;
using ImageService.Photos;
using ImageService.Services.Interface;
using Microsoft.Extensions.Options;
using ImageService.Services;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));
builder.Services.AddSingleton(Provider => {
    var config = Provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
    return new Cloudinary(new Account(config.CloudName, config.ApiKey, config.ApiSecret));
});

builder.Services.AddScoped<IImageService, ImagesService>();

builder.Services.AddMassTransit(x => {
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("image", false));

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

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
