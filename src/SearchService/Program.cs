using System.Net;
using MassTransit;
using MongoDB.Driver;
using MongoDB.Entities;
using Polly;
using Polly.Extensions.Http;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Models;
using SearchService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());

builder.Services.AddMassTransit(x => {

    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));

    x.UsingRabbitMq((context, cfg) => 
    {
           cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host => 
        {
            host.Username(builder.Configuration.GetValue("RabbitMq:Host", "guest"));
            host.Password(builder.Configuration.GetValue("RabbeitMq:Password", "guest"));
        });
        
        cfg.ReceiveEndpoint("search-auction-created", e => 
        {
            e.UseMessageRetry(r => r.Interval(5, 5));

            e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.MapControllers();

app.Lifetime.ApplicationStarted.Register(async () => {
   
    try
    {
        await DbInitializer.InitDb(app);
    }
    catch (Exception e)
    {
    Console.WriteLine(e);
    }  
});

app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy() 
    => HttpPolicyExtensions.HandleTransientHttpError().OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
    .WaitAndRetryForeverAsync(_=> TimeSpan.FromSeconds(3));

