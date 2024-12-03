using MassTransit;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Consumers;
using SearchService.Data;
using SearchService.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
 
builder.Services.AddMassTransit(x => {

    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();

    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search",false));

    x.UsingRabbitMq((context, cfg) =>{
        cfg.ConfigureEndpoints(context);
    });
});
var mongoDbSettings = builder.Configuration.GetSection("MongoDbSettings").Get<SearchDatabaseSettings>();

builder.Services.Configure<SearchDatabaseSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddDbContext<SearchDbContext>(opt => opt.UseMongoDB(mongoDbSettings.ConnectionString ?? "", mongoDbSettings.DatabaseName?? ""));

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

try
{
    await DbInitializer.InitDb(app, mongoDbSettings);
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

    
app.Run();
